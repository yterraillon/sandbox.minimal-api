public class TodoDbContext
{
    public LiteDatabase Database { get; }
    public TodoDbContext()
    {
        Database = new LiteDatabase("Todos.db");

        Database.GetCollection<Todo>()
            .EnsureIndex(todo => todo.Id);
    }
}

public record Todo(int Id, string Title, bool IsComplete);
