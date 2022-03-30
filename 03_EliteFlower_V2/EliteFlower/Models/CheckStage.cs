using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace EliteFlower.Models
{
    class CheckStage
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int _Id { get; set; }
        [BsonElement("Stage")]
        public int Stage { get; set; }
    }
}
