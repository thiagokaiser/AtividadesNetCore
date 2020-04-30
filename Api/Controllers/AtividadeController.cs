using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using Core.Services;
using Core.ViewModels.Atividade;

namespace Api.Controllers
{   
    [Authorize]
    public class AtividadeController : Controller
    {
        private readonly AtividadeService atividadeService;
        private readonly CategoriaService categoriaService;

        public AtividadeController(AtividadeService atividadeService, CategoriaService categoriaService)
        {
            this.atividadeService = atividadeService;
            this.categoriaService = categoriaService;
        }        

        public IActionResult Index()
        {
            var ativs = atividadeService.Select();          
            return View(ativs);            
        }

        public IActionResult Encerrados()
        {
            var ativs = atividadeService.SelectEncerrados();
            return View(ativs);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categoria = SelectListCategoria();
            var model = new Atividade { Responsavel = "Thiago", Setor = "TI", Data = DateTime.Now };
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Atividade atividade)
        {            
            var result = atividadeService.Insert(atividade);
            TempData["Message"] = result.Message;
            return RedirectToAction("Index");            
        }
        
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            Atividade ativ = atividadeService.SelectById(id);
            return View(ativ);
        }

        [HttpPost]
        public IActionResult Excluir(Atividade atividade)
        {
            var result = atividadeService.Delete(atividade);
            TempData["Message"] = result.Message;            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Detalhe(int id)
        {
            Atividade ativ = atividadeService.SelectById(id);
            return View(ativ);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {            
            ViewBag.Categoria = SelectListCategoria();
            var ativ = atividadeService.SelectById(id);
            return View(ativ);
        }

        [HttpPost]
        public IActionResult Editar(Atividade atividade)
        {
            var result = atividadeService.Update(atividade);
            TempData["Message"] = result.Message;
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Encerrar(int id)
        {            
            var ativ = atividadeService.SelectById(id);
            ativ.DataEncerramento = DateTime.Now;
            return View(ativ);
        }

        [HttpPost]
        public IActionResult Encerrar(Atividade atividade)
        {
            var result = atividadeService.UpdateEncerra(atividade);
            TempData["Message"] = result.Message;
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Reabrir(int id)
        {            
            var ativ = atividadeService.SelectById(id);            
            return View(ativ);
        }

        [HttpPost]
        public IActionResult Reabrir(Atividade atividade)
        {
            var result = atividadeService.Reabrir(atividade);
            TempData["Message"] = result.Message;
            return RedirectToAction("Encerrados");
        }

        public IActionResult EnviaEmail()
        {            
            
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress("thiago.kaiser@a.com.br", "thiago"));
            msg.From = new MailAddress("a@a.com.br", "a");
            msg.Subject = "This is a Test Mail";
            msg.Body = "This is a test message using Exchange";
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("a@a.com.br", "senha");
            client.Port = 587; // You can use Port 25 if 587 is blocked (mine is!)
            client.Host = "smtp.office365.com";
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            
            client.Send(msg);                
            
            TempData["Message"] = "Email enviado";
            return RedirectToAction("Index");
        }        

        [HttpPost]
        public IActionResult AtualizaPrioridade([FromBody] List<PrioridadeAtividade> lista)
        {
            if (lista == null) return Json("");                       
            
            var result = atividadeService.AlteraPrioridade(lista);
            return Json(result.Message);
        }

        private List<SelectListItem> SelectListCategoria()
        {
            List<SelectListItem> categs = new List<SelectListItem>();
            var categorias = categoriaService.Select().ToList();
            foreach (var categ in categorias)
            {
                categs.Add(new SelectListItem { Value = categ.Id.ToString(), Text = categ.Descricao });
            }
            return categs;
        }
    }
}