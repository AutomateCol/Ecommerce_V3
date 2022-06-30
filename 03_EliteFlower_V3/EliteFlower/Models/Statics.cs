using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EliteFlower.Models
{
    public class Statics
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int _id { get; set; }
        [BsonElement("Total_Orders")]
        public int Total_Orders { get; set; }
        [BsonElement("Total_Vases")]
        public int Total_Vases { get; set; }
        [BsonElement("Orders")]
        public object Orders { get; set; }
        [BsonElement("Active_Stations")]
        public int[] Active_Stations { get; set; }
        [BsonElement("Estaciones")]
        public string[] Estaciones { get; set; }
        [BsonElement("Cuenta_Estaciones")]
        public int[] Cuenta_Estaciones { get; set; }
        [BsonElement("AddON")]
        public string[] AddON { get; set; }
        [BsonElement("Cuenta_AddON")]
        public int[] Cuenta_AddON { get; set; }

        //[BsonElement("File")]
        //public string File { get; set; }
    }
}
