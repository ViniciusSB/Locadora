using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaLocadora.Models
{
    public class SeriexAtor
    {
        public Ator ator { get; set; }

        public Serie serie { get; set; }

        public List<Serie> series { get; set; }

        public List<Ator> atores { get; set; }
    }
}