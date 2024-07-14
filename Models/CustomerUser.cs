using MongoDB.Bson.Serialization.Attributes;

namespace UserServiceApi.Models;

public class CustomerUser : User{
    [BsonElement("Address")]
    public string Address{get;set;} = null!;
}