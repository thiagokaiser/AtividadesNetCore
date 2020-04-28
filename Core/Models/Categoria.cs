using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Categoria
    {        
        [Key]
        [BsonId()]        
        public int Id { get; set; }

        public string Descricao { get; set; }

        public string Cor { get; set; }                

    }
}
