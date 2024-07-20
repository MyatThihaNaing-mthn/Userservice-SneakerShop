using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UserServiceApi.Configurations;
using UserServiceApi.DTOs;
using UserServiceApi.Interfaces;
using UserServiceApi.Models;
using UserServiceApi.Services;

namespace UserServiceApi.Controllers;

[Route("api/home")]
[Microsoft.AspNetCore.Mvc.ApiController]
public class ValuesController : Microsoft.AspNetCore.Mvc.ControllerBase
{
    private readonly AppConfiguration appConfiguration;
    private readonly ILogger<ValuesController> _logger;

    public ValuesController(IOptions<AppConfiguration> appconfig, ILogger<ValuesController> logger)
    {
        appConfiguration = appconfig.Value;
        _logger = logger;
    }

    // GET api/values
    [HttpGet]
    public Microsoft.AspNetCore.Mvc.ActionResult<string> Get()
    {
        var value1 = appConfiguration.JwtSettings.Issuer;

        return value1.ToString();
    }
}
