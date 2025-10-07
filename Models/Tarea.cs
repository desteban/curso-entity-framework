using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entity_framework.Models;

public class Tarea
{
    public Guid tareaId { get; set; }
    public Guid categoiaId { get; set; }
    public string titulo { get; set; }
    public string? descripcion { get; set; }
    public Prioridad prioridad { get; set; }
    public DateTime creacion { get; set; }
    public virtual Categoria categoria { get; set; }
    public string resumen { get; set; }
}


public enum Prioridad
{
    BAJA,
    MEDIA,
    ALTA,
}
