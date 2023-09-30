using Domain.Entities;
using Domain.Interfaces.Repository;

namespace Data.Repository
{
    public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
    {
        public ProdutoRepository()
        {
        }
    }
}