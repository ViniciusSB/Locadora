using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaLocadora.Models
{
    public class AluguelxPedidoxFuncxCliente
    {
        [Key]
        public int id { get; set; }

        public int idCliente { get; set; }


        public int idFuncionario { get; set; }

        public Cliente cliente { get; set; }

        public List<Filme> filmes { get; set; }

        public List<Serie> series { get; set; }

        public List<Jogo> jogos { get; set; }

        public Pedido pedido { get; set; }

        public List<Pedido> pedidos { get; set; }

        public PessoaxPedidoxProduto ppp { get; set; }

        public List<Produto> produtos { get; set; }

        public Produto produto { get; set; }

        public Double valorTotal { get; set; }

        public String status { get; set; }

        public String dataEntrega { get; set; }

        public String dataLimiteEntrega { get; set; }
    }
}