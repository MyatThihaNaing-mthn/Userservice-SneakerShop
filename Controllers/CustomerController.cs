using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using UserServiceApi.Interfaces;

namespace UserServiceApi.Controllers;

[ApiController]
[Route("api/customer")]
public class CustomerController : ControllerBase{
    private readonly IUserService _userService;

    public CustomerController(IUserService userService){
        _userService = userService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetProfile(){
        string? email =  User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(email == null){
            return Unauthorized();
        }
        var user = await _userService.FindUserByEmail(email);
        if (user == null){
            return Unauthorized();
        }
        return Ok(user);
    }
}