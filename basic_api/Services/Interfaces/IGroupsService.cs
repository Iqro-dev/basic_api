using basic_api.Database.Models;

namespace basic_api.Services.Interfaces
{
    public interface IGroupsService
    {
        Task<List<Group>> GetAsync();
        Task<Group?> GetAsync(string id);
        Task CreateAsync(Group newGroup);
        Task UpdateAsync(string id, Group updatedGroup);
        Task RemoveAsync(string id);
    }
}
