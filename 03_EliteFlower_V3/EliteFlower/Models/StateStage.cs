using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EliteFlower.Models
{
    class StateStage
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int _Id { get; set; }
        [BsonElement("ID")]
        public string ID { get; set; }
        [BsonElement("StageN")]
        public int StageN { get; set; }
        [BsonElement("Check")]
        public bool Check { get; set; }
        [BsonElement("Quantity")]
        public float Quantity { get; set; }
        [BsonElement("Status")]
        public string Status { get; set; }

    }
}
