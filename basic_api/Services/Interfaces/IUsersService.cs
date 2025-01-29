using basic_api.Database.Dto;
using basic_api.Database.Models;

namespace basic_api.Services.Interfaces
{
    public interface IUsersService
    {
        Task<List<User>> GetAsync();
        Task<User?> GetAsync(string id);
        Task CreateAsync(User newUser);
        Task UpdateAsync(string id, User updatedUser);
        Task RemoveAsync(string id);
    }
}
