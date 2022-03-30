using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EliteFlower.Models
{
    public class Package
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string ID { get; set; }
        [BsonElement("package")]
        public int package { get; set; }
    }
}
