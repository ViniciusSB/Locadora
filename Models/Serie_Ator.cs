using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaLocadora.Models
{
    public class Serie_Ator
    {
        [Key]
        public int id { get; set; }
        public int idSerie { get; set; }

        public int idAtor { get; set; }
    }
}