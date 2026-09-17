# ProdutivAgro Frontend

[Português](#português) | [English](#english)

## Português
Aplicação web do ProdutivAgro. Ela consome a API do diretório `../backend` para autenticação, perfil do usuário e gestão de produtos.

### Tecnologias

- Vue 3 com TypeScript
- Vite
- Vue Router e Pinia
- Axios
- Inversify e `reflect-metadata`
- Tailwind CSS, shadcn-vue, Reka UI e Lucide
- ESLint e Prettier
- pnpm

### Pré-requisitos

- Node.js. Não há uma versão mínima declarada pelo projeto.
- pnpm `10.33.2`, versão declarada em `package.json`.
- Backend em execução e acessível pela URL configurada em `VITE_API_URL`.

Execute os comandos a seguir a partir de `frontend/`.

### Configuração de ambiente

O frontend lê uma única variável de ambiente:

| Variável | Uso |
| --- | --- |
| `VITE_API_URL` | URL-base usada por todas as requisições HTTP à API. |

Crie ou atualize `frontend/.env`, que é ignorado pelo Git, com `VITE_API_URL` apontando para a base da API. As rotas do cliente usam caminhos como `/Auth/Login`, `/Users/Current` e `/products`; portanto, a URL-base deve incluir o segmento de API necessário pela instância do backend.

O cliente Axios usa `withCredentials: true`, de modo que os cookies de autenticação da API acompanham as requisições. Para desenvolvimento, o backend permite a origem `http://localhost:5173`.

Não há arquivo `.env.example` versionado.

### Instalação e execução

Instale as dependências:

```sh
pnpm install
```

Inicie o servidor de desenvolvimento:

```sh
pnpm dev
```

### Qualidade e build

| Comando | Finalidade |
| --- | --- |
| `pnpm lint` | Executa ESLint. |
| `pnpm lint:fix` | Executa ESLint e corrige problemas passíveis de correção. |
| `pnpm format:check` | Verifica formatação com Prettier. |
| `pnpm format` | Aplica formatação com Prettier. |
| `pnpm build` | Executa checagem de tipos com `vue-tsc` e gera o build Vite em `dist/`. |
| `pnpm preview` | Serve localmente o build gerado. |

Não há script ou estrutura de testes automatizados configurados no `package.json` atual.

### Estrutura

```text
frontend/
├── public/                 # Arquivos públicos
├── src/
│   ├── app/                # Inicialização, layouts, estilos e roteamento
│   ├── components/         # Componentes de layout e componentes de interface
│   ├── core/               # HTTP, DAO, DI, abstrações e utilitários
│   ├── modules/            # Funcionalidades por domínio
│   │   ├── auth/           # Login, logout, sessão e troca de senha
│   │   ├── dashboard/      # Tela inicial autenticada
│   │   └── products/       # Consulta e manutenção de produtos
│   └── shared/             # Serviços compartilhados
├── package.json
├── pnpm-lock.yaml
└── vite.config.ts
```

### Rotas atuais

| Caminho | Tela | Acesso |
| --- | --- | --- |
| `/auth/login` | Login | Apenas visitante. |
| `/` | Dashboard | Requer autenticação. |
| `/produtos` | Produtos | Requer autenticação. |

Antes de cada navegação, o frontend tenta restaurar a sessão consultando o perfil atual. Rotas protegidas redirecionam visitantes para o login; rotas de visitante redirecionam usuários autenticados ao dashboard.

### Integração HTTP

`src/core/di/container.ts` registra a URL-base fornecida por `VITE_API_URL`. As requisições são criadas por `Http`/Axios, e os módulos usam executores de comando e repositórios REST sobre essa camada.

No estado atual, o frontend integra autenticação, perfil do usuário e produtos. Vendas, organizações e convites são disponibilizados pelo backend, mas não possuem módulo de interface correspondente neste diretório.
## English

### Overview

The ProdutivAgro web application. It consumes the API in `../backend` for authentication, current-user data, and product management.

### Technologies

- Vue 3 with TypeScript
- Vite
- Vue Router and Pinia
- Axios
- Inversify and `reflect-metadata`
- Tailwind CSS, shadcn-vue, Reka UI, and Lucide
- ESLint and Prettier
- pnpm

### Prerequisites

- Node.js. The project does not declare a minimum Node.js version.
- pnpm `10.33.2`, as declared in `package.json`.
- A backend running and reachable at the URL configured through `VITE_API_URL`.

Run the following commands from `frontend/`.

### Environment configuration

The frontend reads one environment variable:

| Variable | Usage |
| --- | --- |
| `VITE_API_URL` | Base URL used by every HTTP request to the API. |

Create or update `frontend/.env`, which Git ignores, with `VITE_API_URL` pointing to the API base URL. Client routes use paths such as `/Auth/Login`, `/Users/Current`, and `/products`; therefore, the base URL must include the API segment required by the backend instance.

The Axios client uses `withCredentials: true`, so API authentication cookies are sent with requests. During development, the backend allows the `http://localhost:5173` origin.

There is no versioned `.env.example` file.

### Install and run

Install dependencies:

```sh
pnpm install
```

Start the development server:

```sh
pnpm dev
```

### Quality and build

| Command | Purpose |
| --- | --- |
| `pnpm lint` | Runs ESLint. |
| `pnpm lint:fix` | Runs ESLint and fixes auto-fixable issues. |
| `pnpm format:check` | Checks formatting with Prettier. |
| `pnpm format` | Applies Prettier formatting. |
| `pnpm build` | Runs `vue-tsc` type checking and creates the Vite build in `dist/`. |
| `pnpm preview` | Serves the generated build locally. |

There is no automated-test script or test structure configured in the current `package.json`.

### Structure

```text
frontend/
├── public/                 # Public files
├── src/
│   ├── app/                # Bootstrap, layouts, styles, and routing
│   ├── components/         # Layout and user-interface components
│   ├── core/               # HTTP, DAO, DI, abstractions, and utilities
│   ├── modules/            # Domain features
│   │   ├── auth/           # Login, logout, session, and password change
│   │   ├── dashboard/      # Authenticated landing page
│   │   └── products/       # Product queries and management
│   └── shared/             # Shared services
├── package.json
├── pnpm-lock.yaml
└── vite.config.ts
```

### Current routes

| Path | Page | Access |
| --- | --- | --- |
| `/auth/login` | Login | Guest only. |
| `/` | Dashboard | Requires authentication. |
| `/produtos` | Products | Requires authentication. |

Before every navigation, the frontend tries to restore the session by requesting the current user. Protected routes redirect guests to login; guest-only routes redirect authenticated users to the dashboard.

### HTTP integration

`src/core/di/container.ts` registers the base URL provided by `VITE_API_URL`. Requests are created by `Http`/Axios, while modules use command executors and REST repositories over that layer.

At present, the frontend integrates authentication, current-user data, and products. Sales, organizations, and invitations are exposed by the backend but have no corresponding UI module in this directory.