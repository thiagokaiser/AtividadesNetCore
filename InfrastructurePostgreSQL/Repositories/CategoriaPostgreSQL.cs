using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Dapper;
using Npgsql;
using System.Reflection;
using Core.Models;
using Core.Interfaces;
using Core.ViewModels;

namespace InfrastructurePostgreSQL.Repositories
{
    public class CategoriaPostgreSQL : IRepositoryCategoria
    {
        private readonly string strconexao;

        public CategoriaPostgreSQL(string strconexao)
        {
            this.strconexao = strconexao;
        }

        public IEnumerable<Categoria> Select()
        {
            using (NpgsqlConnection conexao = new NpgsqlConnection(strconexao))
            {
                IEnumerable<Categoria> categ = conexao.Query<Categoria>("Select * from Categoria");
                return categ;
            }            
        }

        public Categoria SelectById(int id)
        {
            using (NpgsqlConnection conexao = new NpgsqlConnection(strconexao))
            {
                Categoria categ = conexao.QueryFirst<Categoria>("Select * from Categoria WHERE Id = @Id", new { Id = id });
                return categ;
            }
        }

        public ResultViewModel Insert(Categoria categ)
        {
            var validate = ValidaUpdate(categ);
            if (!validate.Success) return validate;

            using (NpgsqlConnection conexao = new NpgsqlConnection(strconexao))
            {
                try
                {
                    var query = @"INSERT INTO Categoria(Descricao, Cor) 
                                                VALUES(@Descricao,@Cor); ";
                    conexao.Execute(query, categ);

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
        }             

        public ResultViewModel Update(Categoria categ)
        {
            var validate = ValidaUpdate(categ);
            if (!validate.Success) return validate;
            using (NpgsqlConnection conexao = new NpgsqlConnection(strconexao))
            {
                try
                {
                    var query = @"Update Categoria Set 
                                    Descricao = @Descricao,
                                    Cor       = @Cor                                        
                                    Where Id = @Id";
                    conexao.Execute(query, categ);

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
        }

        public ResultViewModel Delete(Categoria categ)
        {
            var validate = ValidaDelete(categ);
            if (!validate.Success) return validate;

            using (NpgsqlConnection conexao = new NpgsqlConnection(strconexao))
            {
                try
                {
                    var query = "DELETE FROM Categoria WHERE Id =" + categ.Id;
                    conexao.Execute(query);

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

        private ResultViewModel ValidaUpdate(Categoria categ)
        {
            var listErros = new List<string>();            
            if (categ.Descricao?.TrimEnd() == "asd")
            {
                listErros.Add("Descrição deve ser diferente de 'asd'");
            }

            return new ResultViewModel()
            {
                Success = listErros.Count() == 0,
                Message = listErros.Count() > 0 ? "Ocorreram erros" : "",
                Data = listErros
            };
        }

        private ResultViewModel ValidaDelete(Categoria categ)
        {
            return new ResultViewModel()
            {
                Success = true,
                Message = "",
                Data = null
            };
        }
    }
}
