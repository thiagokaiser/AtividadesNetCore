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
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Atividades.Banco
{
    public class CategoriaCRUD
    {
        
        public static IEnumerable<Categoria> Select()
        {
            string[] strconexao = StrConexao.GetString();        

            IEnumerable<Categoria> categ = new List<Categoria> { };

            switch (strconexao[0])
            {
                case "Mongo":
                    //ativs = AtividadeMongo.Select(strconexao[1]);
                    categ = new List<Categoria>();
                    break;
                case "SQL":
                    categ = CategoriaSQL.Select(strconexao[1]);
                    break;
                case "Postgres":
                    categ = CategoriaPostgres.Select(strconexao[1]);
                    break;
            }           

            return categ;
        }

        public static Categoria SelectById(int id)
        {
            string[] strconexao = StrConexao.GetString();

            Categoria categ = new Categoria { };

            switch (strconexao[0])
            {
                case "Mongo":
                    //ativs = AtividadeMongo.Select(strconexao[1]);                    
                    break;
                case "SQL":
                    categ = CategoriaSQL.SelectById(strconexao[1], id);
                    break;
                case "Postgres":
                    categ = CategoriaPostgres.SelectById(strconexao[1], id);
                    break;
            }

            return categ;
        }

        public static string Insert(Categoria categ)
        {
            string[] strconexao = StrConexao.GetString();
            string mensagem = "";

            switch (strconexao[0])
            {
                case "Mongo":
                    //ativs = AtividadeMongo.Select(strconexao[1]);                    
                    break;
                case "SQL":
                    mensagem = CategoriaSQL.Insert(strconexao[1], categ);
                    break;
                case "Postgres":
                    mensagem = CategoriaPostgres.Insert(strconexao[1], categ);
                    break;
            }           

            return mensagem;
        }     
        
        public static string Update(Categoria categ)
        {
            string[] strconexao = StrConexao.GetString();
            string mensagem = "";

            switch (strconexao[0])
            {
                case "Mongo":
                    //ativs = AtividadeMongo.Select(strconexao[1]);                    
                    break;
                case "SQL":
                    mensagem = CategoriaSQL.Update(strconexao[1], categ);
                    break;
                case "Postgres":
                    mensagem = CategoriaPostgres.Update(strconexao[1], categ);
                    break;
            }            

            return mensagem;
        }

        public static string Delete(Categoria categ)
        {
            string[] strconexao = StrConexao.GetString();
            string mensagem = "";

            switch (strconexao[0])
            {
                case "Mongo":
                    //ativs = AtividadeMongo.Select(strconexao[1]);                    
                    break;
                case "SQL":
                    mensagem = CategoriaSQL.Delete(strconexao[1], categ);
                    break;
                case "Postgres":
                    mensagem = CategoriaPostgres.Delete(strconexao[1], categ);
                    break;
            }           

            return mensagem;
        }
        public static List<SelectListItem> GetSelectList()
        {
            string[] strconexao = StrConexao.GetString();

            List<SelectListItem> categ = new List<SelectListItem>();

            switch (strconexao[0])
            {
                case "Mongo":
                    //ativs = AtividadeMongo.Select(strconexao[1]);                    
                    break;
                case "SQL":
                    categ = CategoriaSQL.GetSelectList();
                    break;
                case "Postgres":
                    categ = CategoriaPostgres.GetSelectList();
                    break;
            }           

            return categ;
        }        
    }
}
