using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Atividades.Banco
{
    public class StrConexao
    {
        public static string[] GetString()
        {
            string banco = "SQL";
            string[] strconexao = new string[2];

            strconexao[0] = banco;

            switch (banco)
            {
                case "SQL":
                    strconexao[1] = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AtividadesDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                    break;
                case "Mongo":
                    strconexao[1] = "mongodb://localhost:27017";
                    break;                
            }                 
            return strconexao;
        }                
    }
}
