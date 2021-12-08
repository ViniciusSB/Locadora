using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaLocadora.Models
{
    public class Produto
    {
        [Key]
        public int id { get; set; }

        public String nome { get; set; }

        public String dataLancamento { get; set; }

        public int classificacaoIndicativa { get; set; }

        public String genero { get; set; }

        public String sinopse { get; set; }

        public String tipo { get; set; }

        public Double valor { get; set; }

        public int quantidadeTotal { get; set; }

        public int quantidadeDisponivel { get; set; }

        public int getId ()
        {
            return this.id;
        }
    }
}