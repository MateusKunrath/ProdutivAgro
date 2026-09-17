# ProdutivAgro

[Português](#português) | [English](#english)

## Português
ProdutivAgro é uma aplicação para gestão de produtos e vendas em organizações agrícolas. O produto reúne uma API REST em .NET e uma aplicação web em Vue, mantidas neste mesmo repositório e evoluídas em conjunto.

### Arquitetura

```text
Browser
  └─ frontend/ (Vue 3 + TypeScript + Vite)
       └─ HTTP com cookies ──► backend/ (ASP.NET Core REST API)
                                  └─ PostgreSQL
```

O frontend usa `VITE_API_URL` como base das requisições e envia credenciais por cookie. No desenvolvimento, a API permite a origem `http://localhost:5173`.

O backend segue Clean Architecture e DDD: a API expõe endpoints HTTP; a camada Application concentra casos de uso e validações; Domain contém entidades e contratos; Infrastructure implementa persistência, autenticação e migrações.

### Estrutura do monorepo

```text
.
├── backend/                  # API .NET, solução, testes e Docker Compose do PostgreSQL
│   ├── src/
│   ├── tests/
│   ├── ProdutivAgro.slnx
│   └── README.md
├── frontend/                 # Aplicação Vue
│   ├── src/
│   ├── package.json
│   └── README.md
└── README.md
```

### Pré-requisitos

- .NET SDK 8.0 ou superior para o backend.
- Node.js para o frontend. O projeto não declara uma versão mínima de Node.js.
- pnpm `10.33.2`, versão indicada em `frontend/package.json`.
- Docker Desktop ou uma instalação local do PostgreSQL para executar o banco do backend.

### Início rápido

1. Siga o guia do [backend](backend/README.md) para iniciar o PostgreSQL e a API.
2. Configure `frontend/.env` com `VITE_API_URL` apontando para a base da API.
3. Siga o guia do [frontend](frontend/README.md) para instalar dependências e iniciar o Vite.

Cada aplicação mantém seus próprios comandos e dependências. Não há, no estado atual, scripts de raiz, pipeline de CI/CD ou imagem Docker para as aplicações.

### Documentação por aplicação

- [Backend: configuração, execução, testes, build e migrações](backend/README.md)
- [Frontend: configuração, execução, build, lint e estrutura](frontend/README.md)
## English

### Overview

ProdutivAgro manages products and sales for agricultural organizations. It combines a .NET REST API and a Vue web application in one repository so both parts of the product evolve together.

### Architecture

```text
Browser
  └─ frontend/ (Vue 3 + TypeScript + Vite)
       └─ HTTP with cookies ──► backend/ (ASP.NET Core REST API)
                                      └─ PostgreSQL
```

The frontend uses `VITE_API_URL` as the request base URL and sends credentials through cookies. In development, the API allows the `http://localhost:5173` origin.

The backend follows Clean Architecture and DDD: API exposes HTTP endpoints; Application contains use cases and validation; Domain contains entities and contracts; Infrastructure implements persistence, authentication, and migrations.

### Monorepo structure

```text
.
├── backend/                  # .NET API, solution, tests, and PostgreSQL Docker Compose
│   ├── src/
│   ├── tests/
│   ├── ProdutivAgro.slnx
│   └── README.md
├── frontend/                 # Vue application
│   ├── src/
│   ├── package.json
│   └── README.md
└── README.md
```

### Prerequisites

- .NET SDK 8.0 or later for the backend.
- Node.js for the frontend. The project does not declare a minimum Node.js version.
- pnpm `10.33.2`, as declared in `frontend/package.json`.
- Docker Desktop or a local PostgreSQL installation for the backend database.

### Quick start

1. Follow the [backend guide](backend/README.md) to start PostgreSQL and the API.
2. Configure `frontend/.env` with `VITE_API_URL` pointing to the API base URL.
3. Follow the [frontend guide](frontend/README.md) to install dependencies and start Vite.

Each application keeps its own commands and dependencies. At this time, there are no root-level scripts, CI/CD pipeline, or application Docker image.

### Application documentation

- [Backend: setup, running, tests, build, and migrations](backend/README.md)
- [Frontend: setup, running, build, lint, and structure](frontend/README.md)