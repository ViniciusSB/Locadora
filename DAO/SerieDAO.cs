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
    public class SerieDAO
    {
        private Context db = new Context();

        public void salvarSeriexAtor(int[] idsAtores, int idSerie)
        {
            if (idsAtores != null)
            {
                foreach (int id in idsAtores)
                {
                    Serie_Ator serie_Ator = new Serie_Ator();
                    serie_Ator.idAtor = id;
                    serie_Ator.idSerie = idSerie;
                    db.Serie_Ator.Add(serie_Ator);
                    db.SaveChanges();
                }
            } 
            else
            {
                Serie_Ator serie_Ator = new Serie_Ator();
                serie_Ator.idSerie = idSerie;
                db.Serie_Ator.Add(serie_Ator);
                db.SaveChanges();
            }
            
        }

        public void editarFilmexAtor(int[] idsAtores, int idSerie)
        {
            List<Serie_Ator> serie_Ators = db.Serie_Ator.SqlQuery("Select * from dbo.Serie_Ator where idSerie = @id", new SqlParameter("@id", idSerie)).ToList();
            foreach (Serie_Ator sa in serie_Ators)
            {
                db.Serie_Ator.Remove(sa);
                db.SaveChanges();
            }
            if (idsAtores != null)
            {
                foreach (int id in idsAtores)
                {
                    Serie_Ator serie_Ator = new Serie_Ator();
                    serie_Ator.idAtor = id;
                    serie_Ator.idSerie = idSerie;
                    db.Serie_Ator.Add(serie_Ator);
                    db.SaveChanges();
                }
            }
        }

        public List<int> buscarAtoresSerie(int? idSerie)
        {
            List<Serie_Ator> serie_Ators = db.Serie_Ator.SqlQuery("Select * from dbo.Serie_Ator where idSerie = @id", new SqlParameter("@id", idSerie)).ToList();
            List<int> atores = new List<int>();
            foreach (Serie_Ator series in serie_Ators)
            {
                atores.Add(series.idAtor);
            }
            return atores;
        }
    }
}