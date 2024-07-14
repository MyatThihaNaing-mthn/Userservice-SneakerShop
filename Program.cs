using UserServiceApi.Configurations;
using UserServiceApi.Interfaces;
using UserServiceApi.Services;
using Steeltoe.Discovery.Client;
using UserServiceApi.Persistence;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.
builder.Services.Configure<UserDatabaseSettings>(builder.Configuration.GetSection(UserDatabaseSettings.SectionName));
// Register to Eureka Server
//builder.Services.AddDiscoveryClient(configuration);
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddControllers();

var app = builder.Build();


app.MapControllers();

app.Run();
