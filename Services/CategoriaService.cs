using entity_framework.Context;
using entity_framework.Models;

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

    }

    public interface ICategoriaService
    {
        public Task<Categoria?> GetById(Guid id);
    }
}
