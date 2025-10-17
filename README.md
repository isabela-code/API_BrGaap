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
- Frontend SAPUI5: http://localhost:3000/index_completo.html

## 📁 Estrutura do Projeto

```
📁 TodoApp/
├── 📄 README_COMPLETO.md           # Documentação principal
├── 📄 cleanup-project.ps1          # Script de limpeza
├── 📁 .vscode/                     # Configurações VS Code
│   └── 📄 tasks.json
├── 📁 .github/                     # Configurações GitHub
│   └── 📄 copilot-instructions.md
├── 📁 backend/                     # 🔧 Backend ASP.NET Core
│   ├── 📄 TodoApp.sln              # Solution principal
│   └── 📁 src/TodoApi/             # Projeto da API
│       ├── 📄 Program.cs           # Configuração da aplicação
│       ├── 📄 TodoApi.csproj       # Arquivo de projeto
│       ├── 📄 TodoApi.http         # Testes HTTP
│       ├── 📄 appsettings.json     # Configurações
│       ├── 📄 todos.db             # Banco SQLite
│       ├── 📁 Controllers/         # Controladores API
│       │   └── 📄 TodosController.cs
│       ├── 📁 Models/              # Modelos de dados
│       │   └── 📄 Todo.cs
│       ├── 📁 DTOs/                # Data Transfer Objects
│       │   └── 📄 TodoDto.cs
│       ├── 📁 Data/                # Contexto do banco
│       │   └── 📄 TodoContext.cs
│       ├── 📁 Services/            # Serviços de negócio
│       │   └── 📄 TodoSyncService.cs
│       ├── 📁 Migrations/          # Migrações EF Core
│       └── 📁 Properties/          # Configurações de execução
└── 📁 frontend/                    # 🎨 Frontend SAPUI5
    ├── 📄 package.json             # Dependências Node.js
    ├── 📄 server.js                # Servidor Express
    └── 📁 webapp/                  # Aplicação web
        ├── 📄 index_completo.html  # ⭐ App SAPUI5 principal
        └── 📄 working.html         # ⭐ Versão HTML pura
```


## 📊 Status do Projeto

- ✅ **Backend API** - Completo e funcional
- ✅ **Frontend SAPUI5** - Completo com todas as funcionalidades
- ✅ **Banco de Dados** - SQLite configurado
- ✅ **Documentação** - README completo
- ✅ **Testes** - Estrutura básica implementada


## 📄 Licença

Este projeto está licenciado sob a Licença MIT - veja o arquivo [LICENSE](LICENSE) para detalhes.