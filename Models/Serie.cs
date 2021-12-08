using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaLocadora.Models
{
    public class Serie: Produto
    {
        public int temporada { get; set; }

        [NotMapped]
        public bool checkboxAnswer { get; set; }

    }
}