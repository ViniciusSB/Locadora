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
    public class FilmesController : Controller
    {
        private Context db = new Context();
        InsertDAO insertDAO = new InsertDAO();
        FilmesDAO filmesDAO = new FilmesDAO();

        // GET: Filmes
        public ActionResult Index()
        {
            FilmexAtor filmexAtor = new FilmexAtor();
            filmexAtor.atores = db.Ators.ToList();
            filmexAtor.filmes = db.filmes.ToList();
            return View(filmexAtor.filmes);
        }

        // GET: Filmes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filme filme = db.filmes.Find(id);
            if (filme == null)
            {
                return HttpNotFound();
            }
            return View(filme);
        }

        // GET: Filmes/Create
        public ActionResult Create()
        {
            if (Session["usuarioLogadoID"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                FilmexAtor filmexAtor = new FilmexAtor();
                filmexAtor.atores = db.Ators.ToList();
                return View(filmexAtor);
            }
        }

        // POST: Filmes/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nome,dataLancamento,classificacaoIndicativa,genero,sinopse,tipo,valor,quantidadeTotal,quantidadeDisponivel,duracao")] Filme filme, int[] AreChecked)
        {
            if (ModelState.IsValid)
            {
                filme.tipo = "filme";
                db.Produtoes.Add(filme);
                db.SaveChanges();
                filmesDAO.salvarFilmexAtor(AreChecked, filme.id);
                return RedirectToAction("Index");
            }

            return View(filme);
        }

        // GET: Filmes/Edit/5
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
                FilmexAtor filmexAtor = new FilmexAtor();
                filmexAtor.atores = db.Ators.ToList();
                List<int> atoresSelecionados = filmesDAO.buscarAtoresFilme(id);
                foreach (Ator ator in filmexAtor.atores)
                {
                    foreach (int intID in atoresSelecionados)
                    {
                        if (ator.id == intID)
                        {
                            ator.checkboxAnswer = true;
                        }
                    }
                }
                filmexAtor.filme = db.filmes.Find(id);

                if (filmexAtor.filme == null)
                {
                    return HttpNotFound();
                }
                return View(filmexAtor);
            }
        }

        // POST: Filmes/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nome,dataLancamento,classificacaoIndicativa,genero,sinopse,tipo,valor,quantidadeTotal,quantidadeDisponivel,duracao")] Filme filme, int[] AreChecked)
        {
            if (ModelState.IsValid)
            {
                db.Entry(filme).State = EntityState.Modified;
                db.SaveChanges();
                filmesDAO.editarFilmexAtor(AreChecked, filme.id);
                return RedirectToAction("Index");
            }
            FilmexAtor filmexAtor = new FilmexAtor();
            filmexAtor.filme = filme;
            return View(filme);
        }

        // GET: Filmes/Delete/5
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
                Filme filme = db.filmes.Find(id);
                if (filme == null)
                {
                    return HttpNotFound();
                }
                return View(filme);
            }
        }

        // POST: Filmes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Filme filme = db.filmes.Find(id);
            db.Produtoes.Remove(filme);
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
