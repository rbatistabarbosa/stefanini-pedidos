using System.Collections.Generic;

namespace Stefanini.Pedidos.Api.Dtos
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public string EmailCliente { get; set; }
        public bool Pago { get; set; }
        public decimal ValorTotal { get; set; }
        public ICollection<ItemPedidoDto> ItensPedido { get; set; }
    }
}
