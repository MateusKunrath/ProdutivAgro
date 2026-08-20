# ProdutivAgro

ProdutivAgro is a REST API for product and sales management in agricultural organizations. Each account belongs to one organization, and product and sales data is isolated by organization.

The project follows **Clean Architecture** and **DDD** principles: the domain contains business rules and contracts; the application layer implements use cases and validation; infrastructure provides persistence, authentication, and technical integrations; and the API exposes documented HTTP endpoints through Swagger.

## Features

- User registration with initial organization creation.
- JWT authentication, refresh-token renewal and invalidation, and password changes.
- Current-organization lookup and responsibility transfer to another user.
- Product CRUD with unit price and measurement unit.
- Sales lifecycle with **Draft**, **Completed**, and **Cancelled** statuses.
- Draft-sale creation; item addition, quantity updates, and item removal.
- Paginated and detailed sales queries, including item and total information.
- Data isolation for the authenticated user's organization.
- Request validation with FluentValidation, centralized error handling, and interactive Swagger documentation.
- Automatic execution of pending Entity Framework Core migrations at API startup.
- Unit and integration tests.

## Domain definitions

| Term | Definition |
| --- | --- |
| Organization | The context that owns users, products, and sales. Data is not shared between organizations. |
| Responsible user | The user responsible for the organization, who can transfer that responsibility to another user. |
| Product | A sellable item identified by description, unit price, and measurement unit. |
| Draft sale | A newly created sale whose items can still be changed. |
| Completed sale | A sale finalized from the draft status. |
| Cancelled sale | A sale marked as cancelled; it cannot be cancelled again. |
| Sale total | The sum of item totals, calculated as `quantity × unit price`. |

Supported measurement units are `Kilogram`, `Unit`, `Box`, and `Tray`.

## Built with

![.NET 8](https://img.shields.io/badge/.NET%208-512BD4?logo=dotnet&logoColor=white&style=for-the-badge)
![C Sharp](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white&style=for-the-badge)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?logo=postgresql&logoColor=white&style=for-the-badge)
![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework%20Core-512BD4?logo=dotnet&logoColor=white&style=for-the-badge)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=black&style=for-the-badge)
![xUnit](https://img.shields.io/badge/xUnit-5E2B97?style=for-the-badge)

## Getting started

### Prerequisites

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0) or later.
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (recommended) or a local PostgreSQL instance.

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

3. Review the development configuration in `src/ProdutivAgro.Api/appsettings.Development.json`. The default configuration uses:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=ProdutivAgroDb;Username=postgres;Password=@Password123"
     }
   }
   ```

   In production, provide the database password and JWT signing key through environment variables or User Secrets. Do not keep secrets in version-controlled files.

4. Restore packages and run the API:

   ```sh
   dotnet restore ProdutivAgro.slnx
   dotnet run --project src/ProdutivAgro.Api/ProdutivAgro.Api.csproj
   ```

   Pending migrations are applied automatically at startup.

5. Open Swagger at [https://localhost:7149/swagger](https://localhost:7149/swagger). The HTTP profile uses `http://localhost:5041/swagger`.

## Authentication

Protected endpoints require a JWT access token:

```text
Authorization: Bearer {accessToken}
```

In Swagger, click **Authorize** and enter `Bearer {accessToken}` after logging in.

## Main endpoints

### Authentication

| Method | Route | Description |
| --- | --- | --- |
| `POST` | `/api/Auth/Register` | Creates a user and its initial organization. |
| `POST` | `/api/Auth/Login` | Authenticates a user and returns tokens. |
| `POST` | `/api/Auth/RefreshAccessToken` | Generates a new access token from a refresh token. |
| `POST` | `/api/Auth/Logout` | Invalidates the provided refresh token. |
| `POST` | `/api/Auth/ChangePassword` | Changes the authenticated user's password. |

Registration request example:

```json
{
  "name": "John Smith",
  "email": "john@goodharvestfarm.com",
  "password": "Password@123",
  "organizationName": "Good Harvest Farm"
}
```

### Organizations

| Method | Route | Description |
| --- | --- | --- |
| `GET` | `/api/Organizations/Current` | Returns the authenticated user's organization. |
| `PUT` | `/api/Organizations/ChangeResponsible` | Transfers organization responsibility. |

### Products

| Method | Route | Description |
| --- | --- | --- |
| `POST` | `/api/Products` | Creates a product. |
| `GET` | `/api/Products?pageNumber=1&pageSize=20` | Lists products with pagination. |
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

### Sales

| Method | Route | Description |
| --- | --- | --- |
| `POST` | `/api/Sales` | Creates a draft sale. |
| `POST` | `/api/Sales/{id}/Items` | Adds one or more items to a sale. |
| `PATCH` | `/api/Sales/{id}/Items/{saleItemId}` | Updates an item's quantity. |
| `DELETE` | `/api/Sales/{id}/Items/{saleItemId}` | Removes an item from a sale. |
| `POST` | `/api/Sales/{id}/Complete` | Completes a sale. |
| `POST` | `/api/Sales/{id}/Cancel` | Cancels a sale. |
| `GET` | `/api/Sales?pageNumber=1&pageSize=20` | Lists organization sales with pagination. |
| `GET` | `/api/Sales/{id}` | Retrieves a sale, its items, and totals. |

Create the sale first:

```json
{
  "soldAt": "2026-08-19T10:00:00-03:00"
}
```

Then add items using the returned sale identifier:

```json
[
  {
    "productId": "00000000-0000-0000-0000-000000000000",
    "quantity": 10.5
  }
]
```

## Database and migrations

Migrations are stored in `src/ProdutivAgro.Infrastructure/Migrations`. To create a migration, install the EF Core CLI tool if needed:

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

## Project structure

```text
src/
  ProdutivAgro.Api/            # Controllers, HTTP authentication, filters, and Swagger
  ProdutivAgro.Application/    # Use cases, validations, and behaviors
  ProdutivAgro.Domain/         # Entities, enums, and repository contracts
  ProdutivAgro.Exception/      # Exceptions and error messages
  ProdutivAgro.Infrastructure/ # EF Core, DbContext, migrations, repositories, and JWT
  ProdutivAgro.SharedKernel/   # Shared domain types

tests/
  ProdutivAgro.Application.UnitTests/ # Use case and validator tests
  ProdutivAgro.Api.IntegrationTests/  # API integration tests
  ProdutivAgro.Testing.Common/         # Test builders and utilities
```
