using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TodosController : ControllerBase
{
    private readonly TodoContext _context;
    private readonly ITodoSyncService _syncService;
    private readonly ILogger<TodosController> _logger;

    public TodosController(TodoContext context, ITodoSyncService syncService, ILogger<TodosController> logger)
    {
        _context = context;
        _syncService = syncService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<TodoDto>>> GetTodos(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? title = null,
        [FromQuery] bool? completed = null,
        [FromQuery] string sort = "id",
        [FromQuery] string order = "asc")
    {
        var query = _context.Todos.AsQueryable();

        // Filtro por título
        if (!string.IsNullOrEmpty(title))
        {
            query = query.Where(t => t.Title.Contains(title));
        }
        
        // Filtro por status
        if (completed.HasValue)
        {
            query = query.Where(t => t.Completed == completed.Value);
        }

        // Ordenação
        query = sort.ToLower() switch
        {
            "title" => order.ToLower() == "desc" ? query.OrderByDescending(t => t.Title) : query.OrderBy(t => t.Title),
            "userid" => order.ToLower() == "desc" ? query.OrderByDescending(t => t.UserId) : query.OrderBy(t => t.UserId),
            "completed" => order.ToLower() == "desc" ? query.OrderByDescending(t => t.Completed) : query.OrderBy(t => t.Completed),
            _ => order.ToLower() == "desc" ? query.OrderByDescending(t => t.Id) : query.OrderBy(t => t.Id)
        };

        var totalCount = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        var todos = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new TodoDto
            {
                Id = t.Id,
                UserId = t.UserId,
                Title = t.Title,
                Completed = t.Completed
            })
            .ToListAsync();

        var response = new PaginatedResponse<TodoDto>
        {
            Data = todos,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoDto>> GetTodo(int id)
    {
        var todo = await _context.Todos.FindAsync(id);

        if (todo == null)
        {
            return NotFound();
        }

        var todoDto = new TodoDto
        {
            Id = todo.Id,
            UserId = todo.UserId,
            Title = todo.Title,
            Completed = todo.Completed
        };

        return Ok(todoDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodo(int id, UpdateTodoDto updateDto)
    {
        var todo = await _context.Todos.FindAsync(id);

        if (todo == null)
        {
            return NotFound();
        }

        // Validar regra de negócio: máximo 5 tarefas incompletas por usuário
        if (!updateDto.Completed && todo.Completed)
        {
            var incompleteCount = await _context.Todos
                .CountAsync(t => t.UserId == todo.UserId && !t.Completed);

            if (incompleteCount >= 5)
            {
                return BadRequest("Usuário já possui o máximo de 5 tarefas incompletas. Complete uma tarefa antes de marcar outra como incompleta.");
            }
        }

        todo.Completed = updateDto.Completed;
        // todo.Priority = updateDto.Priority ?? todo.Priority; // Temporarily disabled until column exists
        await _context.SaveChangesAsync();

        var updatedTodoDto = new TodoDto
        {
            Id = todo.Id,
            Title = todo.Title,
            UserId = todo.UserId,
            Completed = todo.Completed
        };

        return Ok(updatedTodoDto);
    }

    [HttpPost]
    public async Task<ActionResult<TodoDto>> CreateTodo([FromBody] TodoDto todoDto)
    {
        if (string.IsNullOrWhiteSpace(todoDto.Title))
        {
            return BadRequest("Title is required");
        }

        // Check business rule: maximum 5 incomplete todos per user
        var incompleteCount = await _context.Todos
            .CountAsync(t => t.UserId == todoDto.UserId && !t.Completed);
        
        if (incompleteCount >= 5)
        {
            return BadRequest("Usuário já possui o máximo de 5 tarefas incompletas");
        }

        var todo = new Todo
        {
            Title = todoDto.Title,
            UserId = todoDto.UserId,
            Completed = todoDto.Completed
        };

        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        var createdTodoDto = new TodoDto
        {
            Id = todo.Id,
            Title = todo.Title,
            UserId = todo.UserId,
            Completed = todo.Completed
        };

        return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, createdTodoDto);
    }

    [HttpPost("sync")]
    public async Task<IActionResult> SyncTodos()
    {
        try
        {
            await _syncService.SyncTodosAsync();
            return Ok(new { message = "Sincronização realizada com sucesso" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro durante sincronização");
            return StatusCode(500, new { message = "Erro interno do servidor durante a sincronização" });
        }
    }
}