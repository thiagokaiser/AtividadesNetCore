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

namespace Atividades.Banco
{
    public class AtividadeSQL
    {       
        public static IEnumerable<Atividade> Select(string strconexao)
        {                        
            using (SqlConnection conexao = new SqlConnection(strconexao))
            {
                IEnumerable<Atividade> ativs = conexao.Query<Atividade>("Select * from Atividade");
                return ativs;
            }            
        }

        public static Atividade SelectById(string strconexao, string id)
        {
            using (SqlConnection conexao = new SqlConnection(strconexao))
            {
                Atividade ativ = conexao.QueryFirst<Atividade>("Select * from Atividade WHERE Id = @Id", new { Id = id });
                return ativ;
            }
        }

        public static string Insert(string strconexao, Atividade atividade)
        {            
            string mensagem = "";

            mensagem = AtividadeSQL.ValidaUpdate(atividade);
            if (mensagem == "")
            {
                using (SqlConnection conexao = new SqlConnection(strconexao))
                {
                    try
                    {
                        var query = @"INSERT INTO Atividade(Descricao, Responsavel,  Setor,  Categoria, Data) 
                                                    VALUES(@Descricao,@Responsavel, @Setor, @Categoria, @Data); 
                                        SELECT CAST(SCOPE_IDENTITY() as INT);";
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
        public static string Update(string strconexao, Atividade atividade)
        {
            string mensagem = "";
            mensagem = AtividadeSQL.ValidaUpdate(atividade);
            if (mensagem == "")
            {
                using (SqlConnection conexao = new SqlConnection(strconexao))
                {
                    try
                    {
                        var query = @"Update Atividade Set 
                                        Descricao   = @Descricao,
                                        Responsavel = @Responsavel,
                                        Setor       = @Setor,
                                        Categoria   = @Categoria,
                                        Data        = @Data
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

        public static string Delete(string strconexao, Atividade atividade)
        {            
            string mensagem = "";

            mensagem = AtividadeSQL.ValidaDelete(atividade);
            if (mensagem == "")
            {
                using (SqlConnection conexao = new SqlConnection(strconexao))
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
        private static string ValidaUpdate(Atividade atividade)
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
        private static string ValidaDelete(Atividade atividade)
        {
            string mensagem = "";
            string[] strconexao = StrConexao.GetString();
            Atividade ativ = AtividadeSQL.SelectById(strconexao[1], atividade.Id);
            if (ativ.Descricao?.TrimEnd() == "zxc")
            {
                mensagem = "erro ao eliminar";

            }            
            return mensagem;
        }
    }
}
