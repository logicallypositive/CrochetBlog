

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

### Crochet.Contracts

- Contains 


### Create

To Create the project structure

-- Setup Workspace
dotnet new gitignore
git init
dotnet new sln

-- Creating project
dotnet new webapi -controllers -o Crochet.Api
dotnet new classlib -o Crochet.Contracts
dotnet new classlib -o Crochet.Application

-- Setting up project
dotnet sln add ./Crochet.Api ./Crochet.Application ./Crochet.Contracts
dotnet add Crochet.Api reference ./Crochet.Application ./Crochet.Contracts
dotnet add Crochet.Application package Microsoft.Extensions.DependencyInjection.Abstractions --version 8.0.0

-- Cleaning Projects
rm Crochet.Contracts/Class1.cs
rm Crochet.Application/Class1.cs
rm Crochet.Api/WeatherForecast.cs 
rm Crochet.Api/Controllers/WeatherForecastController.cs 
rm Crochet.Api/Crochet.Api.http

-- Contracts stuff
mkdir Crochet.Contracts/Requests
touch Crochet.Contracts/Requests/CreateProjectRequest.cs
touch Crochet.Contracts/Requests/UpdateProjectRequest.cs
mkdir Crochet.Contracts/Responses
touch Crochet.Contracts/Responses/ProjectResponse.cs
touch Crochet.Contracts/Responses/ProjectsResponse.cs

-- Application stuff
touch Crochet.Application/ApplicationServiceCollectionExtensions.cs
mkdir Crochet.Application/Models
touch Crochet.Application/Models/Project.cs
mkdir Crochet.Application/Repositories
touch Crochet.Application/Repositories/IProjectRepository.cs
touch Crochet.Application/Repositories/ProjectRepository.cs

-- API stuff
touch Crochet.Api/Controllers/ProjectController.cs
touch Crochet.Api/ApiEndpoints.cs
mkdir Crochet.Api/Mapping
touch Crochet.Api/Mapping/ContractMapping.cs
