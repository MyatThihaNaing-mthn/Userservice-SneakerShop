using Microsoft.AspNetCore.Mvc;
using UserServiceApi.DTOs;
using UserServiceApi.Interfaces;
using UserServiceApi.Models;

namespace UserServiceApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : ControllerBase{
    private readonly IUserService _userService;

    public AuthenticationController(IUserService userService){
        _userService = userService;
    }

    [HttpPost("admin/register")]
    public async Task<IActionResult> RegisterAdmin(RegisterAdminRequest request){
        AdminDTO admin = await _userService.RegisterAdmin(request);
        return Created($"api/auth/admin/register/{admin.Id}", new {admin = admin });
    }

    [HttpPost("customer/register")]
    public async Task<IActionResult> RegisterCustomer(RegisterCustomerRequest request){
        CustomerDTO customer = await _userService.RegisterCustomer(request);
        return Created($"api/auth/customer/register/{customer.Id}", customer);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAdmin(LogInRequest request){
        Console.WriteLine("loggin in");
        User? user = await _userService.LogIn(request);
        if(user != null){
            return Ok(user);
        }
        return Unauthorized();
    }
}