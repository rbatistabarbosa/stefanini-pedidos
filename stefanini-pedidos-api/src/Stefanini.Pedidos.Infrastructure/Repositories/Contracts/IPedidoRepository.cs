using System.Collections.Generic;
using System.Threading.Tasks;
using Stefanini.Pedidos.Core.Models;

namespace Stefanini.Pedidos.Infrastructure.Repositories.Contracts
{
    public interface IPedidoRepository
    {
        Task<IEnumerable<Pedido>> GetAllAsync();
        Task<Pedido> GetByIdAsync(int id);
        Task AddAsync(Pedido pedido);
        Task UpdateAsync(Pedido pedido);
        Task DeleteAsync(int id);
    }
}
