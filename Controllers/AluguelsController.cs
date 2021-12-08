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
    public class AluguelsController : Controller
    {
        private Context db = new Context();
        AluguelDAO aluguelDAO = new AluguelDAO();
        ProdutoDAO produtoDAO = new ProdutoDAO();

        // GET: Aluguels
        public ActionResult Index()
        {
            return View(db.Aluguels.ToList());
        }

        // GET: Aluguels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluguel aluguel = db.Aluguels.Find(id);
            if (aluguel == null)
            {
                return HttpNotFound();
            }
            return View(aluguel);
        }

        // GET: Aluguels/Create
        public ActionResult Create()
        {
            if (Session["usuarioLogadoID"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                AluguelxPedidoxFuncxCliente apfc = new AluguelxPedidoxFuncxCliente();
                apfc = aluguelDAO.buscarDados();
                List<Pedido> pedidos = apfc.pedidos;
                ViewBag.Pedidos = new SelectList(pedidos, "id", "quantidadeDias");
                return View(apfc);
            }
        }

        // POST: Aluguels/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,idPedido,idFuncionario,valorTotal,status,dataEntrega,dataLimiteEntrega")] Aluguel aluguel, AluguelxPedidoxFuncxCliente apfc)
        {
            if (ModelState.IsValid)
            {
                if (apfc.pedido != null)
                {
                    Session["idPedido"] = apfc.pedido.id;
                }
                return RedirectToAction("Calculo", new { idPedido = Convert.ToInt32(Session["idPedido"].ToString()) });
            }   

            return View(aluguel);
        }

        public ActionResult Calculo(int idPedido)
        {
            List<String> status = new List<string>();
            status.Add("Aguardando pagamento");
            status.Add("Aguardando retorno");
            status.Add("Finalizado");
            ViewBag.Status = new SelectList(status);
            AluguelxPedidoxFuncxCliente apfc = aluguelDAO.calcularDados(idPedido);
            return View(apfc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Calculo(AluguelxPedidoxFuncxCliente apfc)
        {
            AluguelxPedidoxFuncxCliente apfcc = aluguelDAO.calcularDados(Convert.ToInt32(Session["idPedido"].ToString()));
            Aluguel aluguel = new Aluguel();
            aluguel.idPedido = Convert.ToInt32(Session["idPedido"].ToString());
            aluguel.idFuncionario = Convert.ToInt32(Session["usuarioLogadoID"].ToString());
            aluguel.valorTotal = apfc.valorTotal;
            aluguel.status = apfc.status;
            aluguel.dataEntrega = apfc.dataEntrega;
            aluguel.dataLimiteEntrega = apfc.dataLimiteEntrega;
            db.Aluguels.Add(aluguel);
            db.SaveChanges();
            produtoDAO.reduzirQuantidade(apfcc.produtos);
            Session["idPedido"] = null;
            return RedirectToAction("Index");
        }

        // GET: Aluguels/Edit/5
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
                Aluguel aluguel = db.Aluguels.Find(id);
                if (aluguel == null)
                {
                    return HttpNotFound();
                }
                Aluguel alu = new Aluguel();
                alu = db.Aluguels.Find(id);
                AluguelxPedidoxFuncxCliente apfc = aluguelDAO.calcularDados(alu.idPedido);
                apfc.pedido = db.Pedidoes.Find(alu.idPedido);
                apfc.idFuncionario = Convert.ToInt32(Session["usuarioLogadoID"].ToString());
                apfc.valorTotal = apfc.valorTotal;
                apfc.status = alu.status;
                apfc.dataEntrega = apfc.dataEntrega;
                apfc.dataLimiteEntrega = apfc.dataLimiteEntrega;
                List<String> status = new List<string>();
                status.Add("Aguardando pagamento");
                status.Add("Aguardando retorno");
                status.Add("Finalizado");
                ViewBag.Status = new SelectList(status, apfc.status);
                return View(apfc);
            }
        }

        // POST: Aluguels/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idPedido,idFuncionario,valorTotal,status,dataEntrega,dataLimiteEntrega")] Aluguel aluguel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aluguel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aluguel);
        }

        // GET: Aluguels/Delete/5
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
                Aluguel aluguel = db.Aluguels.Find(id);
                if (aluguel == null)
                {
                    return HttpNotFound();
                }
                return View(aluguel);
            }
            
        }

        // POST: Aluguels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Aluguel aluguel = db.Aluguels.Find(id);
            db.Aluguels.Remove(aluguel);
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
