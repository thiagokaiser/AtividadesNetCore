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
using Core.Services;

namespace Api.Controllers
{   
    [Authorize]
    public class CategoriaController : Controller
    {
        private readonly CategoriaService categoriaService;

        public CategoriaController(CategoriaService categoriaService)
        {
            this.categoriaService = categoriaService;
        }

        public IActionResult Index()
        {            
            IEnumerable<Categoria> ativs = categoriaService.Select();          
            return View(ativs);            
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new Categoria { };
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Categoria categ)
        {            
            var result = categoriaService.Insert(categ);
            TempData["Message"] = result.Message;
            return RedirectToAction("Index");            
        }        

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            Categoria categ = categoriaService.SelectById(id);
            return View(categ);
        }

        [HttpPost]
        public IActionResult Excluir(Categoria categ)
        {
            var result = categoriaService.Delete(categ);
            TempData["Message"] = result.Message;            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Detalhe(int id)
        {
            Categoria categ = categoriaService.SelectById(id);
            return View(categ);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            Categoria categ = categoriaService.SelectById(id);
            return View(categ);
        }

        [HttpPost]
        public IActionResult Editar(Categoria categ)
        {
            var result = categoriaService.Update(categ);
            TempData["Message"] = result.Message;
            return RedirectToAction("Index");

        }        
    }
}
