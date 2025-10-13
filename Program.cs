using entity_framework.Context;
using entity_framework.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddDbContext<TareasContext>((context) => context.UseInMemoryDatabase("tareas_db"));

// obtendremos la cadena de conexión para nuestra base de datos
string? connectionString = builder.Configuration.GetConnectionString("cnTareas");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("\n\n----------> La cadena de conexión no es valida <----------\n\n");
}

builder.Services.AddSqlServer<TareasContext>(connectionString);
builder.Services.AddScoped<ITareasService, TareaService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

//app.MapControllers();

app.Run();