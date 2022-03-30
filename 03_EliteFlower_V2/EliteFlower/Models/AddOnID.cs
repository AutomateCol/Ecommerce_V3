using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EliteFlower.Models
{
    public class AddOnID
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string ID { get; set; }
        [BsonElement("Url")]
        public string Url { get; set; }
    }
}
