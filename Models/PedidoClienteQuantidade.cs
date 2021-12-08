using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaLocadora.Models
{
    public class PedidoClienteQuantidade
    {
        [Key]
        public int id { get; set; }

        public Pedido pedido { get; set; }

        public Cliente cliente { get; set; }

        public int quantidadeProdutos { get; set; }
    }
}