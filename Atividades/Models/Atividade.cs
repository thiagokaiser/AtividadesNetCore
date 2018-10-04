using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Atividades.Models
{
    public class Atividade
    {        
        [Key]
        [BsonId()]
        public string Id { get; set; }

        public string Descricao { get; set; }

        public string Responsavel { get; set; }

        public string Setor { get; set; }

        public string Categoria { get; set; }        

    }
}
