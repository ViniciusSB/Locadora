using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaLocadora.Models
{
    public class Aluguel
    {
        [Key]
        public int id { get; set; }

        public int idPedido { get; set; }

        public int idFuncionario { get; set; }

        public Double valorTotal { get; set; }

        public String status { get; set; }

        public String dataEntrega { get; set; }

        public String dataLimiteEntrega { get; set; }
    }
}