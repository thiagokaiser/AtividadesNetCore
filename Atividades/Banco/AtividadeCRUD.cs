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

            IEnumerable<Atividade> ativs = new List<Atividade> { };            

            switch (strconexao[0])
            {
                case "Mongo":
                    ativs = AtividadeMongo.Select(strconexao[1]);
                    break;
                case "SQL":
                    ativs = AtividadeSQL.Select(strconexao[1]);
                    break;
                case "Postgres":
                    ativs = AtividadePostgres.Select(strconexao[1]);
                    break;
            }           

            return ativs;
        }

        public static IEnumerable<Atividade> SelectEncerrados()
        {
            string[] strconexao = StrConexao.GetString();

            IEnumerable<Atividade> ativs = new List<Atividade> { };

            switch (strconexao[0])
            {
                case "Mongo":
                    ativs = AtividadeMongo.Select(strconexao[1]);
                    break;
                case "SQL":
                    ativs = AtividadeSQL.SelectEncerrados(strconexao[1]);
                    break;
                case "Postgres":
                    ativs = AtividadePostgres.SelectEncerrados(strconexao[1]);
                    break;
            }

            return ativs;
        }

        public static Atividade SelectById(int id)
        {
            string[] strconexao = StrConexao.GetString();

            Atividade ativ = new Atividade { };

            switch (strconexao[0])
            {
                case "Mongo":
                    ativ = AtividadeMongo.SelectById(strconexao[1], id);
                    break;
                case "SQL":
                    ativ = AtividadeSQL.SelectById(strconexao[1], id);
                    break;
                case "Postgres":
                    ativ = AtividadePostgres.SelectById(strconexao[1], id);
                    break;
            }

            return ativ;
        }

        public static string Insert(Atividade atividade)
        {
            string[] strconexao = StrConexao.GetString();
            string mensagem = "";

            switch (strconexao[0])
            {
                case "Mongo":
                    mensagem = AtividadeMongo.Insert(strconexao[1], atividade);
                    break;
                case "SQL":
                    mensagem = AtividadeSQL.Insert(strconexao[1], atividade);
                    break;
                case "Postgres":
                    mensagem = AtividadePostgres.Insert(strconexao[1], atividade);
                    break;
            }           

            return mensagem;
        }     
        
        public static string Update(Atividade atividade)
        {
            string[] strconexao = StrConexao.GetString();
            string mensagem = "";

            switch (strconexao[0])
            {
                case "Mongo":
                    mensagem = AtividadeMongo.Update(strconexao[1], atividade);
                    break;
                case "SQL":
                    mensagem = AtividadeSQL.Update(strconexao[1], atividade);
                    break;
                case "Postgres":
                    mensagem = AtividadePostgres.Update(strconexao[1], atividade);
                    break;
            }

            return mensagem;
        }

        public static string UpdateEncerra(Atividade atividade)
        {
            string[] strconexao = StrConexao.GetString();
            string mensagem = "";

            switch (strconexao[0])
            {
                case "Mongo":
                    mensagem = AtividadeMongo.Update(strconexao[1], atividade);
                    break;
                case "SQL":
                    mensagem = AtividadeSQL.UpdateEncerra(strconexao[1], atividade);
                    break;
                case "Postgres":
                    mensagem = AtividadePostgres.UpdateEncerra(strconexao[1], atividade);
                    break;
            }           

            return mensagem;
        }
        public static string Reabrir(Atividade atividade)
        {
            string[] strconexao = StrConexao.GetString();
            string mensagem = "";

            switch (strconexao[0])
            {
                case "Mongo":
                    mensagem = AtividadeMongo.Update(strconexao[1], atividade);
                    break;
                case "SQL":
                    mensagem = AtividadeSQL.Reabrir(strconexao[1], atividade);
                    break;
                case "Postgres":
                    mensagem = AtividadePostgres.Reabrir(strconexao[1], atividade);
                    break;
            }           

            return mensagem;
        }
        public static string Delete(Atividade atividade)
        {
            string[] strconexao = StrConexao.GetString();
            string mensagem = "";

            switch (strconexao[0])
            {
                case "Mongo":
                    mensagem = AtividadeMongo.Delete(strconexao[1], atividade);
                    break;
                case "SQL":
                    mensagem = AtividadeSQL.Delete(strconexao[1], atividade);
                    break;
                case "Postgres":
                    mensagem = AtividadePostgres.Delete(strconexao[1], atividade);
                    break;
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
