using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Atividades.Models
{
    public class Atividade
    {        
        [Key]
        public int Id { get; set; }

        public string Descricao { get; set; }

        public string Responsavel { get; set; }

        public string Setor { get; set; }

        public string Categoria { get; set; }

        public static List<Atividade> cria_lista()
        {
            List<Atividade> lista = new List<Atividade>();
            lista.Add(new Atividade { Id = 1, Descricao = "teste" });
            lista.Add(new Atividade { Id = 2, Descricao = "gegegege" });
            lista.Add(new Atividade { Id = 3, Descricao = "gfcvbcvbs" });
            return lista;

        }

    }
}
