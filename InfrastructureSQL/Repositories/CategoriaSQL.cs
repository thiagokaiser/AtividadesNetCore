using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Dapper;
using System.Data.SqlClient;
using Core.Models;

namespace InfrastructureSQL.Repositories
{
    public class CategoriaSQL
    {       
        public static IEnumerable<Categoria> Select(string strconexao)
        {                        
            using (SqlConnection conexao = new SqlConnection(strconexao))
            {
                IEnumerable<Categoria> categ = conexao.Query<Categoria>("Select * from Categoria");
                return categ;
            }            
        }

        public static Categoria SelectById(string strconexao, int id)
        {
            using (SqlConnection conexao = new SqlConnection(strconexao))
            {
                Categoria categ = conexao.QueryFirst<Categoria>("Select * from Categoria WHERE Id = @Id", new { Id = id });
                return categ;
            }
        }

        public static string Insert(string strconexao, Categoria categ)
        {            
            string mensagem = "";

            mensagem = CategoriaSQL.ValidaUpdate(categ);
            if (mensagem == "")
            {
                using (SqlConnection conexao = new SqlConnection(strconexao))
                {
                    try
                    {
                        var query = @"INSERT INTO Categoria(Descricao, Cor) 
                                                    VALUES(@Descricao,@Cor); 
                                        SELECT CAST(SCOPE_IDENTITY() as INT);";
                        conexao.Execute(query, categ);
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
        public static string Update(string strconexao, Categoria categ)
        {
            string mensagem = "";
            mensagem = CategoriaSQL.ValidaUpdate(categ);
            if (mensagem == "")
            {
                using (SqlConnection conexao = new SqlConnection(strconexao))
                {
                    try
                    {
                        var query = @"Update Categoria Set 
                                        Descricao = @Descricao,
                                        Cor       = @Cor                                        
                                        Where Id = @Id";
                        conexao.Execute(query, categ);
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

        public static string Delete(string strconexao, Categoria categ)
        {            
            string mensagem = "";

            mensagem = CategoriaSQL.ValidaDelete(categ);
            if (mensagem == "")
            {
                using (SqlConnection conexao = new SqlConnection(strconexao))
                {
                    try
                    {
                        var query = "DELETE FROM Categoria WHERE Id =" + categ.Id;
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
        private static string ValidaUpdate(Categoria categ)
        {
            string mensagem = "";            
            if (categ.Descricao?.TrimEnd() == "asd")
            {
                mensagem = "erro ao alterar";
            }
            return mensagem;
        }
        private static string ValidaDelete(Categoria categ)
        {
            string mensagem = "";            
            return mensagem;
        }        
    }
}
