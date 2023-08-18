
using Application.Common.Interfaces;
using Application.Ports;
using Application.User.ActivateUser;
using Application.User.CreateUser;
using Application.User.GetAllUsers;
using Application.User.GetUser;
using Application.User.Login;
using Application.User.RefreshToken;
using Application.User.SignOut;
using Application.User.TerminateUser;
using Application.User.UpdateUserRole;
using Infrastructure.Configurations;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);
// REGISTER SERVICES HERE
string connectionString = builder.Configuration.GetConnectionString("ASRBDConnectionString");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddSingleton<ICryptographyService, CryptographyService>();

var jwtSettingsConfiguration = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettingsConfiguration);
var jwtSettings = jwtSettingsConfiguration.Get<JwtSettings>();

//var userLockSettingsConfiguration = builder.Configuration.GetSection("userLockSettings");
//builder.Services.Configure<UserLockSettings>(userLockSettingsConfiguration);
//var userLockSettings = userLockSettingsConfiguration.Get<UserLockSettings>();

builder.Services.AddSingleton<IUserLockSettings, UserLockSettingsService>();


builder.Services.AddScoped<IAuthTokenService, JwtService>();

builder.Services.AddSingleton(provider =>
{
    var rsa = RSA.Create();
    rsa.ImportRSAPrivateKey(source: Convert.FromBase64String(jwtSettings.AccessTokenSettings.PrivateKey), bytesRead: out int _);
    return new RsaSecurityKey(rsa);
});

builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddControllers();

builder.Services.AddScoped<CreateUser>();
builder.Services.AddScoped<Login>();
builder.Services.AddScoped<RefreshToken>();
builder.Services.AddScoped<SignOut>();
builder.Services.AddScoped<GetAllUsers>();
builder.Services.AddScoped<UpdateUserRole>();
builder.Services.AddScoped<TerminateUser>();
builder.Services.AddScoped<ActivateUser>();
builder.Services.AddScoped<GetUser>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth.API", Version = "v1" });
});

builder.Services.AddHealthChecks();
var app = builder.Build();
app.MapHealthChecks("/health");
// REGISTER MIDDLEWARE HERE

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth.API v1"));
}
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
