using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using FadeFactory_Accounts.Models;
using FadeFactory_Accounts.Services;
using FadeFactory_Accounts.Managers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

DotNetEnv.Env.Load(); //Added

var configuration = builder.Configuration; //Added

// Add services to the container.
string cosmosDbEndpoint = Environment.GetEnvironmentVariable("cosmosDbEndpoint") ?? throw new ArgumentNullException();
string cosmosDbKey = Environment.GetEnvironmentVariable("cosmosDbKey") ?? throw new ArgumentNullException();
string cosmosDbName = Environment.GetEnvironmentVariable("cosmosDbName") ?? throw new ArgumentNullException();
builder.Services.AddDbContext<AccountDbContext>(options => options.UseCosmos(cosmosDbEndpoint, cosmosDbKey, cosmosDbName));

//added
builder.Services.AddSingleton((provider) =>
{
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

builder.Services.AddScoped<IAccountService, AccountDbManager>(); //added

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
