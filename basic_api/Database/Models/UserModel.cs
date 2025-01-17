using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace basic_api.Database.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [JsonPropertyName("Name")]
        [BsonElement("Name")]
        public required string Name { get; set; }
        public required string Email { get; set; }
        public int Age { get; set; }
    }
}
