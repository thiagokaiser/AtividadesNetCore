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
        [HttpPost]
        public IActionResult Add(Atividade atividade)
        {            
            var insert = atividadeService.Insert(atividade);
            TempData["Message"] = insert;
            return RedirectToAction("Index");            
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categoria = SelectListCategoria();
            var model = new Atividade { Responsavel = "Thiago", Setor = "TI", Data = DateTime.Now };
            return View(model);                     
        }
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            Atividade ativs = atividadeService.SelectById(id);
            return View(ativs);
        }
        [HttpPost]
        public IActionResult Excluir(Atividade atividade)
        {
            string delete = atividadeService.Delete(atividade);
            TempData["Message"] = delete;            
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Detalhe(int id)
        {
            Atividade ativs = atividadeService.SelectById(id);
            return View(ativs);
        }
        [HttpGet]
        public IActionResult Editar(int id)
        {            
            ViewBag.Categoria = SelectListCategoria();
            var ativs = atividadeService.SelectById(id);
            return View(ativs);
        }
        [HttpPost]
        public IActionResult Editar(Atividade atividade)
        {
            string ativ = atividadeService.Update(atividade);
            TempData["Message"] = ativ;
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Encerrar(int id)
        {
            ViewBag.Categoria = SelectListCategoria();
            var ativs = atividadeService.SelectById(id);
            ativs.DataEncerramento = DateTime.Now;
            return View(ativs);
        }
        [HttpPost]
        public IActionResult Encerrar(Atividade atividade)
        {
            var ativ = atividadeService.UpdateEncerra(atividade);
            TempData["Message"] = ativ;
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Reabrir(int id)
        {
            ViewBag.Categoria = SelectListCategoria();
            var ativs = atividadeService.SelectById(id);            
            return View(ativs);
        }
        [HttpPost]
        public IActionResult Reabrir(Atividade atividade)
        {
            var ativ = atividadeService.Reabrir(atividade);
            TempData["Message"] = ativ;
            return RedirectToAction("Encerrados");
        }

        public IActionResult EnviaEmail()
        {            
            
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress("thiago.kaiser@danicazipco.com.br", "thiago"));
            msg.From = new MailAddress("site@danicazipco.com.br", "site");
            msg.Subject = "This is a Test Mail";
            msg.Body = "This is a test message using Exchange";
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("site@danicazipco.com.br", "Ert@4321");
            client.Port = 587; // You can use Port 25 if 587 is blocked (mine is!)
            client.Host = "smtp.office365.com";
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            
            client.Send(msg);                
            
            TempData["Message"] = "Email enviado";
            return RedirectToAction("Index");
        }        

        [HttpPost]
        public IActionResult Atualizatable([FromBody] List<JsonPrioridade> lista)
        {
            string mensagem = "Error";
            if (lista != null)
            {
                mensagem = atividadeService.AlteraPrioridade(lista);
                return Json(mensagem);
            }
            else
            {
                return Json(mensagem);
            }

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
