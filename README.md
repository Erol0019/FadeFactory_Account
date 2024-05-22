# Introduction

This reposityry is 1 of 5 microservices in the organization "FadeFactory," which is a barber shop project for the "Development of Large Systems" (DLS) 2024 exam

# Continouse deployment

Github workflow automatically builds and deploys when the main branch is changed

# Hosting and observability

A Windows Web App is running at: https://fadefactoryaccounts.azurewebsites.net/swagger/index.html
The project is a REST API with swagger documentation

# Secrets

Secrets are stored in the Azure deployment

# Local development

Set up a local .env file following the env-template file. The project uses Azure CosmosDB with Entity Framework.
Type "Dotnet run" in your terminal
