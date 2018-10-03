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
using System.Reflection;

using MongoDB.Bson;
using MongoDB.Driver;


namespace Atividades.Banco
{
    public class AtividadeMongo
    {
        public static string Insert(string strconexao, Atividade atividade)
        {
            string mensagem = "";
            var client = new MongoClient(strconexao);            
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<Atividade>("atividades");
            
            collection.InsertOne(atividade);

            mensagem = "Atividade inserida com sucesso";

            return mensagem;


        }
        public static IEnumerable<Atividade> Select(string strconexao)
        {
            List<Atividade> ativs = new List<Atividade>();

            return ativs;           

        }

        public static Atividade SelectById(string strconexao, int id)
        {
            Atividade ativ = new Atividade();
            return ativ;

            

        }        

        public static string Update(string strconexao, Atividade atividade)
        {
            
            string mensagem = "";
            return mensagem;

            
        }

        public static string Delete(string strconexao, Atividade atividade)
        {            
            string mensagem = "";

            return mensagem;

        }        
    }

}
