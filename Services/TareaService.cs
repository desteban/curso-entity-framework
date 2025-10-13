using Azure.Core;
using entity_framework.Context;
using entity_framework.DTOs;
using entity_framework.Models;
using entity_framework.Shared;
using Microsoft.EntityFrameworkCore;

namespace entity_framework.Services
{
    public class TareaService : ITareasService
    {
        private TareasContext dbTareasContext;
        private ICategoriaService categoriaService;
        private ILogger<TareaService> logger;
        public TareaService(TareasContext context, ILogger<TareaService> logger, ICategoriaService categoriaService)
        {
            dbTareasContext = context;
            this.logger = logger;
            this.categoriaService = categoriaService;
        }

        public async Task<IEnumerable<Tarea>> GetTasks()
        {
            return await dbTareasContext.tareas.Include(t => t.categoria).ToListAsync();
        }

        public async Task<Tarea?> GetTask(Guid id)
        {
            var tarea = await dbTareasContext.tareas
                .Include(t => t.categoria)
                .SingleOrDefaultAsync(t => t.tareaId == id);
            return tarea;
        }


        public async Task<Result<Tarea>> AddTask(NewTaskDTO newTask)
        {

            var categoria = categoriaService.GetById(newTask.categoiaId);
            if (categoria is null)
            {
                return Result<Tarea>.Failure("La categoria no es valida");
            }

            Tarea tarea = newTask.ToModel();
            tarea.tareaId = Guid.NewGuid();
            dbTareasContext.Add(tarea);
            int registrosAfectados = await dbTareasContext.SaveChangesAsync();

            if (registrosAfectados == 0)
            {
                return Result<Tarea>.Failure("No fue posible agregar la tarea");
            }

            return Result<Tarea>.Ok(tarea);
        }

        public async Task<Result<Tarea>> UpdateTask(Guid id, UpdateTaskDTO tarea)
        {
            var categoria = await categoriaService.GetById(tarea.categoiaId);
            if (categoria is null)
            {
                return Result<Tarea>.Failure("La categoría no es valida");
            }

            Tarea? currentTarea = await this.GetTask(id);
            if (currentTarea is null)
            {
                return Result<Tarea>.Failure("Tarea no encontrada");
            }

            currentTarea.titulo = tarea.titulo;
            currentTarea.descripcion = tarea.descripcion;
            currentTarea.categoiaId = tarea.categoiaId;
            currentTarea.estado = tarea.estado;

            if (currentTarea.estado == EstadoTarea.COMPLETADA)
            {
                currentTarea.fechaFinalizacion = DateTime.Now;
            }

            await dbTareasContext.SaveChangesAsync();
            return Result<Tarea>.Ok(currentTarea);
        }

    }

    public interface ITareasService
    {
        public Task<IEnumerable<Tarea>> GetTasks();
        public Task<Tarea?> GetTask(Guid id);
        public Task<Result<Tarea>> AddTask(NewTaskDTO newTask);
        public Task<Result<Tarea>> UpdateTask(Guid id, UpdateTaskDTO updateTask);
    }
}
