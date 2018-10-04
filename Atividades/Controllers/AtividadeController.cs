using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Atividades.Models;
using Microsoft.AspNetCore.Authorization;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

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
            return View();                     
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
    }
}
