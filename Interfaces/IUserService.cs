using UserServiceApi.DTOs;
using UserServiceApi.Models;

namespace UserServiceApi.Interfaces;

public interface IUserService{

    Task<AdminDTO> RegisterAdmin(RegisterAdminRequest request);
    Task<CustomerDTO> RegisterCustomer(RegisterCustomerRequest request);
    Task<User?> FindUserWithEmailAndPassword(LogInRequest request);
    Task<UserDTO?> FindUserByEmail(string email);
    Task<User?> LogIn(LogInRequest request);
}

