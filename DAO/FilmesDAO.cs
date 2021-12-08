using SistemaLocadora.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaLocadora.DAO
{
    public class FilmesDAO
    {
        private Context db = new Context();

        public void salvarFilmexAtor(int[] idsAtores, int idFilme)
        {
            foreach (int id in idsAtores)
            {
                Filme_Ator filme_Ator = new Filme_Ator();
                filme_Ator.idAtor = id;
                filme_Ator.idFilme = idFilme;
                db.Filme_Ator.Add(filme_Ator);
                db.SaveChanges();
            }
        }

        public void editarFilmexAtor(int[] idsAtores, int idFilme)
        {
            db.Filme_Ator.SqlQuery("Delete * from dbo.Filme_Ator where id = @id", new SqlParameter("@id", idFilme));
            List<Filme_Ator> filme_Ators = db.Filme_Ator.SqlQuery("Select * from dbo.Filme_Ator where idFilme = @id", new SqlParameter("@id", idFilme)).ToList();
            foreach (Filme_Ator fa in filme_Ators)
            {
                db.Filme_Ator.Remove(fa);
                db.SaveChanges();
            }
            if (idsAtores != null)
            {
                foreach (int id in idsAtores)
                {
                    Filme_Ator filme_Ator = new Filme_Ator();
                    filme_Ator.idAtor = id;
                    filme_Ator.idFilme = idFilme;
                    db.Filme_Ator.Add(filme_Ator);
                    db.SaveChanges();
                }
            }
        }

        public List<int> buscarAtoresFilme(int? idFilme)
        {
            List<Filme_Ator> filme_Ators = db.Filme_Ator.SqlQuery("Select * from dbo.Filme_Ator where idFilme = @id", new SqlParameter("@id", idFilme)).ToList();
            List<int> atores = new List<int>();
            foreach (Filme_Ator films in filme_Ators)
            {
                atores.Add(films.idAtor);
            }
            return atores;
        }
    }
}