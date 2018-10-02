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
    public class AtividadeCRUD
    {       
        public static IEnumerable<Atividade> Select()
        {
            string strconexao = StrConexao.GetString("SQL");
                        
            using (SqlConnection conexao = new SqlConnection(strconexao))
            {
                IEnumerable<Atividade> ativs = conexao.Query<Atividade>("Select * from Atividade");
                return ativs;
            }
            
        }

        public static Atividade SelectById(int id)
        {
            string strconexao = StrConexao.GetString("SQL");

            using (SqlConnection conexao = new SqlConnection(strconexao))
            {
                Atividade ativ = conexao.QueryFirst<Atividade>("Select * from Atividade WHERE Id = @Id", new { Id = id });
                return ativ;
            }

        }

        public static string Insert(Atividade atividade)
        {
            string strconexao = StrConexao.GetString("SQL");
            string mensagem = "";            

            mensagem = AtividadeCRUD.ValidaUpdate(atividade);
            
            if (mensagem == "")
            {                
                using (SqlConnection conexao = new SqlConnection(strconexao))
                {
                    try
                    {
                        var query = @"INSERT INTO Atividade(Descricao, Responsavel,  Setor,  Categoria) 
                                                    VALUES(@Descricao,@Responsavel, @Setor, @Categoria); 
                                      SELECT CAST(SCOPE_IDENTITY() as INT);";
                        conexao.Execute(query, atividade);
                        mensagem = "Atividade adicionada com sucesso";
                    }
                    catch (Exception ex)
                    {
                        mensagem = ex.ToString();
                    }                    
                    return mensagem;
                }
            }
            else
            {
                return mensagem;
            }
        }     
        
        public static string Update(Atividade atividade)
        {
            string strconexao = StrConexao.GetString("SQL");
            string mensagem = "";

            mensagem = AtividadeCRUD.ValidaUpdate(atividade);
            
            if (mensagem == "")
            {
                using (SqlConnection conexao = new SqlConnection(strconexao))
                {
                    try
                    {
                        var query = @"Update Atividade Set 
                                      Descricao = @Descricao,
                                      Setor     = @Setor
                                      Where Id = @Id";
                        conexao.Execute(query, atividade);
                        mensagem = "Atividade alterada com sucesso";
                    }
                    catch (Exception ex)
                    {
                        mensagem = ex.ToString();
                    }
                    return mensagem;
                }
            }
            else
            {
                return mensagem;
            }
        }

        public static string Delete(Atividade atividade)
        {
            string strconexao = StrConexao.GetString("SQL");
            string mensagem = "";
                        
            mensagem = AtividadeCRUD.ValidaDelete(atividade);                        
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
                    return mensagem;
                }
            }
            else
            {
                return mensagem;
            }
        }
        private static string ValidaUpdate(Atividade atividade)
        {
            string mensagem = "";            
            if (atividade.Descricao == "asd")
            {
                mensagem = "erro ao alterar";
            }            
            return mensagem;
        }
        private static string ValidaDelete(Atividade atividade)
        {
            string mensagem = "";
            Atividade ativ = Banco.AtividadeCRUD.SelectById(atividade.Id);
            if (ativ.Descricao.TrimEnd() == "zxc")
            {
                mensagem = "erro ao eliminar";

            }
            
            return mensagem;
        }
    }
}
