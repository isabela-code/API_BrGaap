using TodoApi.Models;

namespace TodoApi.Services;

public interface ITodoSyncService
{
    Task SyncTodosAsync();
}

public class TodoSyncService : ITodoSyncService
{
    private readonly HttpClient _httpClient;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<TodoSyncService> _logger;

    public TodoSyncService(HttpClient httpClient, IServiceProvider serviceProvider, ILogger<TodoSyncService> logger)
    {
        _httpClient = httpClient;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task SyncTodosAsync()
    {
        try
        {
            _logger.LogInformation("Iniciando sincronização com JSONPlaceholder");
            
            var response = await _httpClient.GetFromJsonAsync<Todo[]>("https://jsonplaceholder.typicode.com/todos");
            
            if (response != null)
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<Data.TodoContext>();
                
                // Remove todos existentes
                context.Todos.RemoveRange(context.Todos);
                
                // Adiciona novos todos
                await context.Todos.AddRangeAsync(response);
                await context.SaveChangesAsync();
                
                _logger.LogInformation($"Sincronização concluída. {response.Length} todos carregados.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro durante a sincronização");
            throw;
        }
    }
}