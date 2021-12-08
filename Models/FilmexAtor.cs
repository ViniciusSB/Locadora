using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaLocadora.Models
{
    public class FilmexAtor
    {

        public List<Filme> filmes { get; set; }

        public Filme filme { get; set; }

        public List<Ator> atores { get; set; }

        public Filter[] filters { get; set; }

        [NotMapped]
        public Ator ator { get; set; }
    }
}