using SistemaLocadora.DAO;
using SistemaLocadora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaLocadora.Controllers
{
    public class HomeController : Controller
    {
        LoginDAO loginDAO = new LoginDAO();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            if (Session["usuarioLogadoID"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Cadastro()
        {
            return RedirectToAction("Create", "Funcionarios");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Funcionario funcionario)
        {
            
            Funcionario func = loginDAO.verificarLogin(funcionario.email, funcionario.senha);
            if (func.id != null)
            {
                Session["usuarioLogadoID"] = func.id.ToString();
                int idFuncionario = Convert.ToInt32(Session["usuarioLogadoID"]);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Deslogar()
        {
            Session["usuarioLogadoID"] = null;
            return RedirectToAction("Login");
        }
    }
}