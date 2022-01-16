using System.ComponentModel.DataAnnotations;

/// <summary>
/// EF Setup : 
/// https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/migrations?view=aspnetcore-6.0
/// 
/// Had to : 
/// 1- dotnet tool install dotnet-ef -g
/// 2- reference Microsoft.EntityFrameworkCore.Design in csproj
/// 3- dotnet ef migrations add InitialCreate
/// 4- dotnet-ef database update
/// </summary>
public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Todo> Todos => Set<Todo>();
}

public class Todo
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;
    public bool IsComplete { get; set; }
}
