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

        public string Insert(Categoria categ)
        {            
            string mensagem = "";

            mensagem = ValidaUpdate(categ);
            if (mensagem == "")
            {
                using (NpgsqlConnection conexao = new NpgsqlConnection(strconexao))
                {
                    try
                    {
                        var query = @"INSERT INTO Categoria(Descricao, Cor) 
                                                    VALUES(@Descricao,@Cor); ";
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

        public string Update(Categoria categ)
        {
            string mensagem = "";
            mensagem = ValidaUpdate(categ);
            if (mensagem == "")
            {
                using (NpgsqlConnection conexao = new NpgsqlConnection(strconexao))
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

        public string Delete(Categoria categ)
        {            
            string mensagem = "";

            mensagem = ValidaDelete(categ);
            if (mensagem == "")
            {
                using (NpgsqlConnection conexao = new NpgsqlConnection(strconexao))
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

        private string ValidaUpdate(Categoria categ)
        {
            string mensagem = "";            
            if (categ.Descricao?.TrimEnd() == "asd")
            {
                mensagem = "erro ao alterar";
            }
            return mensagem;
        }

        private string ValidaDelete(Categoria categ)
        {
            string mensagem = "";                        
            return mensagem;
        }
    }
}
