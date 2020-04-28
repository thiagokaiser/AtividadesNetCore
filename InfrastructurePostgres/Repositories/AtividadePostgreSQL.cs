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
                IEnumerable<Atividade> ativs = new List<Atividade> { };                
                
                ativs = conexao.Query<Atividade, Categoria, Atividade>(@"
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
                IEnumerable<Atividade> ativs = conexao.Query<Atividade, Categoria, Atividade>(@"
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
                Atividade ativ = conexao.Query<Atividade, Categoria, Atividade>(@"
                    Select * from Atividade T1 LEFT JOIN Categoria T2 ON T1.CategoriaId = T2.Id WHERE T1.Id = @Id",
                    (Atividade, Categoria) => {
                        Atividade.Categoria = Categoria;                        
                        return Atividade;}, new { Id = id }).FirstOrDefault();
                
                return ativ;
            }
        }

        public string Insert(Atividade atividade)
        {            
            string mensagem = "";

            mensagem = ValidaUpdate(atividade);
            if (mensagem == "")
            {
                using (NpgsqlConnection conexao = new NpgsqlConnection(strconexao))
                {
                    try
                    {                            
                        var query = @"INSERT INTO Atividade(Descricao, Responsavel,  Setor,  CategoriaId, Data,  Prioridade, Solicitante, Narrativa) 
                                                    VALUES(@Descricao,@Responsavel, @Setor, @CategoriaId, @Data, (Select coalesce(MAX(Prioridade), 0) from Atividade) + 1,
                                                           @Solicitante, @Narrativa); 
                                     ";
                        conexao.Execute(query, atividade);
                        mensagem = "Atividade adicionada com sucesso";
                    }
                    catch (Exception ex)
                    {
                        mensagem = ex.ToString();
                    }                    
                }
            }            
            return mensagem;
        }            
        
        public string Update(Atividade atividade)
        {
            string mensagem = "";
            mensagem = ValidaUpdate(atividade);
            if (mensagem == "")
            {
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
                        
                        mensagem = "Atividade alterada com sucesso";
                    }
                    catch (Exception ex)
                    {
                        mensagem = ex.ToString();
                    }                    
                }
            }
            return mensagem;
        }

        public string UpdateEncerra(Atividade atividade)
        {
            string mensagem = "";
            //mensagem = AtividadeSQL.ValidaUpdate(atividade);
            if (mensagem == "")
            {
                using (NpgsqlConnection conexao = new NpgsqlConnection(strconexao))
                {
                    try
                    {
                        var query = @"Update Atividade Set                                         
                                        DataEncerramento = @DataEncerramento
                                        Where Id = @Id";
                        conexao.Execute(query, atividade);
                        mensagem = "Atividade alterada com sucesso";
                    }
                    catch (Exception ex)
                    {
                        mensagem = ex.ToString();
                    }
                }
            }
            return mensagem;
        }

        public string Reabrir(Atividade atividade)
        {
            string mensagem = "";
            //mensagem = AtividadeSQL.ValidaUpdate(atividade);
            if (mensagem == "")
            {
                using (NpgsqlConnection conexao = new NpgsqlConnection(strconexao))
                {
                    try
                    {
                        var query = @"Update Atividade Set                                         
                                        DataEncerramento = NULL
                                        Where Id = @Id";
                        conexao.Execute(query, atividade);
                        mensagem = "Atividade alterada com sucesso";
                    }
                    catch (Exception ex)
                    {
                        mensagem = ex.ToString();
                    }
                }
            }
            return mensagem;
        }

        public string Delete(Atividade atividade)
        {            
            string mensagem = "";

            mensagem = ValidaDelete(atividade);
            if (mensagem == "")
            {
                using (NpgsqlConnection conexao = new NpgsqlConnection(strconexao))
                {
                    try
                    {
                        var query = "DELETE FROM Atividade WHERE Id =" + atividade.Id;
                        conexao.Execute(query);
                        mensagem = "Atividade eliminada com sucesso";
                    }
                    catch (Exception ex)
                    {
                        mensagem = ex.ToString();
                    }                    
                }
            }
            return mensagem;
        }

        private string ValidaUpdate(Atividade atividade)
        {
            string mensagem = "";            
            if (atividade.Descricao?.TrimEnd() == "asd")
            {
                mensagem = "erro ao alterar";
            }
            
            //Validacao para evitar erro no SQL
            if (atividade.Data <= new DateTime(1800, 01, 01) || atividade.Data >= new DateTime(2100, 01, 01))
            {
                mensagem = "Data deve estar entra 01/01/1800 e 01/01/2100";
            }


            return mensagem;
        }
        private string ValidaDelete(Atividade atividade)
        {
            string mensagem = "";                        
            return mensagem;
        }
        public string AlteraPrioridade(JsonPrioridade prioridade)
        {
            string mensagem = "";
            
            using (NpgsqlConnection conexao = new NpgsqlConnection(strconexao))
            {
                try
                {
                    var query = @"Update Atividade Set 
                                    Prioridade   = @Prioridade                                 
                                    Where Id = @Id";
                    conexao.Execute(query, prioridade);
                    mensagem = "Atividade alterada com sucesso";
                }
                catch (Exception ex)
                {
                    mensagem = ex.ToString();
                }
            }            
            return mensagem;
        }
    }
}
