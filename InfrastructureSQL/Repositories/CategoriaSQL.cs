using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Dapper;
using System.Data.SqlClient;
using Core.Models;
using Core.ViewModels;
using Core.Interfaces;

namespace InfrastructureSQL.Repositories
{
    public class CategoriaSQL : IRepositoryCategoria
    {
        private readonly string strconexao;

        public CategoriaSQL(string strconexao)
        {
            this.strconexao = strconexao;
        }

        public IEnumerable<Categoria> Select()
        {                        
            using (SqlConnection conexao = new SqlConnection(strconexao))
            {
                var categ = conexao.Query<Categoria>("Select * from Categoria");
                return categ;
            }            
        }

        public Categoria SelectById(int id)
        {
            using (SqlConnection conexao = new SqlConnection(strconexao))
            {
                var categ = conexao.QueryFirst<Categoria>("Select * from Categoria WHERE Id = @Id", new { Id = id });
                return categ;
            }
        }

        public ResultViewModel Insert(Categoria categ)
        {
            var validate = ValidaUpdate(categ);
            if (!validate.Success) return validate;

            using (SqlConnection conexao = new SqlConnection(strconexao))
            {
                try
                {
                    var query = @"INSERT INTO Categoria(Descricao, Cor) 
                                                VALUES(@Descricao,@Cor); 
                                    SELECT CAST(SCOPE_IDENTITY() as INT);";
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

            using (SqlConnection conexao = new SqlConnection(strconexao))
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

            using (SqlConnection conexao = new SqlConnection(strconexao))
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
