using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SistemaLocadora.Models
{
    public class Context: DbContext
    {
        public System.Data.Entity.DbSet<SistemaLocadora.Models.Filme> filmes { get; set; }

        public System.Data.Entity.DbSet<SistemaLocadora.Models.Serie> Series { get; set; }

        public System.Data.Entity.DbSet<SistemaLocadora.Models.Jogo> Jogoes { get; set; }

        public System.Data.Entity.DbSet<SistemaLocadora.Models.Produto> Produtoes { get; set; }

        public System.Data.Entity.DbSet<SistemaLocadora.Models.Ator> Ators { get; set; }

        public System.Data.Entity.DbSet<SistemaLocadora.Models.Filme_Ator> Filme_Ator { get; set; }

        public System.Data.Entity.DbSet<SistemaLocadora.Models.Serie_Ator> Serie_Ator { get; set; }

        public System.Data.Entity.DbSet<SistemaLocadora.Models.Cliente> Clientes { get; set; }

        public System.Data.Entity.DbSet<SistemaLocadora.Models.Funcionario> Funcionarios { get; set; }

        public System.Data.Entity.DbSet<SistemaLocadora.Models.Pedido> Pedidoes { get; set; }

        public System.Data.Entity.DbSet<SistemaLocadora.Models.Aluguel> Aluguels { get; set; }
    }
}