using System.Collections.Generic;
using System;

namespace Stefanini.Pedidos.Core.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string NomeProduto { get; set; }
        public decimal Valor { get; set; }
    }
}