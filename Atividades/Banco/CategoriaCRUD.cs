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

            IEnumerable<Categoria> categ;

            if (strconexao[0] == "Mongo")
            {
                //ativs = AtividadeMongo.Select(strconexao[1]);
                categ = new List<Categoria>();
            }
            else
            {
                categ = CategoriaSQL.Select(strconexao[1]);
            }            

            return categ;
        }

        public static Categoria SelectById(string id)
        {
            string[] strconexao = StrConexao.GetString();

            Categoria categ;

            if (strconexao[0] == "Mongo")
            {
                //ativ = AtividadeMongo.SelectById(strconexao[1], id);
                categ = new Categoria();
            }
            else
            {
                categ = CategoriaSQL.SelectById(strconexao[1],id);
            }

            return categ;
        }

        public static string Insert(Categoria categ)
        {
            string[] strconexao = StrConexao.GetString();
            string mensagem = "";

            if (strconexao[0] == "Mongo")
            {
                //mensagem = AtividadeMongo.Insert(strconexao[1], atividade);
                
            }
            else
            {
                mensagem = CategoriaSQL.Insert(strconexao[1], categ);
            }

            return mensagem;
        }     
        
        public static string Update(Categoria categ)
        {
            string[] strconexao = StrConexao.GetString();
            string mensagem = "";

            if (strconexao[0] == "Mongo")
            {
                //mensagem = AtividadeMongo.Update(strconexao[1], atividade);
            }
            else
            {
                mensagem = CategoriaSQL.Update(strconexao[1], categ);
            }

            return mensagem;
        }

        public static string Delete(Categoria categ)
        {
            string[] strconexao = StrConexao.GetString();
            string mensagem = "";

            if (strconexao[0] == "Mongo")
            {
                //mensagem = AtividadeMongo.Delete(strconexao[1], atividade);
            }
            else
            {
                mensagem = CategoriaSQL.Delete(strconexao[1], categ);
            }

            return mensagem;
        }
        public static List<SelectListItem> GetSelectList()
        {
            string[] strconexao = StrConexao.GetString();

            List<SelectListItem> categ = new List<SelectListItem>();

            if (strconexao[0] == "Mongo")
            {
                //ativs = AtividadeMongo.Select(strconexao[1]);
                categ = new List<SelectListItem>();
            }
            else
            {
                categ = CategoriaSQL.GetSelectList();
            }

            return categ;
        }        
    }
}
