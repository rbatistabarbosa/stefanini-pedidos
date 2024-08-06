using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stefanini.Pedidos.Core.Models;
using Stefanini.Pedidos.Infrastructure.Repositories.Contracts;

namespace Stefanini.Pedidos.Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly PedidoContext _context;

        public PedidoRepository(PedidoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pedido>> GetAllAsync()
        {
            return await _context.Pedidos.Include(p => p.ItensPedido).ThenInclude(ip => ip.Produto).ToListAsync();
        }

        public async Task<Pedido> GetByIdAsync(int id)
        {
            return await _context.Pedidos.Include(p => p.ItensPedido).ThenInclude(ip => ip.Produto).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Pedido pedido)
        {
            _context.Entry(pedido).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
                await _context.SaveChangesAsync();
            }
        }
    }
}
