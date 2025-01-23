using basic_api.Database.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace basic_api.Services
{
    public class GroupsService
    {
        private readonly IMongoCollection<Group> _groupsCollection;

        public GroupsService(
            IOptions<BasicApiDatabaseSettings> basicApiDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                basicApiDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                basicApiDatabaseSettings.Value.DatabaseName);

            _groupsCollection = mongoDatabase.GetCollection<Group>(
                basicApiDatabaseSettings.Value.GroupsCollectionName);
        }

        public async Task<List<Group>> GetAsync() =>
            await _groupsCollection.Find(_ => true).ToListAsync();

        public async Task<Group?> GetAsync(string id) =>
            await _groupsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Group newGroup) =>
            await _groupsCollection.InsertOneAsync(newGroup);

        public async Task UpdateAsync(string id, Group updatedGroup) =>
            await _groupsCollection.ReplaceOneAsync(x => x.Id == id, updatedGroup);

        public async Task RemoveAsync(string id) =>
            await _groupsCollection.DeleteOneAsync(x => x.Id == id);
    }
}

