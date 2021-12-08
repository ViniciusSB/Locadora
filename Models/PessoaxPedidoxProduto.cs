using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaLocadora.Models
{
    public class PessoaxPedidoxProduto
    {
        [Key]
        public int id { get; set; }

        public Cliente cliente { get; set; }

        public List<Filme> filmes { get; set; }

        public List<Serie> series { get; set; }

        public List<Jogo> jogos { get; set; }

        public int quantidadeDias { get; set; }

        public Pedido pedido { get; set; }

        public List<Pedido> pedidos { get; set; }

        public List<PedidoClienteQuantidade> pedidoClienteQuantidades { get; set; }
    }
}