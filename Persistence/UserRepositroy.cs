using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UserServiceApi.Configurations;
using UserServiceApi.DTOs;
using UserServiceApi.Mappers;
using UserServiceApi.Models;

namespace UserServiceApi.Persistence;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _userCollection;

    public UserRepository(IOptions<UserDatabaseSettings> databaseSettings){
        var MongoClient = new MongoClient(databaseSettings.Value.ConnectionName);
        var MongoDatabase = MongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _userCollection = MongoDatabase.GetCollection<User>(databaseSettings.Value.CollectionName);
    }
    public async Task<AdminUser> CreateAdmin(RegisterAdminRequest request)
    {
        AdminUser admin = ObjectsMapper.ConvertAdminUserFromRegisterAdminRequest(request);
        await _userCollection.InsertOneAsync(admin);
        return admin;
    }

    public async Task<CustomerUser> CreateCustomer(RegisterCustomerRequest request)
    {
        CustomerUser customer = ObjectsMapper.ConvertCustomerFromRequest(request);
        await _userCollection.InsertOneAsync(customer);
        return customer;
    }

    public async Task<User> FindUserByEmail(string email)
    {
        var filter = Builders<User>.Filter
                                        .Eq(obj => obj.Email, email);
        User user = await _userCollection.Find(filter).FirstOrDefaultAsync();
        return user;
    }
}