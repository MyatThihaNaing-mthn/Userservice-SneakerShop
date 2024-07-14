using UserServiceApi.DTOs;
using UserServiceApi.Models;

namespace UserServiceApi.Persistence;

public interface IUserRepository {
    
    Task<AdminUser> CreateAdmin(RegisterAdminRequest request);
    Task<User> FindUserByEmail(string Email);


    Task<CustomerUser> CreateCustomer(RegisterCustomerRequest request);
}