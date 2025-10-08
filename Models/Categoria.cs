using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace entity_framework.Models;

public class Categoria
{
    public Guid categoriaId { get; set; }

    public string nombre { get; set; }

    public string? descripcion { get; set; }

    // El costo (esfuerzo) de la tarea
    public int peso { get; set; }

    [JsonIgnore]
    public virtual ICollection<Tarea> tareas { get; set; }
}
