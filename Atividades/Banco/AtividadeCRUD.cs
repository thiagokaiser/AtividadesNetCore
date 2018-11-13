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

namespace Atividades.Banco
{
    public class AtividadeCRUD
    {
        public interface teste
        {

        }
        public static IEnumerable<Atividade> Select()
        {
            string[] strconexao = StrConexao.GetString();        

            IEnumerable<Atividade> ativs;

            if (strconexao[0] == "Mongo")
            {
                ativs = AtividadeMongo.Select(strconexao[1]);
            }
            else
            {
                ativs = AtividadeSQL.Select(strconexao[1]);
            }            

            return ativs;
        }

        public static Atividade SelectById(string id)
        {
            string[] strconexao = StrConexao.GetString();

            Atividade ativ;

            if (strconexao[0] == "Mongo")
            {
                ativ = AtividadeMongo.SelectById(strconexao[1], id);
            }
            else
            {
                ativ = AtividadeSQL.SelectById(strconexao[1],id);
            }

            return ativ;
        }

        public static string Insert(Atividade atividade)
        {
            string[] strconexao = StrConexao.GetString();
            string mensagem = "";

            if (strconexao[0] == "Mongo")
            {
                mensagem = AtividadeMongo.Insert(strconexao[1], atividade);
                
            }
            else
            {
                mensagem = AtividadeSQL.Insert(strconexao[1], atividade);
            }

            return mensagem;
        }     
        
        public static string Update(Atividade atividade)
        {
            string[] strconexao = StrConexao.GetString();
            string mensagem = "";

            if (strconexao[0] == "Mongo")
            {
                mensagem = AtividadeMongo.Update(strconexao[1], atividade);
            }
            else
            {
                mensagem = AtividadeSQL.Update(strconexao[1], atividade);
            }

            return mensagem;
        }

        public static string UpdateEncerra(Atividade atividade)
        {
            string[] strconexao = StrConexao.GetString();
            string mensagem = "";

            if (strconexao[0] == "Mongo")
            {
                mensagem = AtividadeMongo.Update(strconexao[1], atividade);
            }
            else
            {
                mensagem = AtividadeSQL.UpdateEncerra(strconexao[1], atividade);
            }

            return mensagem;
        }
        public static string Delete(Atividade atividade)
        {
            string[] strconexao = StrConexao.GetString();
            string mensagem = "";

            if (strconexao[0] == "Mongo")
            {
                mensagem = AtividadeMongo.Delete(strconexao[1], atividade);
            }
            else
            {
                mensagem = AtividadeSQL.Delete(strconexao[1], atividade);
            }

            return mensagem;
        }
        public static string AlteraPrioridade(List<JsonPrioridade> lista)
        {
            string[] strconexao = StrConexao.GetString();
            string mensagem = "";
            int priorid = 0;
            foreach (var item in lista)
            {
                priorid += 1;
                item.Prioridade = priorid;
                mensagem = AtividadeSQL.AlteraPrioridade(strconexao[1], item);
            }            
            return mensagem;
        }
    }
}
