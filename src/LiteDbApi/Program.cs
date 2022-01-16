/// <summary>
/// https://www.litedb.org/docs/getting-started/
/// https://github.com/sbecerek/minnak/blob/master/src/minnak.API/Program.cs
/// </summary>

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<TodoDbContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();

app.MapGet("/", () => "Hello World!").ExcludeFromDescription(); ;

app.MapGet("/todos", (TodoDbContext db) =>  db.Database.GetCollection<Todo>().FindAll().ToList());

app.MapGet("/todos/{id}", (TodoDbContext db, int id) => 
    db.Database.GetCollection<Todo>().FindById(id) switch
    {
        Todo todo => Results.Ok(todo),
        null => Results.NotFound()
    }
);

app.MapPost("/todos", (TodoDbContext db, Todo todo) =>
{
    db.Database.GetCollection<Todo>().Insert(todo);
    return Results.Created($"/todos/{todo.Id}", todo);
});

app.UseSwaggerUI();
app.Run();
