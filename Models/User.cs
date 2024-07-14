using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserServiceApi.Models;

public class User{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id{get; set;} = null!;

    [BsonElement("FirstName")]
    public string FirstName{get; set;} = null!;

    [BsonElement("LastName")]
    public string LastName{get; set;} = null!;

    [BsonElement("Email")]
    public string Email{get; set;} = null!;

    [BsonElement("Password")]
    public string Password{get; set;} = null!;

    [BsonElement("Role")]
    public string Role{get; set;} = null!;

    [BsonElement("Token")]
    public string Token{get; set;} = null!;
}