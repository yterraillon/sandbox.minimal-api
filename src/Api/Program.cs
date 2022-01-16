/// <summary>
/// Based on https://medium.com/geekculture/minimal-apis-in-net-6-a-complete-guide-beginners-advanced-fd64f4da07f5
/// </summary>

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.MapGet("/", MyHandler.Hello);

app.Urls.Add("http://localhost:3000");
app.Urls.Add("http://localhost:4000");

app.Run();

class MyHandler {

    public static string Hello()
    { return "Hello World!"; }
}
