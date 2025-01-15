namespace basic_api.Database.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public int Age { get; set; }
    }
}
