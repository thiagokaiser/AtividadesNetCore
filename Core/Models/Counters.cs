using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Counters
    {
        [BsonId()]
        public string Id { get; set; }
        public int seq { get; set; }
    }
}
