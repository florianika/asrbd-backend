using ApiGateway.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;
using Ocelot.Provider.Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy",
        policy => policy.SetIsOriginAllowed(origin => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()); // E rëndësishme për SignalR
});

// JWT Configuration
var jwtSettingsConfiguration = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettingsConfiguration);
var jwtSettings = jwtSettingsConfiguration.Get<JwtSettings>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        if (jwtSettings != null)
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(jwtSettings.AccessTokenSettings.SecretKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
    });

// Ocelot Configuration
builder.Services.AddOcelot(builder.Configuration)
    .AddPolly(); // Shto këtë për QoS support

// Logging
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ocelot API Gateway", Version = "v1" });
});

builder.Services.AddControllers();
builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/health");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ocelot API Gateway v1");
    c.SwaggerEndpoint("/api/auth/swagger/v1/swagger.json", "Authentication");
    c.SwaggerEndpoint("/api/qms/swagger/v1/swagger.json", "QMS API");
});

// IMPORTANT: Order matters for middleware!
app.UseCors("CorsPolicy");

// Enable WebSockets BEFORE Ocelot
app.UseWebSockets(new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(2)
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Ocelot should be one of the last middleware
await app.UseOcelot();

app.MapControllers();

app.Run();