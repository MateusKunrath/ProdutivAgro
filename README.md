# ProdutivAgro

## About the project

**ProdutivAgro** is a REST API designed to support product and sales management for organizations in the agricultural sector. The application allows users to create an account connected to an organization, authenticate, manage products, and register sales containing one or more items.

The project is organized in layers inspired by **Clean Architecture** and **DDD**. The domain layer contains entities and contracts; the application layer implements use cases and validations; the infrastructure layer provides persistence, authentication, and technical integrations; and the API layer exposes documented HTTP endpoints through Swagger.

### Features

- User registration, login, token refresh, logout, and password changes.
- JWT authentication and authorization with refresh tokens.
- Organization creation during the first user registration.
- Current organization lookup and organization responsibility transfer.
- Product CRUD with unit price and measurement unit.
- Sales registration and paginated listing.
- Data isolation by the authenticated user's organization.
- Request validation with FluentValidation.
- Automatic Entity Framework Core migration execution when the API starts.
- Interactive API documentation and testing with Swagger.
- Unit and integration tests with xUnit and Shouldly.

### Built with

![.NET 8](https://img.shields.io/badge/.NET%208-512BD4?logo=dotnet&logoColor=white&style=for-the-badge)
![C Sharp](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white&style=for-the-badge)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?logo=postgresql&logoColor=white&style=for-the-badge)
![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework%20Core-512BD4?logo=dotnet&logoColor=white&style=for-the-badge)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=black&style=for-the-badge)
![xUnit](https://img.shields.io/badge/xUnit-5E2B97?style=for-the-badge)

## Getting started

### Prerequisites

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0) or later.
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (recommended to run PostgreSQL) or a local PostgreSQL instance.
- Visual Studio, Rider, VS Code, or another .NET-compatible IDE.

### Installation and execution

1. Clone the repository and enter the project directory:

   ```sh
   git clone https://github.com/MateusKunrath/ProdutivAgro.git
   cd ProdutivAgro
   ```

2. Start PostgreSQL with Docker:

   ```sh
   docker compose up -d
   ```

   The `docker-compose.yml` file creates the `ProdutivAgroDb` database on port `5432`.

3. Review or update the development configuration in `src/ProdutivAgro.Api/appsettings.Development.json`:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=ProdutivAgroDb;Username=postgres;Password=your_password"
     },
     "Settings": {
       "Jwt": {
         "SigningKey": "a-secure-secret-key-with-an-appropriate-length",
         "ExpiresMinutes": 15
       },
       "RefreshToken": {
         "ExpiresDays": 30
       }
     }
   }
   ```

   If you use the unmodified `docker-compose.yml`, use the credentials configured in it. For real environments, keep database passwords and JWT keys outside version-controlled files, such as in User Secrets or environment variables.

4. Restore dependencies:

   ```sh
   dotnet restore ProdutivAgro.slnx
   ```

5. Run the API:

   ```sh
   dotnet run --project src/ProdutivAgro.Api/ProdutivAgro.Api.csproj
   ```

   When it starts, the API automatically applies any pending migrations to the configured database.

6. Open Swagger:

   ```text
   https://localhost:7149/swagger
   ```

   If you run another profile or port, use the URL displayed in the terminal. The default HTTP profile runs at `http://localhost:5041/swagger`.

## Database and migrations

Migrations are stored in `src/ProdutivAgro.Infrastructure/Migrations`. The application calls `Database.MigrateAsync()` on startup, so the existing migrations are applied as long as the database is available.

To create a migration after changing entities or `DbContext` configurations, install the Entity Framework Core CLI tool if needed:

```sh
dotnet tool install --global dotnet-ef
```

Then run the following command from the repository root:

```sh
dotnet ef migrations add MigrationName --project src/ProdutivAgro.Infrastructure/ProdutivAgro.Infrastructure.csproj --startup-project src/ProdutivAgro.Api/ProdutivAgro.Api.csproj --output-dir Migrations
```

To apply migrations manually without starting the API:

```sh
dotnet ef database update --project src/ProdutivAgro.Infrastructure/ProdutivAgro.Infrastructure.csproj --startup-project src/ProdutivAgro.Api/ProdutivAgro.Api.csproj
```

> Do not edit migrations that have already been shared or applied in other environments. Create a new migration for every schema change.

## Tests

Run all project tests with:

```sh
dotnet test ProdutivAgro.slnx
```

Or run a specific test project:

```sh
dotnet test tests/ProdutivAgro.Application.UnitTests/ProdutivAgro.Application.UnitTests.csproj
dotnet test tests/ProdutivAgro.Api.IntegrationTests/ProdutivAgro.Api.IntegrationTests.csproj
```

## Authentication

Protected endpoints require a JWT token in the request header:

```text
Authorization: Bearer {accessToken}
```

In Swagger, click **Authorize** and enter `Bearer {accessToken}` after logging in.

## Main endpoints

### Identity

| Method | Route | Description |
| --- | --- | --- |
| `POST` | `/api/Identity/Register` | Creates a user and its initial organization. |
| `POST` | `/api/Identity/Login` | Authenticates a user and returns tokens. |
| `POST` | `/api/Identity/RefreshAccessToken` | Generates a new access token from a refresh token. |
| `POST` | `/api/Identity/Logout` | Invalidates a refresh token. |
| `POST` | `/api/Identity/ChangePassword` | Changes the authenticated user's password. |

Registration request example:

```json
{
  "name": "John Smith",
  "email": "john@produtivagro.com",
  "password": "Password@123",
  "organizationName": "Good Harvest Farm"
}
```

### Organizations

| Method | Route | Description |
| --- | --- | --- |
| `GET` | `/api/Organizations/Current` | Returns the authenticated user's organization. |
| `PUT` | `/api/Organizations/ChangeResponsible` | Assigns another user as the organization owner. |

### Products

| Method | Route | Description |
| --- | --- | --- |
| `POST` | `/api/Products` | Creates a product. |
| `GET` | `/api/Products?pageNumber=1&pageSize=20` | Lists paginated products. |
| `GET` | `/api/Products/{id}` | Retrieves a product by identifier. |
| `PUT` | `/api/Products/{id}` | Updates a product. |
| `DELETE` | `/api/Products/{id}` | Deletes a product. |

Product request example:

```json
{
  "description": "Soybeans",
  "unitPrice": 132.5,
  "measurementUnit": "Kilogram"
}
```

Accepted measurement units: `Kilogram`, `Unit`, `Box`, and `Tray`.

### Sales

| Method | Route | Description |
| --- | --- | --- |
| `POST` | `/api/Sales` | Registers a sale with one or more items. |
| `GET` | `/api/Sales?pageNumber=1&pageSize=20` | Lists paginated sales for the organization. |

Sale request example:

```json
{
  "items": [
    {
      "productId": "00000000-0000-0000-0000-000000000000",
      "quantity": 10.5
    }
  ]
}
```

## Project structure

```text
src/
  ProdutivAgro.Api/            # Controllers, HTTP authentication, filters, and Swagger
  ProdutivAgro.Application/    # Use cases, validations, and behaviors
  ProdutivAgro.Communication/  # Request and response DTOs
  ProdutivAgro.Domain/         # Entities, enums, and repository contracts
  ProdutivAgro.Exception/      # Exceptions and error messages
  ProdutivAgro.Infrastructure/ # EF Core, DbContext, migrations, repositories, and JWT
  ProdutivAgro.SharedKernel/   # Shared domain types

tests/
  ProdutivAgro.Application.UnitTests/ # Use case and validator tests
  ProdutivAgro.Api.IntegrationTests/  # API integration tests
  ProdutivAgro.Testing.Common/         # Test builders and utilities
```
