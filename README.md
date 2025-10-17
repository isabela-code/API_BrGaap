# 🚀 Todo App Full Stack - ASP.NET Core + SAPUI5

[![.NET](https://img.shields.io/badge/.NET-9.0-purple)](https://dotnet.microsoft.com/)
[![SAPUI5](https://img.shields.io/badge/SAPUI5-1.120.0-blue)](https://ui5.sap.com/)
[![SQLite](https://img.shields.io/badge/SQLite-Database-green)](https://sqlite.org/)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

Uma aplicação completa de gerenciamento de tarefas (TODOs) desenvolvida com arquitetura Full Stack moderna, utilizando ASP.NET Core no backend e SAPUI5 no frontend.

![Todo App Screenshot](https://via.placeholder.com/800x400/2196F3/ffffff?text=Todo+App+Full+Stack)

## 📋 Índice

- [🎯 Visão Geral](#-visão-geral)
- [✨ Funcionalidades](#-funcionalidades)
- [🏗️ Arquitetura](#-arquitetura)
- [🛠️ Tecnologias](#-tecnologias)
- [⚡ Início Rápido](#-início-rápido)
- [📁 Estrutura do Projeto](#-estrutura-do-projeto)
- [🔧 Configuração](#-configuração)
- [🌐 API Endpoints](#-api-endpoints)
- [🎨 Frontend](#-frontend)
- [💾 Banco de Dados](#-banco-de-dados)
- [🧪 Testes](#-testes)
- [📦 Deploy](#-deploy)
- [🤝 Contribuição](#-contribuição)
- [📄 Licença](#-licença)

## 🎯 Visão Geral

O **Todo App Full Stack** é uma aplicação web moderna para gerenciamento de tarefas que demonstra as melhores práticas de desenvolvimento Full Stack. A aplicação oferece uma interface intuitiva construída com SAPUI5 e uma API robusta desenvolvida em ASP.NET Core.


## ✨ Funcionalidades

### 🎛️ Interface do Usuário
- ✅ **Criação de Tarefas** - Adicione novas tarefas com título e prioridade
- 🔄 **Alteração de Status** - Marque tarefas como completas/pendentes com um clique
- 🎚️ **Sistema de Prioridades** - Alta (🔴), Média (🟡), Baixa (🟢)
- 🔍 **Pesquisa Avançada** - Busque por título, ID ou usuário
- 📄 **Paginação Inteligente** - Navegue facilmente entre múltiplas páginas
- 📱 **Design Responsivo** - Interface adaptável para todos os dispositivos

### ⚙️ Backend
- 🛡️ **API RESTful** - Endpoints padronizados e documentados
- 📊 **Paginação e Filtros** - Suporte a consultas otimizadas
- 🔗 **Sincronização Externa** - Integração com JSONPlaceholder API
- 🎯 **Regras de Negócio** - Máximo 5 tarefas incompletas por usuário
- 💾 **Persistência** - Banco SQLite com Entity Framework Core
- 📈 **Logging** - Monitoramento completo das operações

### 🎨 Experiência do Usuário
- 🚀 **Performance** - Carregamento rápido e responsivo
- 💬 **Feedback Visual** - Mensagens de sucesso/erro em tempo real
- 🎭 **Tema Moderno** - Interface limpa com SAP Horizon theme
- 🔄 **Updates em Tempo Real** - Sincronização automática de dados
- 💾 **Persistência Local** - Prioridades salvas no localStorage


### 🎯 Padrões Implementados
- **MVC Pattern** - Separação clara de responsabilidades
- **Repository Pattern** - Abstração da camada de dados
- **DTO Pattern** - Transferência de dados otimizada
- **CORS** - Comunicação segura entre domínios
- **REST** - API padronizada e escalável

## 🛠️ Tecnologias

### 🔧 Backend
| Tecnologia | Versão | Descrição |
|------------|--------|-----------|
| **.NET Core** | 9.0 | Framework principal |
| **ASP.NET Core** | 9.0 | Web API framework |
| **Entity Framework Core** | 9.0 | ORM para acesso a dados |
| **SQLite** | Latest | Banco de dados embutido |
| **Swagger** | Latest | Documentação da API |

### 🎨 Frontend
| Tecnologia | Versão | Descrição |
|------------|--------|-----------|
| **SAPUI5** | 1.120.0 | Framework UI empresarial |
| **OpenUI5** | 1.120.0 | Versão open source |
| **SAP Horizon** | Latest | Tema moderno |
| **Express.js** | 4.18.2 | Servidor de desenvolvimento |
| **HTML5/CSS3** | Latest | Marcação e estilos |

### 🧪 Testes
| Tecnologia | Versão | Descrição |
|------------|--------|-----------|
| **xUnit** | 2.6.2 | Framework de testes |
| **FluentAssertions** | 6.12.0 | Assertions expressivas |
| **AspNetCore.Mvc.Testing** | 9.0.0 | Testes de integração |
| **EF Core InMemory** | 9.0.0 | Banco de dados para testes |

### 🔨 Ferramentas de Desenvolvimento
- **Visual Studio Code** - IDE principal
- **Node.js** - Runtime para servidor frontend
- **PowerShell** - Scripts de automação
- **Git** - Controle de versão

## ⚡ Início Rápido

### 📋 Pré-requisitos

```bash
# Verificar versões necessárias
dotnet --version  # >= 9.0
node --version    # >= 18.0
npm --version     # >= 9.0
```

### 🚀 Instalação

1. **Clone o repositório**
```bash
git clone https://github.com/seu-usuario/todo-app-fullstack.git
cd todo-app-fullstack
```

2. **Configure o Backend**
```bash
cd backend/src/TodoApi
dotnet restore
dotnet build
```

3. **Configure o Frontend**
```bash
cd frontend
npm install
```

4. **Execute a aplicação**

**Opção 1 - Usando VS Code Tasks (Recomendado):**
```
Ctrl+Shift+P → Tasks: Run Task
- run-backend (inicia o backend)
- build-backend (compila o backend)
- test-backend (executa os testes)
```

**Opção 2 - Terminal Manual:**

**Terminal 1 - Backend:**
```bash
cd backend/src/TodoApi
dotnet run
```

**Terminal 2 - Frontend:**
```bash
cd frontend
npm start
```

5. **Acesse a aplicação**
- Frontend SAPUI5: http://localhost:3000
- Backend API: http://localhost:5000
- Swagger UI: http://localhost:5000/swagger

## 🌐 API Endpoints

A API RESTful oferece os seguintes endpoints:

### 📋 Listar TODOs
```http
GET /todos
GET /todos?page=1&pageSize=10
GET /todos?title=comprar&completed=false
GET /todos?sort=title&order=desc
```

**Parâmetros de Query:**
- `page` (opcional): Número da página para paginação
- `pageSize` (opcional): Quantidade de itens por página
- `title` (opcional): Filtro por título (busca parcial)
- `completed` (opcional): Filtro por status (true/false)
- `sort` (opcional): Campo para ordenação (id, title, userid, completed)
- `order` (opcional): Direção da ordenação (asc, desc)

**Resposta sem paginação:**
```json
[
  {
    "id": 1,
    "userId": 1,
    "title": "Comprar leite",
    "completed": false
  }
]
```

**Resposta com paginação:**
```json
{
  "data": [...],
  "page": 1,
  "pageSize": 10,
  "totalCount": 50,
  "totalPages": 5
}
```

### 🔍 Buscar TODO por ID
```http
GET /todos/{id}
```

**Resposta:**
```json
{
  "id": 1,
  "userId": 1,
  "title": "Comprar leite",
  "completed": false
}
```

### ➕ Criar TODO
```http
POST /todos
Content-Type: application/json

{
  "userId": 1,
  "title": "Nova tarefa",
  "completed": false
}
```

**Regras de Negócio:**
- ❗ Título é obrigatório
- ❗ Máximo de 5 tarefas incompletas por usuário

### ✏️ Atualizar TODO
```http
PUT /todos/{id}
Content-Type: application/json

{
  "completed": true
}
```

**Resposta:** `204 No Content`

### 🗑️ Deletar TODO
```http
DELETE /todos/{id}
```

**Resposta:** `204 No Content`

### 🔄 Sincronizar com API Externa
```http
POST /todos/sync
```

Sincroniza dados com [JSONPlaceholder API](https://jsonplaceholder.typicode.com/).

**Resposta:**
```json
{
  "message": "Sincronização realizada com sucesso"
}
```

## 📁 Estrutura do Projeto

```
📁 TodoApp/
├── 📄 README.md                    # Documentação principal
├──  .vscode/                     # Configurações VS Code
│   └── 📄 tasks.json
├── 📁 .github/                     # Configurações GitHub
│   └── 📄 copilot-instructions.md
├── 📁 backend/                     # 🔧 Backend ASP.NET Core
│   ├── 📄 TodoApp.sln              # Solution principal
│   ├── 📁 src/TodoApi/             # Projeto da API
│   │   ├── 📄 Program.cs           # Configuração da aplicação
│   │   ├── 📄 TodoApi.csproj       # Arquivo de projeto
│   │   ├── 📄 TodoApi.http         # Testes HTTP
│   │   ├── 📄 appsettings.json     # Configurações
│   │   ├── 📄 todos.db             # Banco SQLite
│   │   ├── 📁 Controllers/         # Controladores API
│   │   │   └── 📄 TodosController.cs
│   │   ├── 📁 Models/              # Modelos de dados
│   │   │   └── 📄 Todo.cs
│   │   ├── 📁 DTOs/                # Data Transfer Objects
│   │   │   └── 📄 TodoDto.cs
│   │   ├── 📁 Data/                # Contexto do banco
│   │   │   └── 📄 TodoContext.cs
│   │   ├── 📁 Services/            # Serviços de negócio
│   │   │   └── 📄 TodoSyncService.cs
│   │   └── 📁 Properties/          # Configurações de execução
│   └── 📁 tests/                   # 🧪 Testes de Integração
│       └── 📁 TodoApi.Tests/       
│           ├── 📄 TodoApi.Tests.csproj
│           └── 📄 TodosApiIntegrationTests.cs  # 11 testes ✅
└── 📁 frontend/                    # 🎨 Frontend SAPUI5
    ├── 📄 package.json             # Dependências Node.js
    ├── 📄 server.js                # Servidor Express
    └── 📁 webapp/                  # Aplicação web
        └── 📄 index.html           # ⭐ App SAPUI5 principal
```


## 🧪 Testes

O projeto implementa testes de integração completos para garantir a qualidade e confiabilidade da API.

### ✅ Cobertura de Testes

**11 testes de integração** cobrindo todos os endpoints da API:

#### Testes de Leitura (GET)
- ✅ `GetTodos_ShouldReturnOkWithTodos` - Lista todos os TODOs
- ✅ `GetTodos_WithPagination_ShouldReturnCorrectPage` - Paginação funcional
- ✅ `GetTodo_WithValidId_ShouldReturnTodo` - Busca por ID válido
- ✅ `GetTodo_WithInvalidId_ShouldReturnNotFound` - ID inválido retorna 404

#### Testes de Criação (POST)
- ✅ `CreateTodo_WithValidData_ShouldCreateTodo` - Criação com dados válidos
- ✅ `CreateTodo_WithoutTitle_ShouldReturnBadRequest` - Validação de título obrigatório
- ✅ `CreateTodo_ExceedingLimit_ShouldReturnBadRequest` - Regra de negócio: máx. 5 tarefas incompletas

#### Testes de Atualização (PUT)
- ✅ `UpdateTodo_WithValidId_ShouldUpdateTodo` - Atualização com ID válido
- ✅ `UpdateTodo_WithInvalidId_ShouldReturnNotFound` - ID inválido retorna 404

#### Testes de Exclusão (DELETE)
- ✅ `DeleteTodo_WithValidId_ShouldDeleteTodo` - Exclusão com ID válido
- ✅ `DeleteTodo_WithInvalidId_ShouldReturnNotFound` - ID inválido retorna 404

### 🏃 Executando os Testes

```bash
# Executar todos os testes
cd backend
dotnet test

# Executar com detalhes
dotnet test --logger "console;verbosity=normal"

# Executar testes específicos
dotnet test --filter "GetTodos"

# Executar com cobertura
dotnet test /p:CollectCoverage=true
```

### 🏗️ Arquitetura de Testes

Os testes utilizam:
- **WebApplicationFactory**: Cria uma instância de teste da API
- **InMemory Database**: Banco de dados isolado para cada teste
- **FluentAssertions**: Assertions expressivas e legíveis
- **xUnit**: Framework moderno de testes

```csharp
// Exemplo de teste
[Fact]
public async Task GetTodos_ShouldReturnOkWithTodos()
{
    // Arrange - dados de teste já configurados

    // Act
    var response = await _client.GetAsync("/todos");

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.OK);
    var todos = await response.Content.ReadFromJsonAsync<List<TodoDto>>();
    todos.Should().NotBeNull();
    todos.Should().HaveCount(3);
}
```

### 🎯 Resultados

```
Total de testes: 11
     Aprovados: 11 ✅
     Com falha: 0
     Ignorados: 0
Tempo total: ~1.5s
```


## 📊 Status do Projeto

- ✅ **Backend API** - Completo e funcional
- ✅ **Frontend SAPUI5** - Completo com todas as funcionalidades
- ✅ **Banco de Dados** - SQLite configurado
- ✅ **Documentação** - README completo
- ✅ **Testes de Integração** - 11 testes passando (100% de cobertura)


## 📄 Licença

Este projeto está licenciado sob a Licença MIT - veja o arquivo [LICENSE](LICENSE) para detalhes.