using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaLocadora.DAO;
using SistemaLocadora.Models;

namespace SistemaLocadora.Controllers
{
    public class SeriesController : Controller
    {
        private Context db = new Context();
        SerieDAO serieDAO = new SerieDAO();

        // GET: Series
        public ActionResult Index()
        {
            SeriexAtor seriexAtor = new SeriexAtor();
            seriexAtor.series = db.Series.ToList();
            seriexAtor.atores = db.Ators.ToList();
            return View(seriexAtor.series);
        }

        // GET: Series/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Serie serie = db.Series.Find(id);
            if (serie == null)
            {
                return HttpNotFound();
            }
            return View(serie);
        }

        // GET: Series/Create
        public ActionResult Create()
        {
            if (Session["usuarioLogadoID"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                SeriexAtor seriexAtor = new SeriexAtor();
                seriexAtor.atores = db.Ators.ToList();
                return View(seriexAtor);
            }
        }

        // POST: Series/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nome,dataLancamento,classificacaoIndicativa,genero,sinopse,tipo,valor,quantidadeTotal,quantidadeDisponivel,temporada")] Serie serie, int[] AreChecked)
        {
            if (ModelState.IsValid)
            {
                serie.tipo = "serie";
                db.Produtoes.Add(serie);
                db.SaveChanges();
                serieDAO.salvarSeriexAtor(AreChecked, serie.id);
                return RedirectToAction("Index");
            }

            return View(serie);
        }

        // GET: Series/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["usuarioLogadoID"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SeriexAtor seriexAtor = new SeriexAtor();
                seriexAtor.atores = db.Ators.ToList();
                List<int> atoresSelecionados = serieDAO.buscarAtoresSerie(id);
                foreach (Ator ator in seriexAtor.atores)
                {
                    foreach (int intID in atoresSelecionados)
                    {
                        if (ator.id == intID)
                        {
                            ator.checkboxAnswer = true;
                        }
                    }
                }
                seriexAtor.serie = db.Series.Find(id);
                if (seriexAtor.serie == null)
                {
                    return HttpNotFound();
                }
                return View(seriexAtor);
            }
        }

        // POST: Series/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nome,dataLancamento,classificacaoIndicativa,genero,sinopse,tipo,valor,quantidadeTotal,quantidadeDisponivel,temporada")] Serie serie, int[] AreChecked)
        {
            if (ModelState.IsValid)
            {
                serie.tipo = "serie";
                db.Entry(serie).State = EntityState.Modified;
                db.SaveChanges();
                serieDAO.editarFilmexAtor(AreChecked, serie.id);
                return RedirectToAction("Index");
            }
            return View(serie);
        }

        // GET: Series/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["usuarioLogadoID"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Serie serie = db.Series.Find(id);
                if (serie == null)
                {
                    return HttpNotFound();
                }
                return View(serie);
            }
           
        }

        // POST: Series/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Serie serie = db.Series.Find(id);
            db.Produtoes.Remove(serie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
