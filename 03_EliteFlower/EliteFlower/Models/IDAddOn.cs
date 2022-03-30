using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EliteFlower.Models
{
    public class IDAddOn
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int _Id { get; set; }
        [BsonElement("AddOn")]
        public string AddOn { get; set; }
        [BsonElement("Count")]
        public int Count { get; set; }
        [BsonElement("File")]
        public string File { get; set; }
    }
}
