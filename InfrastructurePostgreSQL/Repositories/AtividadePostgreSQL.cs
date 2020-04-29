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
    public class AtividadePostgreSQL : IRepositoryAtividade
    {
        private readonly string strconexao;

        public AtividadePostgreSQL(string strconexao)
        {
            this.strconexao = strconexao;
        }

        public IEnumerable<Atividade> Select()
        {                        
            using (NpgsqlConnection conexao = new NpgsqlConnection(strconexao))
            {                                
                var ativs = conexao.Query<Atividade, Categoria, Atividade>(@"
                    Select * from Atividade T1 LEFT JOIN Categoria T2 ON T1.CategoriaId = T2.Id 
                    WHERE DataEncerramento IS NULL ORDER BY T1.Prioridade",
                    (Atividade, Categoria) =>
                    {
                        Atividade.Categoria = Categoria;
                        return Atividade;
                    }).Distinct().ToList();

                return ativs;
            }            
        }

        public IEnumerable<Atividade> SelectEncerrados()
        {
            using (NpgsqlConnection conexao = new NpgsqlConnection(strconexao))
            {
                var ativs = conexao.Query<Atividade, Categoria, Atividade>(@"
                        Select * from Atividade T1 LEFT JOIN Categoria T2 ON T1.CategoriaId = T2.Id 
                        WHERE DataEncerramento IS NOT NULL ORDER BY T1.Prioridade",
                        (Atividade, Categoria) => {
                            Atividade.Categoria = Categoria;
                            return Atividade;
                        }).Distinct().ToList();
                return ativs;
            }
        }

        public Atividade SelectById(int id)
        {
            using (NpgsqlConnection conexao = new NpgsqlConnection(strconexao))
            {
                var ativ = conexao.Query<Atividade, Categoria, Atividade>(@"
                    Select * from Atividade T1 LEFT JOIN Categoria T2 ON T1.CategoriaId = T2.Id WHERE T1.Id = @Id",
                    (Atividade, Categoria) => {
                        Atividade.Categoria = Categoria;                        
                        return Atividade;}, new { Id = id }).FirstOrDefault();
                
                return ativ;
            }
        }

        public ResultViewModel Insert(Atividade atividade)
        {
            var validate = ValidaUpdate(atividade);
            if (!validate.Success) return validate;
            
            using (NpgsqlConnection conexao = new NpgsqlConnection(strconexao))
            {
                try
                {                            
                    var query = @"INSERT INTO Atividade(Descricao, Responsavel,  Setor,  CategoriaId, Data,  Prioridade, Solicitante, Narrativa) 
                                                VALUES(@Descricao,@Responsavel, @Setor, @CategoriaId, @Data, (Select coalesce(MAX(Prioridade), 0) from Atividade) + 1,
                                                        @Solicitante, @Narrativa);";
                    conexao.Execute(query, atividade);

                    return new ResultViewModel()
                    {
                        Success = true,
                        Message = "Atividade adicionada com sucesso",
                        Data = atividade
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
        
        public ResultViewModel Update(Atividade atividade)
        {
            var validate = ValidaUpdate(atividade);
            if (!validate.Success) return validate;

            using (NpgsqlConnection conexao = new NpgsqlConnection(strconexao))
            {
                try
                {
                    var query = @"Update Atividade Set 
                                    Descricao   = @Descricao,
                                    Responsavel = @Responsavel,
                                    Setor       = @Setor,
                                    CategoriaId = @CategoriaId,
                                    Data        = @Data,                                        
                                    Solicitante = @Solicitante,
                                    Narrativa   = @Narrativa
                                    Where Id = @Id";

                    conexao.Execute(query, atividade);

                    return new ResultViewModel()
                    {
                        Success = true,
                        Message = "Atividade alterada com sucesso",
                        Data = atividade
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

        public ResultViewModel UpdateEncerra(Atividade atividade)
        {            
            using (NpgsqlConnection conexao = new NpgsqlConnection(strconexao))
            {
                try
                {
                    var query = @"Update Atividade Set                                         
                                    DataEncerramento = @DataEncerramento
                                    Where Id = @Id";
                    conexao.Execute(query, atividade);

                    return new ResultViewModel()
                    {
                        Success = true,
                        Message = "Atividade alterada com sucesso",
                        Data = atividade
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

        public ResultViewModel Reabrir(Atividade atividade)
        {
            
            using (NpgsqlConnection conexao = new NpgsqlConnection(strconexao))
            {
                try
                {
                    var query = @"Update Atividade Set                                         
                                    DataEncerramento = NULL
                                    Where Id = @Id";
                    conexao.Execute(query, atividade);

                    return new ResultViewModel()
                    {
                        Success = true,
                        Message = "Atividade alterada com sucesso",
                        Data = atividade
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

        public ResultViewModel Delete(Atividade atividade)
        {
            var validate = ValidaDelete(atividade);
            if (!validate.Success) return validate;

            using (NpgsqlConnection conexao = new NpgsqlConnection(strconexao))
            {
                try
                {
                    var query = "DELETE FROM Atividade WHERE Id =" + atividade.Id;
                    conexao.Execute(query);

                    return new ResultViewModel()
                    {
                        Success = true,
                        Message = "Atividade eliminada com sucesso",
                        Data = atividade
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

        public ResultViewModel AlteraPrioridade(JsonPrioridade prioridade)
        {
            using (NpgsqlConnection conexao = new NpgsqlConnection(strconexao))
            {
                try
                {
                    var query = @"Update Atividade Set 
                                    Prioridade   = @Prioridade                                 
                                    Where Id = @Id";
                    conexao.Execute(query, prioridade);

                    return new ResultViewModel()
                    {
                        Success = true,
                        Message = "Atividade alterada com sucesso",
                        Data = prioridade
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

        private ResultViewModel ValidaUpdate(Atividade atividade)
        {
            var listErros = new List<string>();   
            
            if (atividade.Descricao?.TrimEnd() == "asd")
            {
                listErros.Add("Descrição deve ser diferente de 'asd'");
            }
            
            //Validacao para evitar erro no SQL
            if (atividade.Data <= new DateTime(1800, 01, 01) || atividade.Data >= new DateTime(2100, 01, 01))
            {
                listErros.Add("Data deve estar entra 01/01/1800 e 01/01/2100");                
            }

            return new ResultViewModel()
            {
                Success = listErros.Count() == 0,
                Message = listErros.Count() > 0 ? "Ocorreram erros" : "",
                Data = listErros
            };            
        }

        private ResultViewModel ValidaDelete(Atividade atividade)
        {
            return new ResultViewModel()
            {
                Success = true,
                Message = "",
                Data = atividade
            };            
        }        
    }
}
