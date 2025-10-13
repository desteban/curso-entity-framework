using entity_framework.DTOs;
using entity_framework.Models;
using entity_framework.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Net;

namespace entity_framework.Controllers
{
    [Route("/api/tareas")]
    public class TaskController : Controller
    {
        private ITareasService tareaService;

        public TaskController(ITareasService service)
        {
            tareaService = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Tarea>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var tareas = await tareaService.GetTasks();
            return Ok(tareas);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Tarea), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Nullable), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Find(Guid id)
        {
            Tarea? tarea = await this.tareaService.GetTask(id);
            if (tarea is null)
            {
                return NotFound(null);
            }

            return Ok(tarea);
        }

        [HttpPost()]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] NewTaskDTO create)
        {

            var tarea = await tareaService.AddTask(create);
            if (tarea.isSuccess == false)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, null);
            }

            return Created("", tarea.Value);
        }

        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateTarea(Guid id, [FromBody] UpdateTaskDTO update)
        {
            var updated = await this.tareaService.UpdateTask(id, update);
            if(updated.isSuccess == false)
            {
                return NotFound(updated.ErrorMessage);
            }

            return Ok("Tarea modificada con exito");
        }
    }
}
