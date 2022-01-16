/// <summary>
/// Based on the following examples : 
/// https://medium.com/geekculture/minimal-apis-in-net-6-a-complete-guide-beginners-advanced-fd64f4da07f5
/// https://github.com/davidfowl/CommunityStandUpMinimalAPI
/// https://www.hanselman.com/blog/exploring-a-minimal-web-api-with-aspnet-core-6
/// </summary>
var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Todos") ?? "Data Source=Todos.db";
builder.Services.AddSqlite<TodoDbContext>(connectionString);

builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();

app.MapGet("/", () => "Hello World!")
        .ExcludeFromDescription();

app.MapGet("/todos", async (TodoDbContext db) => await db.Todos.ToListAsync());

app.MapGet("/todos/{id}", async (TodoDbContext db, int id) =>
    await db.Todos.FindAsync(id) switch
        {
            Todo todo => Results.Ok(todo),
            null => Results.NotFound()
        }
);

app.MapPost("/todos", async (TodoDbContext db, Todo todo) =>
{
    await db.Todos.AddAsync(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/todos/{todo.Id}", todo);
});

app.UseSwaggerUI();
app.Run();
