using Core.Interfaces;
using Core.Models;
using Core.ViewModels;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfrastructureMongoDB.Repositories
{
    public class CategoriaMongoDB : IRepositoryCategoria
    {        
        private readonly IMongoCollection<Categoria> categoria;
        private readonly IMongoCollection<Counters> counters;

        public CategoriaMongoDB(string strconexao)
        {            
            var client = new MongoClient(strconexao);
            var database = client.GetDatabase("AtividadesDB");

            categoria = database.GetCollection<Categoria>("Categoria");
            counters = database.GetCollection<Counters>("Counters");
        }

        private int GetNextSequence(string name)
        {
            var update = Builders<Counters>.Update.Inc("seq", 1);
            var options = new FindOneAndUpdateOptions<Counters, Counters>()
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After
            };            

            return counters.FindOneAndUpdate<Counters>(x => x.Id == name, update, options).seq;            
        }

        public IEnumerable<Categoria> Select()
        {            
            return categoria.Find(x => true).ToList();
        }

        public Categoria SelectById(int id)
        {            
            return categoria.Find<Categoria>(x => x.Id == id).FirstOrDefault();
        }

        public ResultViewModel Insert(Categoria categ)
        {
            try
            {
                categ.Id = GetNextSequence("categid");
                categoria.InsertOne(categ);

                return new ResultViewModel()
                {
                    Success = true,
                    Message = "Categoria adicionada com sucesso",
                    Data = categ
                };
            }
            catch (Exception ex)
            {
                return new ResultViewModel()
                {
                    Success = false,
                    Message = ex.Message,
                    Data = ex
                };
            }
        }        

        public ResultViewModel Update(Categoria categ)
        {
            try
            {                
                categoria.ReplaceOne(x => x.Id == categ.Id, categ);
                return new ResultViewModel()
                {
                    Success = true,
                    Message = "Categoria alterada com sucesso",
                    Data = categ
                };
            }
            catch (Exception ex)
            {
                return new ResultViewModel()
                {
                    Success = false,
                    Message = ex.Message,
                    Data = ex
                };
            }
        }

        public ResultViewModel Delete(Categoria categ)
        {
            try
            {
                categoria.DeleteOne(x => x.Id == categ.Id);
                return new ResultViewModel()
                {
                    Success = true,
                    Message = "Categoria eliminada com sucesso",
                    Data = categ
                };
            }
            catch (Exception ex)
            {
                return new ResultViewModel()
                {
                    Success = false,
                    Message = ex.Message,
                    Data = ex
                };
            }
        }
    }
}
