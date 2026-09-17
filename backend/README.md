# ProdutivAgro Backend

[Português](#português) | [English](#english)

## Português
API REST para organizações agrícolas, produtos, vendas, autenticação e convites. O projeto é uma solução .NET 8 organizada em Clean Architecture e DDD, usando Entity Framework Core e PostgreSQL.

### Tecnologias

- .NET 8 e ASP.NET Core Web API
- C#
- Entity Framework Core com Npgsql/PostgreSQL
- MediatR e FluentValidation
- JWT transportado em cookies HTTP-only
- Swagger, xUnit e FluentAssertions

### Estrutura

```text
backend/
├── src/
│   ├── ProdutivAgro.Api/            # Controllers, HTTP, cookies, filtros e Swagger
│   ├── ProdutivAgro.Application/    # Casos de uso, validações e comportamentos
│   ├── ProdutivAgro.Domain/         # Entidades, enums e contratos de repositório
│   ├── ProdutivAgro.Exception/      # Exceções e mensagens de erro
│   └── ProdutivAgro.Infrastructure/ # EF Core, DbContext, migrações, repositórios e JWT
├── tests/
│   ├── ProdutivAgro.Application.UnitTests/
│   ├── ProdutivAgro.Api.IntegrationTests/
│   └── ProdutivAgro.Testing.Common/
├── ProdutivAgro.slnx
└── docker-compose.yml
```

### Pré-requisitos

- .NET SDK 8.0 ou superior.
- Docker Desktop, ou PostgreSQL disponível localmente.
- A ferramenta `dotnet-ef` somente para criar ou aplicar migrações manualmente.

Todos os comandos abaixo devem ser executados a partir de `backend/`, salvo quando indicado de outra forma.

### Banco de dados local

O `docker-compose.yml` sobe um contêiner PostgreSQL chamado `produtivagro-postgres`, publica a porta `5432` e cria o banco configurado para desenvolvimento.

```sh
docker compose up -d
```

Para parar e remover o contêiner criado pelo Compose:

```sh
docker compose down
```

### Configuração

O ambiente de desenvolvimento é configurado em `src/ProdutivAgro.Api/appsettings.Development.json`. As chaves usadas pela aplicação são:

| Chave de configuração | Finalidade |
| --- | --- |
| `ConnectionStrings:DefaultConnection` | Conexão com PostgreSQL. |
| `Settings:Jwt:SigningKey` | Chave de assinatura dos tokens JWT. |
| `Settings:Jwt:ExpiresMinutes` | Duração do token de acesso. |
| `Settings:RefreshToken:ExpiresDays` | Duração do refresh token. |
| `Settings:Invitation:AcceptanceUrl` | URL usada na criação de convites. |
| `Settings:Invitation:ExpiresDays` | Duração dos convites. |

O ASP.NET Core também aceita essas chaves como variáveis de ambiente, substituindo `:` por `__`; por exemplo, `ConnectionStrings__DefaultConnection` e `Settings__Jwt__SigningKey`. Não existe arquivo `.env` ou exemplo versionado para o backend.

As configurações de desenvolvimento atuais já contêm valores locais. Para outros ambientes, forneça valores adequados fora do controle de versão, especialmente para conexão e chave JWT.

### Executar

Restaure os pacotes e inicie a API:

```sh
dotnet restore ProdutivAgro.slnx
dotnet run --project src/ProdutivAgro.Api/ProdutivAgro.Api.csproj
```

As migrações pendentes são aplicadas automaticamente na inicialização, exceto no ambiente de teste.

Os perfis atuais definem `ASPNETCORE_ENVIRONMENT=Development` e expõem a API em:

- perfil HTTP: `http://localhost:5000`
- perfil HTTPS: `https://localhost:5000` e `http://localhost:5041`

Com o perfil HTTPS, a interface Swagger está em `https://localhost:5000/swagger`.

### Autenticação e frontend

Os endpoints protegidos usam o cookie `access_token`; o cookie é HTTP-only e não é acessível ao JavaScript do navegador. A API também usa um cookie de refresh token.

O CORS de desenvolvimento está configurado apenas para `http://localhost:5173`, permitindo cabeçalhos, métodos e credenciais. O frontend deve apontar `VITE_API_URL` para a base da API e executar nessa origem para que a autenticação por cookies funcione na configuração atual.

### Endpoints principais

| Área | Rotas disponíveis |
| --- | --- |
| Autenticação | `POST /api/Auth/Login`, `POST /api/Auth/Register`, `POST /api/Auth/RefreshAccessToken`, `POST /api/Auth/Logout` |
| Usuário | `GET /api/Users/Current`, `POST /api/Users/ChangePassword` |
| Organização | `GET /api/Organizations/Current`, `PUT /api/Organizations/ChangeResponsible`, `POST` e `GET /api/Organizations/Invitations` |
| Produtos | `POST`, `GET /api/Products`; `GET`, `PUT` e `DELETE /api/Products/{id}` |
| Vendas | criação, consulta, itens, conclusão, cancelamento, reabertura e reversão sob `/api/Sales` |

Os endpoints de convites e as operações administrativas de reabrir ou desfazer cancelamento de venda exigem a função `Administrator`.

### Testes e build

Execute todos os testes:

```sh
dotnet test ProdutivAgro.slnx
```

Execute somente os testes unitários da aplicação:

```sh
dotnet test tests/ProdutivAgro.Application.UnitTests/ProdutivAgro.Application.UnitTests.csproj
```

Build da solução:

```sh
dotnet build ProdutivAgro.slnx
```

### Migrações

As migrações ficam em `src/ProdutivAgro.Infrastructure/Migrations`.

Instale a CLI do Entity Framework Core, se ela ainda não estiver disponível:

```sh
dotnet tool install --global dotnet-ef
```

Crie uma migração:

```sh
dotnet ef migrations add MigrationName --project src/ProdutivAgro.Infrastructure/ProdutivAgro.Infrastructure.csproj --startup-project src/ProdutivAgro.Api/ProdutivAgro.Api.csproj --output-dir Migrations
```

Atualize o banco manualmente, sem iniciar a API:

```sh
dotnet ef database update --project src/ProdutivAgro.Infrastructure/ProdutivAgro.Infrastructure.csproj --startup-project src/ProdutivAgro.Api/ProdutivAgro.Api.csproj
```

Não edite migrações que já tenham sido compartilhadas ou aplicadas em outros ambientes; crie uma nova migração para cada alteração de esquema.
## English

### Overview

REST API for agricultural organizations, products, sales, authentication, and invitations. It is a .NET 8 solution organized with Clean Architecture and DDD, using Entity Framework Core and PostgreSQL.

### Technologies

- .NET 8 and ASP.NET Core Web API
- C#
- Entity Framework Core with Npgsql/PostgreSQL
- MediatR and FluentValidation
- JWT transported in HTTP-only cookies
- Swagger, xUnit, and FluentAssertions

### Structure

```text
backend/
├── src/
│   ├── ProdutivAgro.Api/            # Controllers, HTTP, cookies, filters, and Swagger
│   ├── ProdutivAgro.Application/    # Use cases, validation, and behaviors
│   ├── ProdutivAgro.Domain/         # Entities, enums, and repository contracts
│   ├── ProdutivAgro.Exception/      # Exceptions and error messages
│   └── ProdutivAgro.Infrastructure/ # EF Core, DbContext, migrations, repositories, and JWT
├── tests/
│   ├── ProdutivAgro.Application.UnitTests/
│   ├── ProdutivAgro.Api.IntegrationTests/
│   └── ProdutivAgro.Testing.Common/
├── ProdutivAgro.slnx
└── docker-compose.yml
```

### Prerequisites

- .NET SDK 8.0 or later.
- Docker Desktop or a local PostgreSQL instance.
- `dotnet-ef` only when creating or applying migrations manually.

Run every command below from `backend/`, unless noted otherwise.

### Local database

`docker-compose.yml` starts a PostgreSQL container named `produtivagro-postgres`, exposes port `5432`, and creates the database configured for development.

```sh
docker compose up -d
```

Stop and remove the Compose container:

```sh
docker compose down
```

### Configuration

Development settings are in `src/ProdutivAgro.Api/appsettings.Development.json`.

| Configuration key | Purpose |
| --- | --- |
| `ConnectionStrings:DefaultConnection` | PostgreSQL connection. |
| `Settings:Jwt:SigningKey` | JWT signing key. |
| `Settings:Jwt:ExpiresMinutes` | Access-token lifetime. |
| `Settings:RefreshToken:ExpiresDays` | Refresh-token lifetime. |
| `Settings:Invitation:AcceptanceUrl` | URL used when creating invitations. |
| `Settings:Invitation:ExpiresDays` | Invitation lifetime. |

ASP.NET Core also accepts these keys as environment variables by replacing `:` with `__`, such as `ConnectionStrings__DefaultConnection` and `Settings__Jwt__SigningKey`. The backend has no versioned `.env` file or example.

The current development file already contains local values. Provide suitable values outside version control for other environments, especially database connection and JWT signing key.

### Run

Restore packages and start the API:

```sh
dotnet restore ProdutivAgro.slnx
dotnet run --project src/ProdutivAgro.Api/ProdutivAgro.Api.csproj
```

Pending migrations run automatically during startup, except in the test environment.

The current profiles set `ASPNETCORE_ENVIRONMENT=Development` and expose:

- HTTP profile: `http://localhost:5000`
- HTTPS profile: `https://localhost:5000` and `http://localhost:5041`

With the HTTPS profile, Swagger is available at `https://localhost:5000/swagger`.

### Authentication and frontend

Protected endpoints use the HTTP-only `access_token` cookie; the browser cannot read it from JavaScript. The API also uses a refresh-token cookie.

Development CORS allows only `http://localhost:5173`, headers, methods, and credentials. The frontend must point `VITE_API_URL` to the API base URL and run from that origin for cookie authentication to work with the current configuration.

### Main endpoints

| Area | Available routes |
| --- | --- |
| Authentication | `POST /api/Auth/Login`, `POST /api/Auth/Register`, `POST /api/Auth/RefreshAccessToken`, `POST /api/Auth/Logout` |
| User | `GET /api/Users/Current`, `POST /api/Users/ChangePassword` |
| Organization | `GET /api/Organizations/Current`, `PUT /api/Organizations/ChangeResponsible`, `POST` and `GET /api/Organizations/Invitations` |
| Products | `POST`, `GET /api/Products`; `GET`, `PUT`, and `DELETE /api/Products/{id}` |
| Sales | creation, queries, item management, completion, cancellation, reopening, and undoing cancellation under `/api/Sales` |

Invitation endpoints and administrative sale operations require the `Administrator` role.

### Tests and build

Run all tests:

```sh
dotnet test ProdutivAgro.slnx
```

Run application unit tests only:

```sh
dotnet test tests/ProdutivAgro.Application.UnitTests/ProdutivAgro.Application.UnitTests.csproj
```

Build the solution:

```sh
dotnet build ProdutivAgro.slnx
```

### Migrations

Migrations are in `src/ProdutivAgro.Infrastructure/Migrations`.

Install the Entity Framework Core CLI if it is not already available:

```sh
dotnet tool install --global dotnet-ef
```

Create a migration:

```sh
dotnet ef migrations add MigrationName --project src/ProdutivAgro.Infrastructure/ProdutivAgro.Infrastructure.csproj --startup-project src/ProdutivAgro.Api/ProdutivAgro.Api.csproj --output-dir Migrations
```

Update the database without starting the API:

```sh
dotnet ef database update --project src/ProdutivAgro.Infrastructure/ProdutivAgro.Infrastructure.csproj --startup-project src/ProdutivAgro.Api/ProdutivAgro.Api.csproj
```

Do not edit migrations already shared or applied in other environments; create a new migration for every schema change.