using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaLocadora.Models
{
    public class Filme_Ator
    {
        [Key]
        public int id { get; set; }
        public int idFilme { get; set; }

        public int idAtor { get; set; }
    }
}