using System.Collections.Generic;
using System;

namespace Stefanini.Pedidos.Core.Models
{
    public class Pedido
    {
        public Pedido()
        {
            ItensPedido = new List<ItemPedido>();
        }

        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public string EmailCliente { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Pago { get; set; }

        public IList<ItemPedido> ItensPedido { get; set; }
    }
}