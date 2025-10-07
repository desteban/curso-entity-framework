using entity_framework.Models;
using Microsoft.EntityFrameworkCore;

namespace entity_framework.Context;

public class TareasContext : DbContext
{
    public DbSet<Categoria> categorias { get; set; }
    public DbSet<Tarea> tareas { get; set; }

    public TareasContext(DbContextOptions<TareasContext> options) : base(options) { }

    // Para usar un override debemos usar un protected
    protected override void OnModelCreating(ModelBuilder model)
    {
        this.ModelCategoria(model);
        this.ModelTarea(model);
    }

    private void ModelCategoria(ModelBuilder model)
    {
        model.Entity<Categoria>((categoria) =>
        {
            categoria.ToTable("categoria");
            categoria.HasKey(c => c.categoriaId);
            categoria.Property(c => c.nombre).IsRequired().HasMaxLength(150);
            categoria.Property(c => c.descripcion);

            /* 
            Podemos crear algunos seeders
             */
            categoria.HasData(
                new Categoria
                {
                    categoriaId = Guid.NewGuid(),
                    nombre = "Pendientes",
                    descripcion = "Tareas que requieren atención."
                });
        });
    }

    private void ModelTarea(ModelBuilder model)
    {
        model.Entity<Tarea>(tarea =>
        {
            tarea.ToTable("tarea");
            tarea.HasKey(t => t.tareaId);
            tarea.Property(t => t.titulo).IsRequired().HasMaxLength(200);
            tarea.Property(t => t.creacion).IsRequired();
            tarea.Property(t => t.prioridad);
            tarea.Ignore(t => t.resumen);

            /* 
            Para crear la relación debemos utiliza la propiedad virtual
            esta nos dice que cada tarea tiene una categoría

            * Las relaciones las escribimos en un modelos y ya son validas para los demás
             */
            tarea.HasOne(t => t.categoria)
            .WithMany(c => c.tareas)
            .HasForeignKey(t => t.categoiaId);
        });
    }
}
