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

namespace Atividades.SQL
{
    public class AtividadeSQL
    {       
        public static string Insert(Atividade atividade)
        {
            string strconexao = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AtividadesDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            
            string mensagem = "";
            int valida = 0;

            if (atividade.Descricao == "asd")
            {
                mensagem = "para de fazer merda";
                valida = 1;

            }
            if (valida == 0)
            {
                using (SqlConnection conexao = new SqlConnection(strconexao))
                {
                    try
                    {
                        var query = "INSERT INTO Atividade(Descricao, Setor) VALUES(@Descricao, @Setor); SELECT CAST(SCOPE_IDENTITY() as INT);";
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
        private static string ValidaUpdate(Atividade atividade)
        {
            string mensagem;
            if (atividade.Descricao == "asd")
            {
                mensagem = "para de fazer merda";
                valida = 1;

            }

            return "";
        }
    }
}
