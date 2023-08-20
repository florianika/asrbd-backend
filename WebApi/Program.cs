
using Application.Common.Interfaces;
using Application.Ports;
using Application.RolePermission.CreateRolePermission;
using Application.RolePermission.DeleteRolePermission;
using Application.RolePermission.GetAllPermssions;
using Application.RolePermission.GetPermissionsByRole;
using Application.RolePermission.GetPermissionsByRoleAndEntity;
using Application.RolePermission.GetPermissionsByRoleAndEntityAndVariable;
using Application.RolePermission.UpdateRolePermission;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// REGISTER SERVICES HERE
string connectionString = builder.Configuration.GetConnectionString("ASRBDConnectionString");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddSingleton<ICryptographyService, CryptographyService>();

var jwtSettingsConfiguration = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettingsConfiguration);
var jwtSettings = jwtSettingsConfiguration.Get<JwtSettings>();


builder.Services.AddSingleton<IUserLockSettings, UserLockSettingsService>();


builder.Services.AddScoped<IAuthTokenService, JwtService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<CreateUser>();
builder.Services.AddScoped<Login>();
builder.Services.AddScoped<RefreshToken>();
builder.Services.AddScoped<SignOut>();
builder.Services.AddScoped<GetAllUsers>();
builder.Services.AddScoped<UpdateUserRole>();
builder.Services.AddScoped<TerminateUser>();
builder.Services.AddScoped<ActivateUser>();
builder.Services.AddScoped<GetUser>();
builder.Services.AddScoped<CreateRolePermission>();
builder.Services.AddScoped<GetAllPermissions>();
builder.Services.AddScoped<GetPermissionsByRole>();
builder.Services.AddScoped<GetPermissionsByRoleAndEntity>();
builder.Services.AddScoped<GetPermissionsByRoleAndEntityAndVariable>();
builder.Services.AddScoped<DeleteRolePermission>();
builder.Services.AddScoped<UpdateRolePermission>();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {accessToken}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(jwtSettings.AccessTokenSettings.SecretKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
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
