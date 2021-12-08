using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaLocadora.Models;

namespace SistemaLocadora.Controllers
{
    public class JogoesController : Controller
    {
        private Context db = new Context();

        // GET: Jogoes
        public ActionResult Index()
        {
            return View(db.Jogoes.ToList());
        }

        // GET: Jogoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jogo jogo = db.Jogoes.Find(id);
            if (jogo == null)
            {
                return HttpNotFound();
            }
            return View(jogo);
        }

        // GET: Jogoes/Create
        public ActionResult Create()
        {
            if (Session["usuarioLogadoID"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
            
        }

        // POST: Jogoes/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nome,dataLancamento,classificacaoIndicativa,genero,sinopse,tipo,valor,quantidadeTotal,quantidadeDisponivel,plataforma,publicadora")] Jogo jogo)
        {
            if (ModelState.IsValid)
            {
                jogo.tipo = "jogo";
                db.Produtoes.Add(jogo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jogo);
        }

        // GET: Jogoes/Edit/5
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
                Jogo jogo = db.Jogoes.Find(id);
                if (jogo == null)
                {
                    return HttpNotFound();
                }
                return View(jogo);
            }
        }

        // POST: Jogoes/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nome,dataLancamento,classificacaoIndicativa,genero,sinopse,tipo,valor,quantidadeTotal,quantidadeDisponivel,plataforma,publicadora")] Jogo jogo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jogo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jogo);
        }

        // GET: Jogoes/Delete/5
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
                Jogo jogo = db.Jogoes.Find(id);
                if (jogo == null)
                {
                    return HttpNotFound();
                }
                return View(jogo);
            }
        }

        // POST: Jogoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jogo jogo = db.Jogoes.Find(id);
            db.Produtoes.Remove(jogo);
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
