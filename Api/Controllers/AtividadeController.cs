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

        public AtividadeController(AtividadeService atividadeService)
        {
            this.atividadeService = atividadeService;            
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
            var model = atividadeService.editAtividadeViewModel();
            
            ViewBag.Categoria = SelectListCategoria(model.Categorias.ToList());

            model.Responsavel = "Thiago";
            model.Setor = "TI";
            model.Data = DateTime.Now;
            
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(EditAtividadeViewModel atividade)
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
            var ativ = atividadeService.SelectByIdWithCateg(id);
            ViewBag.Categoria = SelectListCategoria(ativ.Categorias);
            
            return View(ativ);
        }

        [HttpPost]
        public IActionResult Editar(EditAtividadeViewModel atividade)
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

        [HttpPost]
        public IActionResult AtualizaPrioridade([FromBody] List<PrioridadeAtividade> lista)
        {
            if (lista == null) return Json("");                       
            
            var result = atividadeService.AlteraPrioridade(lista);
            return Json(result.Message);
        }

        private List<SelectListItem> SelectListCategoria(List<Categoria> categorias)
        {
            List<SelectListItem> categs = new List<SelectListItem>();            
            foreach (var categ in categorias)
            {
                categs.Add(new SelectListItem { Value = categ.Id.ToString(), Text = categ.Descricao });
            }
            return categs;
        }
    }
}