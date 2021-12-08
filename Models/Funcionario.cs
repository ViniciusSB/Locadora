using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaLocadora.Models
{
    public class Funcionario
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Nome obrigatório")]
        public String nome { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "E-mail obrigatório")]
        public String email { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Senha obrigatório")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public String senha { get; set; }

        public String cpf { get; set; }

        [Display(Name = "Data de nascimento")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public String dataNascimento { get; set; }
    }
}