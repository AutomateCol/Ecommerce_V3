using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EliteFlower.Models
{
    class IDOverview
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string _Id { get; set; }
        [BsonElement("ID")]
        public int ID { get; set; }
        [BsonElement("overview")]
        public bool Overview { get; set; }
        [BsonElement("recovery")]
        public bool Recovery { get; set; }
        [BsonElement("nameActualFile")]
        public string NameActualFile { get; set; }
        [BsonElement("createDate")]
        public string createDate { get; set; }
        [BsonElement("lastUpdate")]
        public string lastUpdate { get; set; }
        [BsonElement("initialFeed")]
        public bool initialFeed { get; set; }
        [BsonElement("workUp")]
        public bool workUp { get; set; }
        [BsonElement("workMesanin")]
        public bool workMesanin { get; set; }
        [BsonElement("lastPosEmpujador")]
        public int lastPosEmpujador { get; set; }
        [BsonElement("lastPosSorter")]
        public int lastPosSorter { get; set; }
        [BsonElement("ESActive")]
        public bool ESActive { get; set; }
    }
}
