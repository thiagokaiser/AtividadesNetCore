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
        public static IEnumerable<Atividade> Select(string strconexao)
        {
            

            var client = new MongoClient(strconexao);
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<Atividade>("atividades");
            var ativs = collection.Find(new BsonDocument()).ToList();
            
            return ativs;           

        }

        public static Atividade SelectById(string strconexao, string id)
        {
            var client = new MongoClient(strconexao);
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<Atividade>("atividades");
            var filter = Builders<Atividade>.Filter.Eq("id", id);
            var ativ = collection.Find(filter).FirstOrDefault();
            return ativ;



        }
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
