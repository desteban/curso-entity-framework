using entity_framework.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace entity_framework.DTOs
{
    public class NewTaskDTO
    {

        [Required]
        public Guid categoiaId { get; set; }

        [Required]
        public string titulo { get; set; }

        public string? descripcion { get; set; }

        [Required]
        public Prioridad prioridad { get; set; }

        public DateTime creacion { get; set; }

        [Required]
        public EstadoTarea estado { get; set; }

        public Tarea ToModel()
        {
            return new Tarea
            {
                categoiaId = this.categoiaId,
                creacion = DateTime.Now,
                titulo = this.titulo,
                descripcion = this.descripcion,
                estado = EstadoTarea.SIN_EMPESAR,
                prioridad = this.prioridad,
            };
        }
    }

    public class UpdateTaskDTO
    {

        [Required]
        public Guid categoiaId { get; set; }

        [Required]
        public string titulo { get; set; }

        public string? descripcion { get; set; }

        [Required]
        public Prioridad prioridad { get; set; }

        public DateTime creacion { get; set; }

        [Required]
        public EstadoTarea estado { get; set; }
    }
}
