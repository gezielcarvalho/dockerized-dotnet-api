# .NET 8 Web API with TDD and Docker

This project demonstrates the development of a .NET 8 Web API using Test Driven Development (TDD) and Docker. The application is built from scratch, tested, and containerized for easy deployment and management across various environments, including on-premises and cloud platforms.

## Table of Contents
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Running Tests](#running-tests)
- [Dockerizing the Application](#dockerizing-the-application)
- [Health Check](#health-check)
- [License](#license)

## Features
- .NET 8 Web API development with TDD
- Unit testing using xUnit
- Dependency injection and inversion of control
- SOLID design principles
- REST API development
- Docker containerization
- Health check endpoint

## Technologies Used
- .NET 8
- ASP.NET Core Web API
- xUnit
- HttpClient
- Docker

## Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)
- [Visual Studio or Visual Studio Code](https://visualstudio.microsoft.com/)

## Getting Started
### Cloning the Repository
```bash
git clone https://github.com/yourusername/dotnet8-webapi-tdd-docker.git
cd dotnet8-webapi-tdd-docker
```

### Setting Up the Solution
1. Create a new solution:
    ```bash
    dotnet new sln -n MySolution
    ```

2. Create and add a new Web API project:
    ```bash
    dotnet new webapi -n MyWebApi
    dotnet sln add MyWebApi/MyWebApi.csproj
    ```

3. Create and add a new xUnit test project:
    ```bash
    dotnet new xunit -n MyWebApi.Tests
    dotnet sln add MyWebApi.Tests/MyWebApi.Tests.csproj
    ```

4. Add a project reference from the test project to the Web API project:
    ```bash
    dotnet add MyWebApi.Tests/MyWebApi.Tests.csproj reference MyWebApi/MyWebApi.csproj
    ```

## Running Tests
To run the unit tests:
```bash
dotnet test
```

## Dockerizing the Application
1. **Create a Dockerfile:**
    ```Dockerfile
    # Use the official .NET 8 SDK image for building the app
    FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
    WORKDIR /app

    # Copy the project files and restore dependencies
    COPY *.csproj .
    RUN dotnet restore

    # Copy the remaining files and build the app
    COPY . .
    RUN dotnet publish -c Release -o out

    # Use the official .NET runtime image for running the app
    FROM mcr.microsoft.com/dotnet/aspnet:8.0
    WORKDIR /app
    COPY --from=build /app/out .

    # Set the entry point for the container
    ENTRYPOINT ["dotnet", "MyWebApi.dll"]
    ```

2. **Build the Docker image:**
    ```bash
    docker build -t mywebapi:latest .
    ```

3. **Run a Docker container:**
    ```bash
    docker run -d -p 8080:80 --name mywebapi_container mywebapi:latest
    ```

4. **Test locally in the browser:**
   Navigate to `http://localhost:8080` to access the API.

## Health Check
1. **Add a health check endpoint in `Startup.cs` or `Program.cs`:**
    ```csharp
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapHealthChecks("/health");
    });
    ```

2. **Rebuild the Docker image:**
    ```bash
    docker build -t mywebapi:latest .
    ```

3. **Test the health check endpoint in the browser:**
   Navigate to `http://localhost:8080/health` to verify the health check.

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.
