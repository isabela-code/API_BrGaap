# Testes de Integração - TodoApi

## Status Atual

Os arquivos de teste foram criados do zero, mas ainda há um problema técnico a ser resolvido:

### Problema
O Entity Framework Core não permite ter dois provedores de banco de dados (SQLite e InMemory) registrados simultaneamente no mesmo Service Provider.

### Arquivos Criados

1. **TodoApi.Tests.csproj** - Projeto de testes com todas as dependências necessárias:
   - xUnit (framework de testes)
   - FluentAssertions (assertions mais legíveis)
   - Microsoft.AspNetCore.Mvc.Testing (para testes de integração)
   - Microsoft.EntityFrameworkCore.InMemory (banco de dados em memória para testes)

2. **TodosApiIntegrationTests.cs** - Classe de testes com 11 casos de teste cobrindo:
   - GET /todos (lista com paginação)
   - GET /todos/{id} (busca por ID)
   - POST /todos (criação com validação de regras de negócio)
   - PUT /todos/{id} (atualização)
   - DELETE /todos/{id} (exclusão)
   - Testes de erros (IDs inválidos, validações, etc.)

### Solução Necessária

Para fazer os testes funcionarem corretamente, é necessário:

1. **Opção 1 - Modificar Program.cs para aceitar configuração condicional**:
   - O Program.cs já verifica o ambiente "Testing"
   - Mas a configuração do DbContext precisa ser melhor isolada
   
2. **Opção 2 - Usar apenas SQLite nos testes**:
   - Remover a tentativa de usar InMemoryDatabase
   - Usar um arquivo SQLite temporário para cada execução de teste

3. **Opção 3 - Criar uma classe de inicialização customizada**:
   - Criar uma WebApplicationFactory customizada
   - Substituir completamente a configuração do DbContext

## Como os Testes Deveriam Funcionar

```csharp
// 1. WebApplicationFactory cria uma instância de teste da API
// 2. DbContext é configurado para usar InMemoryDatabase
// 3. Dados de teste são inseridos no banco em memória
// 4. Testes HTTP são executados contra a API
// 5. Banco de dados em memória é limpo entre os testes
```

##Estrutura dos Testes

```
TodoApi.Tests/
├── TodoApi.Tests.csproj
└── TodosApiIntegrationTests.cs
    ├── GetTodos_ShouldReturnOkWithTodos
    ├── GetTodos_WithPagination_ShouldReturnCorrectPage
    ├── GetTodo_WithValidId_ShouldReturnTodo
    ├── GetTodo_WithInvalidId_ShouldReturnNotFound
    ├── CreateTodo_WithValidData_ShouldCreateTodo
    ├── CreateTodo_WithoutTitle_ShouldReturnBadRequest
    ├── CreateTodo_ExceedingLimit_ShouldReturnBadRequest
    ├── UpdateTodo_WithValidId_ShouldUpdateTodo
    ├── UpdateTodo_WithInvalidId_ShouldReturnNotFound
    ├── DeleteTodo_WithValidId_ShouldDeleteTodo
    └── DeleteTodo_WithInvalidId_ShouldReturnNotFound
```

## Próximos Passos

1. Resolver o conflito de provedores de banco de dados
2. Executar os testes e verificar se passam
3. Adicionar testes adicionais conforme necessário
4. Configurar CI/CD para executar os testes automaticamente
