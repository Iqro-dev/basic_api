using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace basic_api.Database.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("name")]
        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("email")]
        [BsonElement("email")]
        public string Email { get; set; } = null!;

        [JsonPropertyName("age")]
        [BsonElement("age")]
        public int Age { get; set; }

        [JsonPropertyName("groupId")]
        [BsonElement("groupId")]
        public string? GroupId { get; set; }
    }
}
