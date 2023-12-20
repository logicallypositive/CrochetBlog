

## Architecture

Crochet.Api
- Responsible for things API specific
- Authentication/Authorization
- Documentation
- HealthCheck endpoints

Crochet.Application
- Agnostic of API 
- Contains business logic

Crochet.Contracts
- Api contracts
- Can publish as a nuget package
- Useful for anyone with anyone who wants to use the api
- Always in sync with exposed endpoints


### Create

To Create the post structure

-- Setup Workspace
dotnet new gitignore
git init
dotnet new sln

-- Creating post
dotnet new webapi -controllers -o Crochet.Api
dotnet new classlib -o Crochet.Contracts
dotnet new classlib -o Crochet.Application

-- Setting up post
dotnet sln add ./Crochet.Api ./Crochet.Application ./Crochet.Contracts
dotnet add Crochet.Api reference ./Crochet.Application ./Crochet.Contracts
dotnet add Crochet.Application package Microsoft.Extensions.DependencyInjection.Abstractions --version 8.0.0
dotnet add Crochet.Application package Npgsql --version 8.0.1
dotnet add Crochet.Application package Dapper --version 2.1.24

-- Cleaning Posts
rm Crochet.Contracts/Class1.cs
rm Crochet.Application/Class1.cs
rm Crochet.Api/WeatherForecast.cs 
rm Crochet.Api/Controllers/WeatherForecastController.cs 
rm Crochet.Api/Crochet.Api.http

-- Contracts stuff
mkdir Crochet.Contracts/Requests
touch Crochet.Contracts/Requests/CreatePostRequest.cs
touch Crochet.Contracts/Requests/UpdatePostRequest.cs
mkdir Crochet.Contracts/Responses
touch Crochet.Contracts/Responses/PostResponse.cs
touch Crochet.Contracts/Responses/PostsResponse.cs

-- Application stuff
touch Crochet.Application/ApplicationServiceCollectionExtensions.cs
mkdir Crochet.Application/Models
touch Crochet.Application/Models/Post.cs
mkdir Crochet.Application/Repositories
touch Crochet.Application/Repositories/IPostRepository.cs
touch Crochet.Application/Repositories/PostRepository.cs
mkdir Crochet.Application/Database/
touch Crochet.Application/Database/DbConnectionFactory.cs
touch Crochet.Application/Database/DbInitializer.cs

-- API stuff
touch Crochet.Api/Controllers/PostController.cs
touch Crochet.Api/ApiEndpoints.cs
mkdir Crochet.Api/Mapping
touch Crochet.Api/Mapping/ContractMapping.cs

## Connecting to postgres

- psql -h localhost -p 5432 -U your_username -d postgres
