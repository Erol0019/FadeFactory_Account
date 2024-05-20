using FadeFactory_Accounts;
using FadeFactory_Accounts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Configuration;



namespace FadeFactory_Accounts.Data
{
    public class AccountRepository : IAccountRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _taskContainer;
        public AccountRepository(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;

            var databaseName = Environment.GetEnvironmentVariable("cosmosDbName");
            if (string.IsNullOrEmpty(databaseName))
            {
                throw new ArgumentNullException(nameof(databaseName), "The database name cannot be null or empty.");
            }

            var taskContainerName = "Accounts";
            _taskContainer = _cosmosClient.GetContainer(databaseName, taskContainerName);
        }
        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            var query = _taskContainer.GetItemQueryIterator<Account>(new QueryDefinition("SELECT * FROM account"));
            List<Account> results = new List<Account>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<IEnumerable<Account>> GetAccountByIdAsync(string Id)
        {
            var query = _taskContainer.GetItemLinqQueryable<Account>()
                .Where(a => a.Id == Id)
                .ToFeedIterator();

            var accounts = new List<Account>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                accounts.AddRange(response);
            }

            return accounts;
        }
        public async Task<ActionResult<IEnumerable<Account>>> GetAccountByFirstNameAsync(string firstName)
        {
            var query = _taskContainer.GetItemLinqQueryable<Account>()
                .Where(f => f.FirstName == firstName)
                .ToFeedIterator();
            var accounts = new List<Account>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                accounts.AddRange(response);
            }
            return accounts;
        }
        public async Task<Account> CreateAccountAsync(Account account)
        {
            var response = await _taskContainer.CreateItemAsync(account);
            return response.Resource;
        }

        public async Task<Account> UpdateAccountAsync(Account account)
        {
            var response = await _taskContainer.ReplaceItemAsync(account, account.Id);
            return response.Resource;
        }
        public async Task DeleteAccountAsync(string Id)
        {
            try
            {
                await _taskContainer.DeleteItemAsync<Account>(Id, PartitionKey.None);
                Console.WriteLine($"Account with id {Id} deleted successfully.");
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Account with id {Id} not found for deletion.");
            }
            catch (CosmosException ex)
            {
                Console.WriteLine($"CosmosException: {ex.StatusCode}, {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }


    }
}