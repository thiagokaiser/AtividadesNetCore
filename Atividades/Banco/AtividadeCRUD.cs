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

namespace Atividades.Banco
{
    public class AtividadeCRUD
    {        
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
                mensagem = "para de fazer merda";
            }

            return mensagem;
        }
        private static string ValidaDelete(Atividade atividade)
        {
            string mensagem = "";
            return mensagem;
        }
    }
}
