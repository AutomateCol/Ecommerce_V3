using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EliteFlower.Models
{
    public class MLlenado
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string _Id { get; set; }
        [BsonElement("ID")]
        public int ID { get; set; }
        [BsonElement("reference")]
        public string[] References { get; set; }
        [BsonElement("quantity")]
        public int[] Quantitys { get; set; }
        [BsonElement("indexReference")]
        public int indRef { get; set; }
    }
}
