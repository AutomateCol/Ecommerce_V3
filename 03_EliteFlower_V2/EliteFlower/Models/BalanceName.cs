using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EliteFlower.Models
{
    public class BalanceName
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int _Id { get; set; }
        [BsonElement("Stage")]
        public int Stage { get; set; }
        [BsonElement("Count")]
        public float Count { get; set; }
        [BsonElement("ID1")]
        public string ID1 { get; set; }
        [BsonElement("ID2")]
        public string ID2 { get; set; }
        [BsonElement("ID3")]
        public string ID3 { get; set; }
        [BsonElement("File")]
        public string File { get; set; }
    }
}
