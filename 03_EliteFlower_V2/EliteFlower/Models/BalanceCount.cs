using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EliteFlower.Models
{
    public class BalanceCount
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int _Id { get; set; }
        [BsonElement("Stage")]
        public int Stage { get; set; }
        [BsonElement("Count")]
        public float Count { get; set; }
        [BsonElement("ID1")]
        public float ID1 { get; set; }
        [BsonElement("ID2")]
        public float ID2 { get; set; }
        [BsonElement("ID3")]
        public float ID3 { get; set; }
        [BsonElement("File")]
        public string File { get; set; }
    }
}
