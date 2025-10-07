using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entity_framework.Models;

[Table("categorias")]
public class Categoria
{
    /* 
    Nombre de la clase + id: Se detecta automáticamente como el id
     */
    [Key]
    public Guid categoriaId { get; set; }

    [Required]
    [MaxLength(150)]
    public string nombre { get; set; }

    public string? descripcion { get; set; }
    public virtual ICollection<Tarea> tareas { get; set; }
}
