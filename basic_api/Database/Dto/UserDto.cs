using basic_api.Database.Models;
using System.Text.Json.Serialization;

namespace basic_api.Database.Dto
{
    public class UserDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? GroupId { get; set; }

        public UserDto(string id, string name, string? groupId)
        {
            Id = id;
            Name = name;
            GroupId = groupId;
        }
    }

}
