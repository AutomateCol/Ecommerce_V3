using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EliteFlower.Models
{
    class Btemplate
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string ID { get; set; }

        [BsonElement("Configuration")]
        public string[] Template { get; set; }

        [BsonElement("Percentage")]
        public string[] Percentage { get; set; }

        [BsonElement("Add_on")]
        public string[] Add_on { get; set; }

    }
}
