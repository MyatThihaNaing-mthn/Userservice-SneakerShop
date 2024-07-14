using UserServiceApi.DTOs;
using UserServiceApi.Models;

namespace UserServiceApi.Mappers;

public static class ObjectsMapper{
    public static AdminDTO ConvertToAdminDTO(User user){
        AdminDTO admin = new()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role
        };
        return admin;
    }

    public static AdminUser ConvertAdminUserFromRegisterAdminRequest(RegisterAdminRequest request){
        return new(){
          FirstName = request.FirstName,
          LastName = request.LastName,
          Email = request.Email,
          Password = request.Password,
          Role = "Admin"
        };
    }

    public static AdminDTO ConvertToAdminDTOFromAdmin(AdminUser admin){
        return new(){
            Id = admin.Id,
            FirstName = admin.FirstName,
            LastName = admin.LastName,
            Email = admin.LastName,
            Role = admin.Role
        };
    }

    internal static CustomerUser ConvertCustomerFromRequest(RegisterCustomerRequest request)
    {
        return new(){
          FirstName = request.FirstName,
          LastName = request.LastName,
          Email = request.Email,
          Password = request.Password,
          Address = request.Address,
          Role = "Customer"
        };
    }

    internal static CustomerDTO ConvertCustomerDTOFromCustomer(CustomerUser customer)
    {
        return new(){
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            Address = customer.Address,
            Role = customer.Role
        };
    }
}