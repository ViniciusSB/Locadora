using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaLocadora.Models
{
    public class Jogo: Produto
    {
        public String plataforma { get; set; }

        public String publicadora { get; set; }

        [NotMapped]
        public bool checkboxAnswer { get; set; }
    }
}