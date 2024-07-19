using UserServiceApi.Configurations;
using UserServiceApi.Interfaces;
using UserServiceApi.Services;
using Steeltoe.Discovery.Client;
using UserServiceApi.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.Configure<UserDatabaseSettings>(builder.Configuration.GetSection(UserDatabaseSettings.SectionName));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.SectionName));
var jwtSettings = builder.Configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>() ?? throw new InvalidOperationException("JWT settings are not configured properly.");

// Register to Eureka Server
builder.Services.AddDiscoveryClient(configuration);


builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddTransient<JwtGenerator>();
builder.Services.AddControllers();

builder.Services.AddAuthentication(auth =>
    {
        auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
)
.AddJwtBearer(options => {
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
                                            {
                                                ValidateIssuer = true,
                                                ValidIssuer = jwtSettings.Issuer,
                                                ValidateAudience = true,
                                                ValidAudience = jwtSettings.Audience,
                                                ValidateIssuerSigningKey = true,
                                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SigningKey))
                                            };
}
);

var app = builder.Build();

// TODO Add Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
