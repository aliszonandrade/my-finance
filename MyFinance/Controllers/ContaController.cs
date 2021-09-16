    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class ContaController : Controller
    {
        IHttpContextAccessor HttpContextAccessor { get; set; }

        public ContaController(IHttpContextAccessor httpContext)
        {
            HttpContextAccessor = httpContext;
        }

        public IActionResult Index()
        {
            ContaModel conta = new ContaModel(HttpContextAccessor);
            ViewBag.ListaConta = conta.ListaConta();
            return View();
        }

        [HttpPost]
        public IActionResult CriarConta(ContaModel contaModel)
        {
            if(ModelState.IsValid)
            {
                contaModel.Usuario_Id = int.Parse(HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado"));
                contaModel.Inserir();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult CriarConta()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            new ContaModel().Excluir(id);
            return RedirectToAction("Index");
        }
    }
}