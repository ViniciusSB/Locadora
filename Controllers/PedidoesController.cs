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
    public class PedidoesController : Controller
    {
        private Context db = new Context();
        PedidoDAO pedidoDAO = new PedidoDAO();

        // GET: Pedidoes
        public ActionResult Index()
        {
            PessoaxPedidoxProduto ppp = new PessoaxPedidoxProduto();
            ppp.pedidos = pedidoDAO.listarPedidosPorUsuarioPedido();
            ppp.pedidoClienteQuantidades = new List<PedidoClienteQuantidade>();
            if (ppp.pedidos != null)
            {
                foreach(Pedido p in ppp.pedidos)
                {
                    Cliente cliente = db.Clientes.Find(p.idCliente);
                    int numero = pedidoDAO.buscarQuantidadePedidosById(p.id);
                    PedidoClienteQuantidade pcq = new PedidoClienteQuantidade();
                    pcq.cliente = cliente;
                    pcq.pedido = p;
                    pcq.quantidadeProdutos = numero;
                    ppp.pedidoClienteQuantidades.Add(pcq);
                }
            }
            return View(ppp);
        }

        // GET: Pedidoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidoes.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // GET: Pedidoes/Create
        public ActionResult Create()
        {
            if (Session["usuarioLogadoID"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                PessoaxPedidoxProduto ppp = new PessoaxPedidoxProduto();
                ppp.series = db.Series.ToList();
                ppp.filmes = db.filmes.ToList();
                ppp.jogos = db.Jogoes.ToList();
                List<Cliente> clientes = db.Clientes.ToList();
                ViewBag.Clientes = new SelectList(clientes, "id", "nome");
                return View(ppp);
            }
        }

        // POST: Pedidoes/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,quantidadeDias")] Pedido pedido, int[] filmesChecked, int[] seriesChecked, int[] jogosChecked, PessoaxPedidoxProduto ppp)
        {
            if (ModelState.IsValid)
            {
                pedidoDAO.salvarPedido(ppp.cliente.id, pedido, filmesChecked, seriesChecked, jogosChecked);
                return RedirectToAction("Index");
            }

            return View(pedido);
        }

        // GET: Pedidoes/Edit/5
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
                PessoaxPedidoxProduto ppp = new PessoaxPedidoxProduto();
                ppp.series = db.Series.ToList();
                ppp.filmes = db.filmes.ToList();
                ppp.jogos = db.Jogoes.ToList();
                List<int> seriesSelecionadas = pedidoDAO.buscarSeriesPedidos(id);
                foreach (Serie serie in ppp.series)
                {
                    foreach (int intID in seriesSelecionadas)
                    {
                        if (serie.id == intID)
                        {
                            serie.checkboxAnswer = true;
                        }
                    }
                }
                List<int> jogosSelecionados = pedidoDAO.buscarJogosPedidos(id);
                foreach (Jogo jogo in ppp.jogos)
                {
                    foreach (int intID in jogosSelecionados)
                    {
                        if (jogo.id == intID)
                        {
                            jogo.checkboxAnswer = true;
                        }
                    }
                }
                List<int> filmesSelecionadas = pedidoDAO.buscarFilmesPedidos(id);
                foreach (Filme filme in ppp.filmes)
                {
                    foreach (int intID in filmesSelecionadas)
                    {
                        if (filme.id == intID)
                        {
                            filme.checkboxAnswer = true;
                        }
                    }
                }
                Pedido pedido = db.Pedidoes.Find(id);
                ppp.pedido = pedido;
                List<Cliente> clientes = db.Clientes.ToList();
                ViewBag.Clientes = new SelectList(clientes, "id", "nome", pedido.idCliente);
                if (pedido == null)
                {
                    return HttpNotFound();
                }
                return View(ppp);
            }
        }

        // POST: Pedidoes/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,quantidadeDias")] Pedido pedido, int[] filmesChecked, int[] seriesChecked, int[] jogosChecked, PessoaxPedidoxProduto ppp)
        {
            if (ModelState.IsValid)
            {
                pedidoDAO.editarPedido(ppp.cliente.id, pedido, filmesChecked, seriesChecked, jogosChecked);
                return RedirectToAction("Index");
            }
            return View(pedido);
        }

        // GET: Pedidoes/Delete/5
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
                Pedido pedido = db.Pedidoes.Find(id);
                if (pedido == null)
                {
                    return HttpNotFound();
                }
                return View(pedido);
            }
        }

        // POST: Pedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pedido pedido = db.Pedidoes.Find(id);
            pedidoDAO.deletarPedidos(pedido.idCliente, pedido);
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
