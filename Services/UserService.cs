using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UserServiceApi.Configurations;
using UserServiceApi.DTOs;
using UserServiceApi.Interfaces;
using UserServiceApi.Mappers;
using UserServiceApi.Models;
using UserServiceApi.Persistence;

namespace UserServiceApi.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository){
        _userRepository = userRepository;
    }


    public async Task<AdminDTO> RegisterAdmin(RegisterAdminRequest request)
    {
       AdminUser admin = await _userRepository.CreateAdmin(request);
       AdminDTO adminDTO = ObjectsMapper.ConvertToAdminDTOFromAdmin(admin);
       return adminDTO;
    }

    public async Task<CustomerDTO> RegisterCustomer(RegisterCustomerRequest request)
    {
        CustomerUser customer = await _userRepository.CreateCustomer(request);
        return ObjectsMapper.ConvertCustomerDTOFromCustomer(customer);
    }

    public Task<User?> FindUserWithEmailAndPassword(LogInRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> LogIn(LogInRequest request)
    {
        User? user = await _userRepository.FindUserByEmail(request.Email);
        if(user != null && user.Password == request.Password){
            return user;
        }
        return null;
    }

    public async Task<UserDTO?> FindUserByEmail(string email)
    {
        User? user = await _userRepository.FindUserByEmail(email);
        if(user != null){
            if(user.GetType() == typeof(CustomerUser)){
                return ObjectsMapper.ConvertCustomerDTOFromCustomer((CustomerUser)user);
            }
            else if(user.GetType() == typeof(AdminUser)){
                return ObjectsMapper.ConvertToAdminDTO((AdminUser)user);
            }
        }
        return null;
    }
}

