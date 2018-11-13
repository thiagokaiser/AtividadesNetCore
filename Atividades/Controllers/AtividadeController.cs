using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Atividades.Models;
using Atividades.Classes;
using Microsoft.AspNetCore.Authorization;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Atividades.Controllers
{   
    [Authorize]
    public class AtividadeController : Controller
    {
        private IConfiguration _config;

        public AtividadeController(IConfiguration configuration)
        {
            _config = configuration;
        }
        public IActionResult Index()
        {            
            IEnumerable<Atividade> ativs = Banco.AtividadeCRUD.Select();          
            return View(ativs);            
        }
        public IActionResult Encerrados()
        {
            IEnumerable<Atividade> ativs = Banco.AtividadeCRUD.SelectEncerrados();
            return View(ativs);
        }
        [HttpPost]
        public IActionResult Add(Atividade atividade)
        {            
            string insert = Banco.AtividadeCRUD.Insert(atividade);
            TempData["Message"] = insert;
            return RedirectToAction("Index");            
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categoria = Banco.CategoriaCRUD.GetSelectList();

            var model = new Atividade { Responsavel = "Thiago", Setor = "TI", Data = DateTime.Now };
            return View(model);                     
        }
        [HttpGet]
        public IActionResult Excluir(string id)
        {
            Atividade ativs = Banco.AtividadeCRUD.SelectById(id);
            return View(ativs);
        }
        [HttpPost]
        public IActionResult Excluir(Atividade atividade)
        {
            string delete = Banco.AtividadeCRUD.Delete(atividade);
            TempData["Message"] = delete;            
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Detalhe(string id)
        {
            Atividade ativs = Banco.AtividadeCRUD.SelectById(id);
            return View(ativs);
        }
        [HttpGet]
        public IActionResult Editar(string id)
        {
            ViewBag.Categoria = Banco.CategoriaCRUD.GetSelectList();
            Atividade ativs = Banco.AtividadeCRUD.SelectById(id);
            return View(ativs);
        }
        [HttpPost]
        public IActionResult Editar(Atividade atividade)
        {
            string ativ = Banco.AtividadeCRUD.Update(atividade);
            TempData["Message"] = ativ;
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Encerrar(string id)
        {
            ViewBag.Categoria = Banco.CategoriaCRUD.GetSelectList();            
            Atividade ativs = Banco.AtividadeCRUD.SelectById(id);
            ativs.DataEncerramento = DateTime.Now;
            return View(ativs);
        }
        [HttpPost]
        public IActionResult Encerrar(Atividade atividade)
        {
            string ativ = Banco.AtividadeCRUD.UpdateEncerra(atividade);
            TempData["Message"] = ativ;
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Reabrir(string id)
        {
            ViewBag.Categoria = Banco.CategoriaCRUD.GetSelectList();
            Atividade ativs = Banco.AtividadeCRUD.SelectById(id);            
            return View(ativs);
        }
        [HttpPost]
        public IActionResult Reabrir(Atividade atividade)
        {
            string ativ = Banco.AtividadeCRUD.Reabrir(atividade);
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
                mensagem = Banco.AtividadeCRUD.AlteraPrioridade(lista);
                return Json(mensagem);
            }
            else
            {
                return Json(mensagem);
            }

        }
    }
}
