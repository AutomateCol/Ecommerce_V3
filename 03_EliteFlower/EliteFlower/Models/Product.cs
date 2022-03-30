using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EliteFlower.Models
{
    class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string ID { get; set; }
        [BsonElement("package")]
        public int Package { get; set; }
        [BsonElement("url")]
        public string Url { get; set; }
    }
}
