using FadeFactory_Accounts.Controllers;
using System.Configuration;
using FadeFactory_Accounts.Data;
using Microsoft.Azure.Cosmos;
using System.Net;
using System.Collections;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

DotNetEnv.Env.Load(); //Added

var configuration = builder.Configuration; //Added

// Add services to the container.

//added
builder.Services.AddSingleton((provider) =>
{
    var cosmosDbEndpoint = Environment.GetEnvironmentVariable("cosmosDbEndpoint");
    var cosmosDbKey = Environment.GetEnvironmentVariable("cosmosDbKey");
    var cosmosDbName = Environment.GetEnvironmentVariable("cosmosDbName");
    if (string.IsNullOrEmpty(cosmosDbEndpoint) || string.IsNullOrEmpty(cosmosDbKey) || string.IsNullOrEmpty(cosmosDbName))
    {
        throw new ArgumentNullException("Cosmos DB configuration is missing.");
    }

    // var cosmosDbEndpoint = configuration["cosmosDbSettings:cosmosDbEndpoint"];
    // var cosmosDbKey = configuration["cosmosDbSettings:cosmosDbKey"];
    // var cosmosDbName = configuration["cosmosDbSettings:cosmosDbName"];

    var cosmosClientOptions = new CosmosClientOptions
    {
        ApplicationName = cosmosDbName
    };

    var loggerFactory = LoggerFactory.Create(builder =>
    {
        builder.AddConsole();
    });

    var cosmosClient = new CosmosClient(cosmosDbEndpoint, cosmosDbKey, cosmosClientOptions);

    cosmosClient.ClientOptions.ConnectionMode = ConnectionMode.Gateway;

    return cosmosClient;
});
//end
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAccountRepository, AccountRepository>(); //added

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
