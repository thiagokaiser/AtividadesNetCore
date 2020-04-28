using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using MongoDB.Bson;
using MongoDB.Driver;
using Core.Models;

namespace InfrastructureMongoDB.Repositories
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
        public static Atividade SelectById(string strconexao, int id)
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
