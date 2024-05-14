using Microsoft.EntityFrameworkCore;
using FadeFactory_Account.Models;
using DotEnv;
using System;
using DotNetEnv;

namespace FadeFactory_Account.Data
{
    public class FadeFactoryContext : DbContext
    {
        public DbSet<Account>? Accounts { get; set; }
        public DbSet<Customer>? Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Load the .env file
            Env.Load();

            // Use Cosmos DB configuration from .env file
            string cosmosDbEndpoint = Environment.GetEnvironmentVariable("cosmosDBEndpoint") ?? string.Empty;
            string cosmosDbKey = Environment.GetEnvironmentVariable("cosmosDBKey") ?? string.Empty;
            string cosmosDbDatabaseId = Environment.GetEnvironmentVariable("cosmosDBDatabaseName") ?? string.Empty;

            if (string.IsNullOrEmpty(cosmosDbEndpoint) || string.IsNullOrEmpty(cosmosDbKey) || string.IsNullOrEmpty(cosmosDbDatabaseId))
            {
                throw new InvalidOperationException("Missing or invalid Cosmos DB configuration in .env file.");
            }

            try
            {
                optionsBuilder.UseCosmos(cosmosDbEndpoint, cosmosDbKey, cosmosDbDatabaseId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error configuring Cosmos DB.", ex);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .ToContainer("Accounts") // This will store in container named Accounts
                .HasPartitionKey(a => a.Id); // This will partition the data based on the Id

            modelBuilder.Entity<Customer>()
                .ToContainer("Customers")
                .HasPartitionKey(c => c.Id);

            modelBuilder.Entity<Customer>().OwnsMany(p => p.Orders);
        }
    }
}
