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
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                IEnumerable<Atividade> ativs = conexao.Query<Atividade>("Select * from Atividade");
                                
                return View(ativs);
            }            
            
        }
        [HttpPost]
        public IActionResult Add(Atividade atividade)
        {

            string insert = SQL.AtividadeSQL.Insert(atividade);

            
            TempData["Message"] = insert;
            return RedirectToAction("Index");
            
            //return RedirectToAction("Index");            
            
            
            
            
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();                     
        }
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {                
                Atividade ativ = conexao.QueryFirst<Atividade>("Select * from Atividade WHERE Id = @Id", new { Id = id });
                return View(ativ);                
            }
        }
        [HttpPost]
        public IActionResult Excluir(Atividade atividade)
        {
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                try
                {                    
                    var query = "DELETE FROM Atividade WHERE Id =" + atividade.Id;
                    conexao.Execute(query);                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return RedirectToAction("Index");

            }
        }
        [HttpGet]
        public IActionResult Detalhe(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                Atividade ativ = conexao.QueryFirst<Atividade>("Select * from Atividade WHERE Id = @Id", new { Id = id });
                return View(ativ);
            }
        }
        [HttpGet]
        public IActionResult Editar(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                Atividade ativ = conexao.QueryFirst<Atividade>("Select * from Atividade WHERE Id = @Id", new { Id = id });
                return View(ativ);
            }
        }
        [HttpPost]
        public IActionResult Editar(Atividade atividade)
        {
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    var query = @"Update Atividade Set 
                                  Descricao = @Descricao,
                                  Setor     = @Setor
                                  Where Id = @Id";
                    conexao.Execute(query, atividade);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return RedirectToAction("Index");

            }
        }
    }
}
