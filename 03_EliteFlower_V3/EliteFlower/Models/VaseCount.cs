using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EliteFlower.Models
{
    public class VaseCount
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int _Id { get; set; }
        [BsonElement("File")]
        public string File { get; set; }
        [BsonElement("Vase")]
        public string Vase { get; set; }
        [BsonElement("Count")]
        public int Count { get; set; }
    }
}
