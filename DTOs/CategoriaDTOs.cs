using entity_framework.Models;
using System.ComponentModel.DataAnnotations;

namespace entity_framework.DTOs
{
    public class CreateCategoriaDTO
    {
        [Required]
        public string nombre { get; set; }

        public string descripcion { get; set; }

        [Required]
        public int peso { get; set; }

        public Categoria ToModel()
        {
            return new Categoria
            {
                nombre = nombre,
                descripcion = descripcion,
                peso = peso,
            };
        }
    }

    public class UpdateCategoriaDTO
    {
        [Required]
        public string nombre { get; set; }

        public string descripcion { get; set; }

        [Required]
        public int peso { get; set; }

        public Categoria ToModel()
        {
            return new Categoria
            {
                nombre = nombre,
                descripcion = descripcion,
                peso = peso,
            };
        }
    }
}
