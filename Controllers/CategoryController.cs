using entity_framework.DTOs;
using entity_framework.Models;
using entity_framework.Services;
using entity_framework.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace entity_framework.Controllers
{
    [Route("/api/categorias")]
    public class CategoryController : Controller
    {
        private ICategoriaService categoriaService;
        private string baseUrl = "/api/categorias";

        public CategoryController(ICategoriaService categoriaService)
        {
            this.categoriaService = categoriaService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Categoria>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> List()
        {
            var categories = await this.categoriaService.GetAll();
            return Ok(categories);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Categoria), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] CreateCategoriaDTO dto)
        {
            Result<Categoria> categoria = await this.categoriaService.Create(dto);
            if (categoria.isSuccess == false)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, categoria.ErrorMessage);
            }

            return Created($"{baseUrl}/{categoria.Value.categoriaId}", categoria.Value);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Categoria), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Find(Guid id)
        {
            Categoria categoria = await this.categoriaService.GetById(id);
            if (categoria is null)
            {
                return NotFound("Categoria no encontrada");
            }

            return Ok(categoria);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Categoria), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoriaDTO dto)
        {
            Result<Categoria> categoria = await this.categoriaService.Update(id, dto);
            if (categoria.isSuccess == false)
            {
                return NotFound(categoria.ErrorMessage);
            }

            return Ok(categoria.Value);
        }
    }
}
