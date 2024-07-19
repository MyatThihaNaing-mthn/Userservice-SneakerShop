using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UserServiceApi.Configurations;
using UserServiceApi.Models;

namespace UserServiceApi.Services;

public class JwtGenerator {
    private readonly JwtSettings _jwtSettings;

    public JwtGenerator(IOptions<JwtSettings> jwtSettings){
        _jwtSettings = jwtSettings.Value;
    }

    public JwtSecurityToken GetJwtSecurityToken(User user){
        var claims = new []{
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("role", user.Role)
        };

        var SigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));
        var creds = new SigningCredentials(key: SigningKey, algorithm: SecurityAlgorithms.HmacSha256);

        return new JwtSecurityToken(
            issuer : _jwtSettings.Issuer,
            audience : _jwtSettings.Audience,
            expires : DateTime.UtcNow.AddHours(1),
            claims : claims,
            signingCredentials : creds
        );

    }
}