using basic_api.Database.Models;
using basic_api.Services.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace basic_api.Services
{
    public class UsersService : IUsersService
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UsersService(
            IOptions<BasicApiDatabaseSettings> basicApiDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                basicApiDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                basicApiDatabaseSettings.Value.DatabaseName);

            _usersCollection = mongoDatabase.GetCollection<User>(
                basicApiDatabaseSettings.Value.UsersCollectionName);
        }

        public async Task<List<User>> GetAsync() =>
            await _usersCollection.Find(_ => true).ToListAsync();

        public async Task<User?> GetAsync(string id) =>
            await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(User newUser) =>
            await _usersCollection.InsertOneAsync(newUser);
        

        public async Task UpdateAsync(string id, User updatedUser) =>
            await _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

        public async Task RemoveAsync(string id) =>
            await _usersCollection.DeleteOneAsync(x => x.Id == id);
    }
}
