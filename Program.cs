using entity_framework.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using entity_framework.Models;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<TareasContext>((context) => context.UseInMemoryDatabase("tareas_db"));

// obtendremos la cadena de conexión para nuestra base de datos
string? connectionString = builder.Configuration.GetConnectionString("cnTareas");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("\n\n----------> La cadena de conexión no es valida <----------\n\n");
}

builder.Services.AddSqlServer<TareasContext>(connectionString);

var app = builder.Build();

app.MapGet("/", () => "Hello World!.");
app.MapGet("/connection", async ([FromServices] TareasContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok($"Status: {dbContext.Database.IsInMemory()}");
});


app.MapGet("/api/tareas", async ([FromServices] TareasContext dbContext) =>
{
    var tareas = dbContext.tareas.Include(t => t.categoria);
    return Results.Ok(tareas);
});

app.MapPost("/api/tareas", async ([FromServices] TareasContext db, [FromBody] Tarea tarea) =>
{
    tarea.tareaId = Guid.NewGuid();
    tarea.creacion = DateTime.Now;

    await db.tareas.AddAsync(tarea);
    await db.SaveChangesAsync();
    string locationUri = "/api/tareas/" + tarea.tareaId.ToString();

    return Results.Created(locationUri, new { message = "Tarea creada con éxito" });
});

app.MapPut("/api/tareas/{id}", async ([FromServices] TareasContext db, [FromBody] Tarea tarea, [FromRoute] Guid id) =>
{
    Tarea? currentTarea = await db.tareas.FindAsync(id);

    if (currentTarea is null)
    {
        return Results.NotFound(new
        {
            message = "Tarea no encontrada",
        });
    }

    currentTarea.titulo = tarea.titulo;
    currentTarea.descripcion = tarea.descripcion;
    currentTarea.categoiaId = tarea.categoiaId;
    currentTarea.estado = tarea.estado;

    if (currentTarea.estado == EstadoTarea.COMPLETADA)
    {
        currentTarea.fechaFinalizacion = DateTime.Now;
    }

    await db.SaveChangesAsync();

    return Results.Ok(new { message = "Tarea actualizada con éxito" });
});

app.MapDelete("/api/tareas/{id}", async ([FromServices] TareasContext db, [FromRoute] Guid id) =>
{
    Tarea? tarea = await db.tareas.FindAsync(id);

    if (tarea is null)
    {
        return Results.BadRequest(new
        {
            message = "Error al eliminar la tarea",
        });
    }

    db.tareas.Remove(tarea);
    await db.SaveChangesAsync();

    return Results.Ok(new
    {
        message = "Tarea eliminada con éxito",
    });
});

app.Run();