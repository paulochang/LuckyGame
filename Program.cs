global using FastEndpoints;
global using FastEndpoints.Security;
global using FluentValidation;
global using LuckyGame.Auth;

using FastEndpoints.Swagger;
using LuckyGame.Infrastructure.Database;
using LuckyGame.Infrastructure.Repository;
using LuckyGame.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;


builder.Services.AddFastEndpoints();
builder.Services.AddJWTBearerAuth(config.GetValue<string>("JwtSigningKey")); 

builder.Services.SwaggerDocument();

builder.Services.AddSingleton<IDbConnectionFactory>(_ =>
    new SqliteConnectionFactory(config.GetValue<string>("Database:ConnectionString")));
builder.Services.AddSingleton<DatabaseInitializer>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IRandomService, RandomService>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();
app.UseSwaggerGen();

var databaseInitializer = app.Services.GetRequiredService<DatabaseInitializer>();
await databaseInitializer.InitializeAsync();

app.Run();