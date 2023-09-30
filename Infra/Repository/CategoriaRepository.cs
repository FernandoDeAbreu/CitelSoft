using Domain.Entities;
using Domain.Interfaces.Repository;

namespace Data.Repository
{
    public class CategoriaRepository : RepositoryBase<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository()
        {
        }
    }
}