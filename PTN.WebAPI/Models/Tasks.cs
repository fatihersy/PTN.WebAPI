using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System.Numerics;

namespace PTN.WebAPI.Models
{
    public class Tasks
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string? Name { get; set; }

        [BsonElement("description")]
        public string? Description { get; set; }

        [BsonElement("startdate")]
        public DateTime? StartDate { get; set; }

        [BsonElement("enddate")]
        public DateTime? EndDate { get; set; }

        [BsonElement("status")]
        public string? Status { get; set; }

    }
}
