using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Não adiciona SQLite durante testes - os testes configurarão InMemoryDatabase
if (!builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddDbContext<TodoContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
}

builder.Services.AddHttpClient<ITodoSyncService, TodoSyncService>();
builder.Services.AddScoped<ITodoSyncService, TodoSyncService>();

builder.Services.AddOpenApi();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSAPUI5", policy =>
    {
        policy.WithOrigins("http://localhost:8080", "http://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection();
app.UseCors("AllowSAPUI5");
app.MapControllers();

// Não criar banco de dados durante testes de integração
if (!app.Environment.IsEnvironment("Testing"))
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<TodoContext>();
        context.Database.EnsureCreated();
    }
}

app.Run();

public partial class Program { }
