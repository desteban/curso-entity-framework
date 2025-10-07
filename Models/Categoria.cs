using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entity_framework.Models;

public class Categoria
{
    public Guid categoriaId { get; set; }

    public string nombre { get; set; }

    public string? descripcion { get; set; }
    public virtual ICollection<Tarea> tareas { get; set; }
}
