using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaLocadora.Models
{
    public class Pedido
    {
        [Key]
        public int id { get; set; }

        public int idCliente { get; set; }

        public int  idProduto { get; set; }

        public int quantidadeDias { get; set; }

        public int vezesAlugadas { get; set; }

        [NotMapped]
        public bool checkboxAnswer { get; set; }
    }
}