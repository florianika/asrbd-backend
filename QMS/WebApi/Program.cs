using Application.Ports;
using Application.Rule.CreateRule;
using Application.Rule.GetAllRules;
using Application.Rule.GetRule;
using Application.Rule.GetRulesByEntity;
using Application.Rule.GetRulesByVarableAndEntity;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using System.Text.Json.Serialization;
using WebApi.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("QMSConnectionString");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));


builder.Services.AddScoped<IRuleRepository, RuleRepository>();

builder.Services.AddScoped<CreateRule>();
builder.Services.AddScoped<GetAllRules>();
builder.Services.AddScoped<GetRulesByVarableAndEntity>();
builder.Services.AddScoped<GetRulesByEntity>();
builder.Services.AddScoped<GetRule>();

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
                .GetBytes("key test")),
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

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

