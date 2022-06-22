using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EliteFlower.Models
{
    class IDStages
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int _Id { get; set; }
        [BsonElement("Vase")]
        public string Vase { get; set; }
        [BsonElement("Stage1")]
        public int Stage1 { get; set; }
        [BsonElement("Stage2")]
        public int Stage2 { get; set; }
        [BsonElement("Stage3")]
        public int Stage3 { get; set; }
        [BsonElement("Status")]
        public string status { get; set; }
    }
}
