using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EliteFlower.Models
{
    public class DataProduct
    {
        //-- DB info
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string OrderNumber { get; set; }
        [BsonElement("TrackingNumber")]
        public string TrackingNumber { get; set; }
        [BsonElement("Vase")]
        public string Vase { get; set; }
        [BsonElement("AddOnId")]
        public string AddOnId { get; set; }
        [BsonElement("SKU")]
        public string SKU { get; set; }
        [BsonElement("ShipDate")]
        public string ShipDate { get; set; }
        [BsonElement("ShipMethod")]
        public string ShipMethod { get; set; }
        [BsonElement("Origin")]
        public string Origin { get; set; }
        //-- Internal info
        [BsonElement("Readed")]
        public bool Readed { get; set; }
        [BsonElement("ReadedAddon")]
        public bool ReadedAddon { get; set; }
        [BsonElement("ReadedStage")]
        public int[] ReadedStage { get; set; }
        [BsonElement("WhStage")]
        public int WhStage { get; set; }
        [BsonElement("BalanceUUID")]
        public string BalanceUUID { get; set; }
        [BsonElement("Balance")]
        public int Balance { get; set; }
        [BsonElement("BalanceStage")]
        public int BalanceStage { get; set; }
        [BsonElement("ReadedDate")]
        public string ReadedDate { get; set; }
        [BsonElement("Random")]
        public int Random { get; set; }
        [BsonElement("TypeBand")]
        public string TypeBand { get; set; }
        [BsonElement("AddOnBalance")]
        public string AddOnBalance { get; set; }
       

    }
}
