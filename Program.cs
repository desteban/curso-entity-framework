var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!.");
app.MapGet("/saludo", () => "Hola mundo!.");
app.MapGet("/salud", () => "Vamos a Brindar!.");


app.Run();