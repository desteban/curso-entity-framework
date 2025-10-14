using entity_framework.Context;
using entity_framework.DTOs;
using entity_framework.Models;
using entity_framework.Shared;
using Microsoft.EntityFrameworkCore;

namespace entity_framework.Services
{
    public class CategoriaService : ICategoriaService
    {
        private TareasContext dbTareaContext;

        public CategoriaService(TareasContext dbContex)
        {
            dbTareaContext = dbContex;
        }

        public async Task<Categoria?> GetById(Guid id)
        {
            Categoria? categoria = await dbTareaContext.categorias.FindAsync(id);
            return categoria;
        }

        public async Task<IEnumerable<Categoria>> GetAll()
        {
            var categorias = await dbTareaContext.categorias.ToListAsync();
            return categorias;
        }


        public async Task<Result<Categoria>> Create(CreateCategoriaDTO categoria)
        {
            Categoria newCategoria = categoria.ToModel();
            newCategoria.categoriaId = Guid.NewGuid();
            await dbTareaContext.AddAsync(newCategoria);
            int registros = await dbTareaContext.SaveChangesAsync();
            if (registros == 0)
            {
                return Result<Categoria>.Failure("No pudimos crear la categoria");
            }

            return Result<Categoria>.Ok(newCategoria);
        }
        public async Task<Result<Categoria>> Update(Guid id, UpdateCategoriaDTO update)
        {
            Categoria? categoria = await this.GetById(id);
            if (categoria is null)
            {
                return Result<Categoria>.Failure("La categoría no fue encontrada");
            }

            categoria.nombre = update.nombre;
            categoria.descripcion = update.descripcion;
            categoria.peso = update.peso;

            int registros = await dbTareaContext.SaveChangesAsync();
            if (registros == 0)
            {
                return Result<Categoria>.Failure("No pudimos actualizar la categoría");
            }

            return Result<Categoria>.Ok(categoria);
        }
    }

    public interface ICategoriaService
    {
        public Task<Categoria?> GetById(Guid id);
        public Task<IEnumerable<Categoria>> GetAll();
        public Task<Result<Categoria>> Create(CreateCategoriaDTO categoria);
        public Task<Result<Categoria>> Update(Guid id, UpdateCategoriaDTO update);
    }
}
