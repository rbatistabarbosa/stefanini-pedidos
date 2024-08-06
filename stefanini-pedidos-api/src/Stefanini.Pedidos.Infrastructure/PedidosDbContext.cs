using Microsoft.EntityFrameworkCore;
using Stefanini.Pedidos.Core.Models;

namespace Stefanini.Pedidos.Infrastructure
{
    public class PedidoContext : DbContext
    {
        public PedidoContext(DbContextOptions<PedidoContext> options) : base(options) { }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração de Pedido
            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NomeCliente).IsRequired().HasMaxLength(60);
                entity.Property(e => e.EmailCliente).IsRequired().HasMaxLength(60);
                entity.Property(e => e.DataCriacao).IsRequired();
                entity.Property(e => e.Pago).IsRequired();
                entity.HasMany(e => e.ItensPedido)
                      .WithOne(e => e.Pedido)
                      .HasForeignKey(e => e.PedidoId);
            });

            // Configuração de Produto
            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NomeProduto).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Valor).IsRequired();
            });

            // Configuração de ItemPedido
            modelBuilder.Entity<ItemPedido>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Quantidade).IsRequired();
                entity.HasOne(e => e.Produto)
                      .WithMany()
                      .HasForeignKey(e => e.ProdutoId);
            });
        }
    }
}
