using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Atividades.Banco
{
    public class StrConexao
    {
        public static string GetString(string banco)
        {
            string strconexao = "";
            switch (banco)
            {
                case "SQL":
                    strconexao = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AtividadesDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                    break;
                case "Mongo":
                    strconexao = "";
                    break;                
            }            
            return strconexao;
        }        
    }
}
