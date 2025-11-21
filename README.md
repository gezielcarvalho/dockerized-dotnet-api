# Dockerized .NET API - Cloud Customers

This project is a .NET 8 Web API built with Test Driven Development (TDD) and Docker containerization. The CloudCustomers API demonstrates best practices in modern API development, including unit testing with xUnit, dependency injection, and containerized deployment.

## Table of Contents

- [Dockerized .NET API - Cloud Customers](#dockerized-net-api---cloud-customers)
  - [Table of Contents](#table-of-contents)
  - [Overview](#overview)
  - [Features](#features)
  - [Technologies Used](#technologies-used)
  - [Prerequisites](#prerequisites)
  - [Getting Started](#getting-started)
    - [Cloning the Repository](#cloning-the-repository)
    - [Configuration](#configuration)
    - [Running in Visual Studio Code](#running-in-visual-studio-code)
    - [Building the Solution Manually](#building-the-solution-manually)
  - [Running Tests](#running-tests)
  - [Docker Deployment](#docker-deployment)
    - [Using Docker](#using-docker)
    - [Using Docker Compose](#using-docker-compose)
  - [API Endpoints](#api-endpoints)
    - [Get Users](#get-users)
    - [Swagger Documentation](#swagger-documentation)
  - [Health Check](#health-check)
  - [Project Structure](#project-structure)
  - [License](#license)

## Overview

The CloudCustomers API is a RESTful service that fetches and manages user data from an external API. It showcases:

- Clean architecture with separation of concerns
- Service layer abstraction with `IUsersService`
- Dependency injection and configuration management
- Comprehensive unit testing with mocked HTTP clients
- Docker containerization with multi-stage builds

## Features

- .NET 8 Web API with ASP.NET Core
- Test Driven Development using xUnit
- Dependency injection and inversion of control
- SOLID design principles
- RESTful API with HTTP controllers
- Docker and Docker Compose support
- Health check endpoint for monitoring
- Swagger/OpenAPI documentation
- Configuration-based external API integration

## Technologies Used

- .NET 8
- ASP.NET Core Web API
- xUnit & FluentAssertions
- Moq for mocking
- HttpClient for external API calls
- Newtonsoft.Json for serialization
- Docker & Docker Compose
- Swagger/OpenAPI

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)
- [Visual Studio 2022 or Visual Studio Code](https://visualstudio.microsoft.com/)

## Getting Started

### Cloning the Repository

```bash
git clone https://github.com/gezielcarvalho/dockerized-dotnet-api.git
cd dockerized-dotnet-api
```

### Configuration

The API requires configuration for the external users API endpoint. Update `appsettings.Development.json`:

```json
{
  "UsersApiOptions": {
    "Endpoint": "https://jsonplaceholder.typicode.com/users"
  }
}
```

This configures the API to fetch user data from JSONPlaceholder, a free fake REST API for testing and prototyping.

### Running in Visual Studio Code

The project includes VS Code debug configurations for easy development:

1. Open the project in VS Code
2. Press `F5` or go to Run > Start Debugging
3. Select `.NET Core Launch (web)` from the debug configurations
4. The API will build, launch, and automatically open Swagger UI in your browser

Available debug configurations:

- **.NET Core Launch (web)** - Run locally with debugger
- **.NET Core Attach** - Attach to running process
- **Docker .NET Launch** - Run in Docker with debugging

### Building the Solution Manually

1. Restore dependencies:

   ```bash
   dotnet restore
   ```

2. Build the solution:

   ```bash
   dotnet build
   ```

3. Run the API locally:
   ```bash
   dotnet run --project CloudCustomers.API
   ```

The API will be available at:

- HTTPS: `https://localhost:7251`
- HTTP: `http://localhost:5018`

## Running Tests

To run all unit tests:

```bash
dotnet test
```

To run tests with detailed output:

```bash
dotnet test --verbosity normal
```

## Docker Deployment

### Using Docker

1. **Build the Docker image:**

   ```bash
   docker build -t cloud-customers-api:latest .
   ```

2. **Run a Docker container:**
   ```bash
   docker run -d -p 5000:5000 -p 5001:5001 --name cloud-customers cloud-customers-api:latest
   ```

### Using Docker Compose

1. **Start the application:**

   ```bash
   docker-compose up -d
   ```

2. **Stop the application:**

   ```bash
   docker-compose down
   ```

3. **View logs:**
   ```bash
   docker-compose logs -f
   ```

The containerized API will be available at `http://localhost:5000`.

## API Endpoints

### Get Users

- **Endpoint:** `GET /Users/GetUsers`
- **Description:** Retrieves a list of users from the configured external API (JSONPlaceholder)
- **Response:**
  - `200 OK` - Returns list of users with id, name, username, email, address, phone, website, and company information
  - `404 Not Found` - No users found

Example response:

```json
[
  {
    "id": 1,
    "name": "Leanne Graham",
    "username": "Bret",
    "email": "Sincere@april.biz",
    "address": {
      "street": "Kulas Light",
      "suite": "Apt. 556",
      "city": "Gwenborough",
      "zipcode": "92998-3874",
      "geo": { "lat": "-37.3159", "lng": "81.1496" }
    },
    "phone": "1-770-736-8031 x56442",
    "website": "hildegard.org",
    "company": {
      "name": "Romaguera-Crona",
      "catchPhrase": "Multi-layered client-server neural-net",
      "bs": "harness real-time e-markets"
    }
  }
]
```

### Swagger Documentation

Access the interactive API documentation at:

- **Development (local):** `https://localhost:7251/swagger`
- **HTTP:** `http://localhost:5018/swagger`

## Health Check

The API includes a health check endpoint for monitoring and container orchestration.

**Endpoint:** `GET /health`

Test the health check:

```bash
curl https://localhost:7251/health
# or
curl http://localhost:5018/health
```

## Project Structure

```
dockerized-dotnet-api/
├── .vscode/                          # VS Code debug and task configurations
│   ├── launch.json                   # Debug configurations
│   └── tasks.json                    # Build, test, and Docker tasks
├── CloudCustomers.API/               # Main API project
│   ├── Config/
│   │   └── UsersApiOptions.cs        # Configuration model for external API
│   ├── Controllers/
│   │   └── UsersController.cs        # API controller
│   ├── Models/
│   │   ├── User.cs                   # User model
│   │   ├── Address.cs                # Address model
│   │   ├── Company.cs                # Company model
│   │   └── Geo.cs                    # Geo-location model
│   ├── Services/
│   │   └── UsersService.cs           # Business logic for user operations
│   ├── appsettings.Development.json  # Development configuration
│   └── Program.cs                    # Application entry point
├── CloudCustomers.UnitTests/         # Unit test project
│   ├── Fixtures/
│   │   └── UsersFixture.cs           # Test data fixtures
│   ├── Helpers/
│   │   └── MockHttpMessageHandler.cs # HTTP mocking utilities
│   └── Systems/
│       ├── Controllers/
│       │   └── TestUsersController.cs
│       └── Services/
│           └── TestUsersService.cs
├── Dockerfile                        # Multi-stage Docker build
├── docker-compose.yaml               # Docker Compose configuration
└── README.md                         # This file
```

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.
