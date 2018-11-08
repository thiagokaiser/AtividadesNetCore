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
using System.Net.Mail;
using System.Net;

namespace Atividades.Controllers
{   
    [Authorize]
    public class CategoriaController : Controller
    {        
        public IActionResult Index()
        {            
            IEnumerable<Categoria> ativs = Banco.CategoriaCRUD.Select();          
            return View(ativs);            
        }
        [HttpPost]
        public IActionResult Add(Categoria categ)
        {            
            string insert = Banco.CategoriaCRUD.Insert(categ);
            TempData["Message"] = insert;
            return RedirectToAction("Index");            
        }
        [HttpGet]
        public IActionResult Add()
        {
            var model = new Categoria { };
            return View(model);                     
        }
        [HttpGet]
        public IActionResult Excluir(string id)
        {
            Categoria categ = Banco.CategoriaCRUD.SelectById(id);
            return View(categ);
        }
        [HttpPost]
        public IActionResult Excluir(Categoria categ)
        {
            string delete = Banco.CategoriaCRUD.Delete(categ);
            TempData["Message"] = delete;            
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Detalhe(string id)
        {
            Categoria categ = Banco.CategoriaCRUD.SelectById(id);
            return View(categ);
        }
        [HttpGet]
        public IActionResult Editar(string id)
        {
            Categoria categ = Banco.CategoriaCRUD.SelectById(id);
            return View(categ);
        }
        [HttpPost]
        public IActionResult Editar(Categoria categ)
        {
            string categoria = Banco.CategoriaCRUD.Update(categ);
            TempData["Message"] = categoria;
            return RedirectToAction("Index");

        }        
    }
}
