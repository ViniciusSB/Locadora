using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaLocadora.Models
{
    public class Ator
    {
        [Key]
        public int id { get; set; }

        public String nome { get; set; }

        public String dataNascimento { get; set; }

        [NotMapped]
        public bool checkboxAnswer { get; set; }
}
}