using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Stefanini.Pedidos.Core.Models;
using Stefanini.Pedidos.Infrastructure.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Stefanini.Pedidos.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly PedidoContext _context;

        public ProdutoRepository(PedidoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            return await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddRandomProductsAsync(int count)
        {
            var random = new Random();
            for (int i = 0; i < count; i++)
            {
                _context.Produtos.Add(new Produto
                {
                    NomeProduto = $"Produto {i + 1}",
                    Valor = (decimal)(random.NextDouble() * 100)
                });
            }
            await _context.SaveChangesAsync();
        }
    }
}
