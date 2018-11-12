using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Atividades.Classes
{
    public class JsonPrioridade
    {        
        
        public string Id { get; set; }

        public string Prioridade { get; set; }


    }
}
