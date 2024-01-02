# Crochet

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

## Create

### Setup Workspace

#### Create gitignore

dotnet new gitignore

#### Create git repo

git init

#### Create solution

dotnet new sln

#### Create projects

dotnet new webapi -controllers -o Crochet.Api
dotnet new classlib -o Crochet.Contracts
dotnet new classlib -o Crochet.Application

### Setting up projects

#### Add projects to solution

dotnet sln add ./Crochet.Api ./Crochet.Application ./Crochet.Contracts

#### Add refrences

dotnet add Crochet.Api reference ./Crochet.Application ./Crochet.Contracts

#### Add package dependencies

dotnet add Crochet.Application package Microsoft.Extensions.DependencyInjection.Abstractions --version 8.0.0
dotnet add Crochet.Application package Npgsql --version 8.0.1
dotnet add Crochet.Application package Dapper --version 2.1.24

### Cleaning Projects

rm Crochet.Contracts/Class1.cs
rm Crochet.Application/Class1.cs
rm Crochet.Api/WeatherForecast.cs
rm Crochet.Api/Controllers/WeatherForecastController.cs
rm Crochet.Api/Crochet.Api.http

### Contracts stuff

#### Requests

mkdir Crochet.Contracts/Requests
touch Crochet.Contracts/Requests/CreatePostRequest.cs
touch Crochet.Contracts/Requests/UpdatePostRequest.cs

#### Responses

mkdir Crochet.Contracts/Responses
touch Crochet.Contracts/Responses/PostResponse.cs
touch Crochet.Contracts/Responses/PostsResponse.cs
touch Crochet.Contracts/Responses/ValidationFailureResponse.cs

### Application stuff

#### Database

mkdir Crochet.Application/Database/
touch Crochet.Application/Database/DbConnectionFactory.cs
touch Crochet.Application/Database/DbInitializer.cs

#### Repositories

mkdir Crochet.Application/Repositories
touch Crochet.Application/Repositories/IPostRepository.cs
touch Crochet.Application/Repositories/PostRepository.cs

#### Services

mkdir Crochet.Application/Services
touch Crochet.Application/Services/IPostService.cs
touch Crochet.Application/Services/PostService.cs

#### Models

mkdir Crochet.Application/Models
touch Crochet.Application/Models/Post.cs

#### Validation

touch Crochet.Application/IApplicationMarker.cs
mkdir Crochet.Application/Validators
touch Crochet.Application/Validators/PostValidator.cs

#### ApplicationServiceCollectionExtensions

touch Crochet.Application/ApplicationServiceCollectionExtensions.cs

### API stuff

#### Controllers

touch Crochet.Api/Controllers/PostController.cs

#### ApiEndpoints

touch Crochet.Api/ApiEndpoints.cs

#### Mapping

mkdir Crochet.Api/Mapping
touch Crochet.Api/Mapping/ContractMapping.cs

## Postgres

### Running postgres via docker compose

docker-compose up -d

### Connecting to postgres

- psql -h localhost -p 5432 -U your_username -d postgres
