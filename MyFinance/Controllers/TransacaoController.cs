using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class TransacaoController : Controller
    {
        IHttpContextAccessor HttpContextAccessor { get; set; }

        public TransacaoController(IHttpContextAccessor httpContext)
        {
            HttpContextAccessor = httpContext;
        }

        public IActionResult Index()
        {
            TransacaoModel transacao = new TransacaoModel(HttpContextAccessor);
            ViewBag.ListaTransacao = transacao.ListaTransacao();
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(TransacaoModel transacao, int? id)
        {
            if (ModelState.IsValid)
            {
                transacao.Usuario_Id = int.Parse(HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado"));
                transacao.Inserir(id);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Registrar(int? id)
        {
            if (id != null)
            {
                TransacaoModel transacao = new TransacaoModel(HttpContextAccessor);
                ViewBag.Registro = transacao.CarregarRegistro(id);
            }

            ViewBag.ListaContas = new ContaModel(HttpContextAccessor).ListaConta();
            ViewBag.ListaPlanoContas = new PlanoContaModel(HttpContextAccessor).ListaPlanoContas();

            return View();
        }

        [HttpGet]
        public IActionResult ExcluirTransacao(int id)
        {
            ViewBag.Registro = new TransacaoModel(HttpContextAccessor).CarregarRegistro(id);
            return View();
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            new TransacaoModel(HttpContextAccessor).Excluir(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [HttpPost]
        public IActionResult Extrato(TransacaoModel formulario)
        {
            formulario.HttpContextAccessor = HttpContextAccessor;
            ViewBag.ListaTransacao = formulario.ListaTransacao();
            ViewBag.ListaContas = new ContaModel(HttpContextAccessor).ListaConta();

            return View();
        }

        public IActionResult Dashboard()
        {
            List<DashBoard> listaDashBoard= new DashBoard(HttpContextAccessor).retornarDados();

            string valores = "";
            string labels = "";
            string cores = "";

            var random = new Random();

            foreach (var item in listaDashBoard)
            {
                valores += $"{item.Total},"; 
                labels += $"'{item.Descricao}',";
                cores += $"'{String.Format("#{0:X6}", random.Next(0x1000000))}',";
            }

            ViewBag.valores = valores;
            ViewBag.labels = labels;
            ViewBag.cores = cores;

            return View();
        }
    }
}