using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaLocadora.Models
{
    public class Cliente
    {
        [Key]
        public int id { get; set; }

        public String nome { get; set; }

        public String email { get; set; }

        public String cpf { get; set; }

        public String dataNascimento { get; set; }
    }
}