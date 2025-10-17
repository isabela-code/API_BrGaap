using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TodoApi.Data;
using TodoApi.DTOs;
using TodoApi.Models;
using Xunit;

namespace TodoApi.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        
        builder.ConfigureServices(services =>
        {
            // Adiciona o DbContext com InMemoryDatabase para testes
            services.AddDbContext<TodoContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryTestDb");
            });

            // Inicializa o banco de dados
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<TodoContext>();

            db.Database.EnsureCreated();

            // Adiciona dados de teste
            if (!db.Todos.Any())
            {
                db.Todos.AddRange(
                    new Todo { Id = 1, UserId = 1, Title = "Test Todo 1", Completed = true },
                    new Todo { Id = 2, UserId = 1, Title = "Test Todo 2", Completed = true },
                    new Todo { Id = 3, UserId = 2, Title = "Test Todo 3", Completed = false }
                );
                db.SaveChanges();
            }
        });
    }
}

public class TodosApiIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;

    public TodosApiIntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task GetTodos_ShouldReturnOkWithTodos()
    {
        // Act
        var response = await _client.GetAsync("/todos");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var todos = await response.Content.ReadFromJsonAsync<List<TodoDto>>();
        todos.Should().NotBeNull();
        todos.Should().HaveCountGreaterOrEqualTo(3);
    }

    [Fact]
    public async Task GetTodos_WithPagination_ShouldReturnCorrectPage()
    {
        // Act
        var response = await _client.GetAsync("/todos?page=1&pageSize=2");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var paginatedResponse = await response.Content.ReadFromJsonAsync<PaginatedResponse<TodoDto>>();
        paginatedResponse.Should().NotBeNull();
        paginatedResponse!.Data.Should().HaveCount(2);
        paginatedResponse.Page.Should().Be(1);
        paginatedResponse.PageSize.Should().Be(2);
        paginatedResponse.TotalCount.Should().Be(3);
    }

    [Fact]
    public async Task GetTodo_WithValidId_ShouldReturnTodo()
    {
        // Act
        var response = await _client.GetAsync("/todos/1");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var todo = await response.Content.ReadFromJsonAsync<TodoDto>();
        todo.Should().NotBeNull();
        todo!.Id.Should().Be(1);
        todo.Title.Should().Be("Test Todo 1");
    }

    [Fact]
    public async Task GetTodo_WithInvalidId_ShouldReturnNotFound()
    {
        // Act
        var response = await _client.GetAsync("/todos/999");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateTodo_WithValidData_ShouldCreateTodo()
    {
        // Arrange - Cria uma tarefa COMPLETA para não violar a regra de 5 incompletas
        var newTodo = new TodoDto
        {
            UserId = 10,
            Title = "New Test Todo",
            Completed = true  // Importante: marcada como completa
        };

        // Act
        var response = await _client.PostAsJsonAsync("/todos", newTodo);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var createdTodo = await response.Content.ReadFromJsonAsync<TodoDto>();
        createdTodo.Should().NotBeNull();
        createdTodo!.Title.Should().Be("New Test Todo");
        createdTodo.UserId.Should().Be(10);
        createdTodo.Completed.Should().BeTrue();
        
        response.Headers.Location.Should().NotBeNull();
    }

    [Fact]
    public async Task CreateTodo_WithoutTitle_ShouldReturnBadRequest()
    {
        // Arrange
        var newTodo = new TodoDto
        {
            UserId = 1,
            Title = "",  // Título vazio (inválido)
            Completed = true
        };

        // Act
        var response = await _client.PostAsJsonAsync("/todos", newTodo);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task CreateTodo_ExceedingLimit_ShouldReturnBadRequest()
    {
        // Arrange - Cria 5 tarefas incompletas para o mesmo usuário
        var userId = 100;
        for (int i = 0; i < 5; i++)
        {
            var todo = new TodoDto
            {
                UserId = userId,
                Title = $"Incomplete Todo {i + 1}",
                Completed = false
            };
            await _client.PostAsJsonAsync("/todos", todo);
        }

        // Act - Tenta criar a 6ª tarefa incompleta (deve falhar)
        var exceededTodo = new TodoDto
        {
            UserId = userId,
            Title = "Exceeded Todo",
            Completed = false
        };
        var response = await _client.PostAsJsonAsync("/todos", exceededTodo);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var errorMessage = await response.Content.ReadAsStringAsync();
        errorMessage.Should().Contain("5");  // Deve mencionar o limite de 5
    }

    [Fact]
    public async Task UpdateTodo_WithValidId_ShouldUpdateTodo()
    {
        // Arrange - Usa o ID 2 que existe no banco de teste
        var updateDto = new { completed = true };

        // Act
        var response = await _client.PutAsJsonAsync("/todos/2", updateDto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        // Verifica se a atualização foi aplicada
        var getResponse = await _client.GetAsync("/todos/2");
        var updatedTodo = await getResponse.Content.ReadFromJsonAsync<TodoDto>();
        updatedTodo!.Completed.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateTodo_WithInvalidId_ShouldReturnNotFound()
    {
        // Arrange
        var updateDto = new { completed = true };

        // Act
        var response = await _client.PutAsJsonAsync("/todos/999", updateDto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteTodo_WithValidId_ShouldDeleteTodo()
    {
        // Arrange - Primeiro cria uma tarefa para deletar
        var newTodo = new TodoDto
        {
            UserId = 50,
            Title = "Todo to Delete",
            Completed = true
        };
        var createResponse = await _client.PostAsJsonAsync("/todos", newTodo);
        var createdTodo = await createResponse.Content.ReadFromJsonAsync<TodoDto>();

        // Act
        var deleteResponse = await _client.DeleteAsync($"/todos/{createdTodo!.Id}");

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        // Verifica se foi realmente deletado
        var getResponse = await _client.GetAsync($"/todos/{createdTodo.Id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteTodo_WithInvalidId_ShouldReturnNotFound()
    {
        // Act
        var response = await _client.DeleteAsync("/todos/999");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
