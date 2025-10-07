using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entity_framework.Models;

[Table("tareas")]
public class Tarea
{
    [Key]
    public Guid tareaId { get; set; }

    [ForeignKey("categoriaId")]
    public Guid categoiaId { get; set; }

    [Required]
    [MaxLength(200)]
    public string titulo { get; set; }
    public string? descripcion { get; set; }
    public Prioridad prioridad { get; set; }

    [Required]
    public DateTime creacion { get; set; }

    public virtual Categoria categoria { get; set; }

    // NotMapped: Permite omitir atributos para que no los mapee en la db
    [NotMapped]
    public string resumen { get; set; }
}


public enum Prioridad
{
    BAJA,
    MEDIA,
    ALTA,
}
