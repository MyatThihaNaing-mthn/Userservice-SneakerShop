using UserServiceApi.Configurations;
using UserServiceApi.Interfaces;
using UserServiceApi.Services;
using Steeltoe.Discovery.Client;
using UserServiceApi.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Steeltoe.Discovery.Eureka;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfigServer();
var config = builder.Configuration;
var env = builder.Environment;

var jwtSettings = config.GetSection(JwtSettings.SectionName).Get<JwtSettings>() ?? throw new InvalidOperationException("jwtsettings not found..");

ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
ILogger logger = factory.CreateLogger("Program");

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.SectionName));

// Add services to the container.
builder.Services.Configure<UserDatabaseSettings>(config.GetSection(UserDatabaseSettings.SectionName));
builder.Services.Configure<JwtSettings>(config.GetSection(JwtSettings.SectionName));


// Register to Eureka Server
// this method cannot read nested settings
builder.Services.AddDiscoveryClient(config);

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
