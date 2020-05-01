using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using MongoDB.Bson;
using MongoDB.Driver;
using Core.Models;
using Core.Interfaces;
using Core.ViewModels;
using Core.ViewModels.Atividade;

namespace InfrastructureMongoDB.Repositories
{
    public class AtividadeMongoDB : IRepositoryAtividade
    {
        private readonly IMongoCollection<Atividade> atividade;        
        private readonly IMongoCollection<Counters> counters;
        private readonly IMongoCollection<Categoria> categoria;

        public AtividadeMongoDB(string strconexao)
        {
            var client = new MongoClient(strconexao);
            var database = client.GetDatabase("AtividadesDB");

            atividade = database.GetCollection<Atividade>("Atividade");            
            counters = database.GetCollection<Counters>("Counters");
            categoria = database.GetCollection<Categoria>("Categoria");
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

        public IEnumerable<Atividade> Select()
        {            
            return atividade.Find(x => x.DataEncerramento == new DateTime(01/01/0001))
                .SortBy(x => x.Prioridade)
                .ToList();
        }

        public IEnumerable<Atividade> SelectEncerrados()
        {
            return atividade.Find<Atividade>(x => x.DataEncerramento != new DateTime(01/01/0001)).ToList();
        }

        public Atividade SelectById(int id)
        {
            return atividade.Find<Atividade>(x => x.Id == id).FirstOrDefault();
        }

        public EditAtividadeViewModel SelectByIdWithCateg(int id)
        {
            var ativ = atividade.Find<Atividade>(x => x.Id == id).FirstOrDefault();
            var categ = categoria.Find<Categoria>(x => true).ToList();

            var result = new EditAtividadeViewModel()
            {
                Id = ativ.Id,
                Descricao = ativ.Descricao,
                Data = ativ.Data,
                Responsavel = ativ.Responsavel,
                Setor = ativ.Setor,
                Solicitante = ativ.Solicitante,
                Narrativa = ativ.Narrativa,
                CategoriaId = ativ.CategoriaId,
                Categorias = categ
            };

            return result;
        }

        public EditAtividadeViewModel editAtividadeViewModel()
        {
            return new EditAtividadeViewModel()
            {
                Categorias = categoria.Find<Categoria>(x => true).ToList()
            };
        }

        public ResultViewModel Insert(EditAtividadeViewModel ativ)
        {
            try
            {
                atividade.InsertOne(new Atividade()
                {
                    Id = GetNextSequence("ativid"),
                    Data = ativ.Data,
                    Descricao = ativ.Descricao,
                    Responsavel = ativ.Responsavel,
                    Setor = ativ.Setor,
                    Solicitante = ativ.Solicitante,
                    Narrativa = ativ.Narrativa,
                    DataEncerramento = new DateTime(01 / 01 / 0001),
                    CategoriaId = ativ.CategoriaId,
                    Categoria = categoria.Find<Categoria>(x => x.Id == ativ.CategoriaId).FirstOrDefault(),
                    Prioridade = atividade.Find(x => x.DataEncerramento == new DateTime(01 / 01 / 0001))
                                          .SortByDescending(x => x.Prioridade).FirstOrDefault().Prioridade + 1
                });
                
                return new ResultViewModel()
                {
                    Success = true,
                    Message = "Atividade adicionada com sucesso",
                    Data = ativ
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

        public ResultViewModel Update(EditAtividadeViewModel ativ)
        {
            try
            {                
                var update = Builders<Atividade>.Update.Combine(
                                Builders<Atividade>.Update.Set("Data", ativ.Data),
                                Builders<Atividade>.Update.Set("Descricao", ativ.Descricao),
                                Builders<Atividade>.Update.Set("Responsavel", ativ.Responsavel),
                                Builders<Atividade>.Update.Set("Setor", ativ.Setor),
                                Builders<Atividade>.Update.Set("Solicitante", ativ.Solicitante),
                                Builders<Atividade>.Update.Set("Narrativa", ativ.Narrativa),
                                Builders<Atividade>.Update.Set("CategoriaId", ativ.CategoriaId),
                                Builders<Atividade>.Update.Set("Categoria", categoria.Find<Categoria>(x => x.Id == ativ.CategoriaId).FirstOrDefault())
                             );               
                
                atividade.UpdateOne(x => x.Id == ativ.Id, update);

                return new ResultViewModel()
                {
                    Success = true,
                    Message = "Atividade alterada com sucesso",
                    Data = ativ
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

        public ResultViewModel UpdateEncerra(Atividade ativ)
        {
            try
            {
                var update = Builders<Atividade>.Update.Combine(
                                Builders<Atividade>.Update.Set("DataEncerramento", ativ.DataEncerramento),
                                Builders<Atividade>.Update.Set("Prioridade", 0)
                             );

                atividade.UpdateOne(x => x.Id == ativ.Id, update);
                
                return new ResultViewModel()
                {
                    Success = true,
                    Message = "Atividade alterada com sucesso",
                    Data = ativ
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

        public ResultViewModel Reabrir(Atividade ativ)
        {
            try
            {
                var update = Builders<Atividade>.Update.Set("DataEncerramento", new DateTime(01/01/0001));
                atividade.UpdateOne(x => x.Id == ativ.Id, update);
                                
                return new ResultViewModel()
                {
                    Success = true,
                    Message = "Atividade alterada com sucesso",
                    Data = ativ
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

        public ResultViewModel Delete(Atividade ativ)
        {
            try
            {
                atividade.DeleteOne(x => x.Id == ativ.Id);
                return new ResultViewModel()
                {
                    Success = true,
                    Message = "Atividade eliminada com sucesso",
                    Data = ativ
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

        public ResultViewModel AlteraPrioridade(PrioridadeAtividade item)
        {
            try
            {                
                var update = Builders<Atividade>.Update.Set("Prioridade", item.Prioridade);                
                atividade.UpdateOne(x => x.Id == item.Id, update);

                return new ResultViewModel()
                {
                    Success = true,
                    Message = "Atividade alterada com sucesso",
                    Data = item
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
