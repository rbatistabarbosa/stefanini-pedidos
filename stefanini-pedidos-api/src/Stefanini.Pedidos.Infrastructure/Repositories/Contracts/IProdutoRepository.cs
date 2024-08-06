using System.Collections.Generic;
using System.Threading.Tasks;
using Stefanini.Pedidos.Core.Models;

namespace Stefanini.Pedidos.Infrastructure.Repositories.Contracts
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetAllAsync();
        Task<Produto> GetByIdAsync(int id);
        Task AddRandomProductsAsync(int count);
    }
}
