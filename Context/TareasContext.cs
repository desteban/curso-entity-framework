using entity_framework.Models;
using Microsoft.EntityFrameworkCore;

namespace entity_framework.Context;

public class TareasContext : DbContext
{
    public DbSet<Categoria> categorias { get; set; }
    public DbSet<Tarea> tareas { get; set; }

    public TareasContext(DbContextOptions<TareasContext> options) : base(options) { }
}
