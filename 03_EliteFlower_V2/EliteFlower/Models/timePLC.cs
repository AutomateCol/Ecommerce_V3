using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EliteFlower.Models
{
    class timePLC
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public int ID { get; set; }
        [BsonElement("timeLed")]
        public double timeLed { get; set; }
        [BsonElement("velocityMotor1")]
        public double velocityMotor1 { get; set; }
        [BsonElement("velocityMotor2")]
        public double velocityMotor2 { get; set; }
        [BsonElement("percentageMotor1")]
        public double percentageMotor1 { get; set; }
        [BsonElement("percentageMotor2")]
        public double percentageMotor2 { get; set; }
    }
}
