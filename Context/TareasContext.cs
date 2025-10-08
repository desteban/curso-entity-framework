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
            categoria.Property(c => c.nombre).IsRequired().HasMaxLength(200);
            categoria.Property(c => c.descripcion);
            categoria.Property(c => c.peso);

            categoria.HasData(this.SeederCategoria());
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
            tarea.Property(t => t.estado).HasDefaultValue(EstadoTarea.SIN_EMPESAR);
            tarea.Property(t => t.fechaFinalizacion);

            /* 
            Para crear la relación debemos utiliza la propiedad virtual
            esta nos dice que cada tarea tiene una categoría

            * Las relaciones las escribimos en un modelos y ya son validas para los demás
             */
            tarea.HasOne(t => t.categoria)
            .WithMany(c => c.tareas)
            .HasForeignKey(t => t.categoiaId);

            tarea.HasData(this.SeederTareas());
        });
    }

    private List<Categoria> SeederCategoria()
    {
        List<Categoria> categorias = new List<Categoria>();
        categorias.Add(new Categoria()
        {
            categoriaId = Guid.Parse("c3db2d1e-b1ff-43c4-acfe-257f49916ae2"),
            nombre = "En Casa",
            descripcion = "Tareas que debemos realizar en casa",
            peso = 2,
        });

        categorias.Add(new Categoria()
        {
            categoriaId = Guid.Parse("f5edfbfd-aa6c-4261-b24a-3ef3c85a5984"),
            nombre = "Ejercitar",
            descripcion = "Tareas que requieren un desgaste físico",
            peso = 5,
        });

        categorias.Add(new Categoria()
        {
            categoriaId = Guid.Parse("99eb1cae-a854-4503-a1ac-bf13c2453271"),
            nombre = "Estudiar",
            descripcion = "Estudiar o practicar alguna habilidad",
            peso = 3,
        });

        return categorias;
    }

    private List<Tarea> SeederTareas()
    {
        List<Tarea> tareas = new List<Tarea>();
        tareas.Add(new Tarea()
        {
            tareaId = Guid.Parse("2afee021-4f8a-4a60-9824-1703695b23c3"),
            categoiaId = Guid.Parse("99eb1cae-a854-4503-a1ac-bf13c2453271"),
            titulo = "Aprender .net",
            descripcion = "Realizar la ruta de desarrollo con .net",
            creacion = new DateTime(2025, 10, 7),
            estado = EstadoTarea.EN_CURSO,
            prioridad = Prioridad.ALTA,
        });

        return tareas;
    }
}
