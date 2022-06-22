using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace EliteFlower.Models
{
    class LogsEliteFlower
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime startTime { get; set; }
        [BsonElement("sourceMethod")]
        public string sourceMethod { get; set; }
        [BsonElement("window")]
        public string window { get; set; }
        [BsonElement("sourceError")]
        public string sourceError { get; set; }
        [BsonElement("errorType")]
        public string errorType { get; set; }
        [BsonElement("errorCode")]
        public int errorCode { get; set; }
        [BsonElement("verbose")]
        public string verbose { get; set; }
    }
}
