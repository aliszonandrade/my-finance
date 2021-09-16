using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class PlanoContaController : Controller
    {
        IHttpContextAccessor HttpContextAccessor { get; set; }

        public PlanoContaController(IHttpContextAccessor httpContext)
        {
            HttpContextAccessor = httpContext;
        }

        public IActionResult Index()
        {
            PlanoContaModel planoConta = new PlanoContaModel(HttpContextAccessor);
            ViewBag.ListaPlanoContas = planoConta.ListaPlanoContas();
            return View();
        }

        [HttpPost]
        public IActionResult CriarPlanoConta(PlanoContaModel planoContaModel, int? id)
        {
            if (ModelState.IsValid)
            {
                planoContaModel.Usuario_Id = int.Parse(HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado"));
                planoContaModel.Inserir(id);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult CriarPlanoConta(int? id)
        {
            if(id != null)
            {
                PlanoContaModel planoContaModel = new PlanoContaModel(HttpContextAccessor);
                ViewBag.Registro = planoContaModel.CarregarRegistro(id);
            }
            return View();
        }

        [HttpGet]
        public IActionResult ExcluirPlanoConta(int id)
        {
            new PlanoContaModel().Excluir(id);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult EditarPlanoConta(int id)
        {
            new PlanoContaModel().Excluir(id);
            return RedirectToAction("Index");
        }

    }
}