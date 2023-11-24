using Application.Ports;
using Application.Exceptions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Infrastructure.Services
{
    public class JwtService : IAuthTokenService
    {
        private readonly IOptions<JwtSettings> _settings;
        public JwtService(IOptions<JwtSettings> settings)
        {
            _settings = settings;
        }


        public Task<int> GetRefreshTokenLifetimeInMinutes()
        {
            return Task.FromResult(_settings.Value.RefreshTokenSettings.LifeTimeInMinutes);
        }
        public Task<Guid> GetUserIdFromToken(string token)
        {
            try
            {
                var jwtHandler = new JwtSecurityTokenHandler();
                var userIdClaim = jwtHandler.ReadJwtToken(token).Claims
                    .FirstOrDefault(c => c.Type.Equals(JwtRegisteredClaimNames.NameId, StringComparison.InvariantCultureIgnoreCase))
                        ?? throw new InvalidTokenException("Invalid jwt token");
                return Task.FromResult(Guid.Parse(userIdClaim.Value));
            }
            catch (Exception ex)
            {
                throw new InvalidTokenException(ex.Message, ex);
            }
        }

        public Task<bool> IsTokenValid(string token, bool validateLifeTime)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            if (jwtHandler.CanReadToken(token))
            {
                try
                {
                    TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters(validateLifeTime);
                    jwtHandler.ValidateToken(token, tokenValidationParameters, out _);
                    return Task.FromResult(true);
                }
                catch
                {
                    return Task.FromResult(false);
                }
            }
            return Task.FromResult(false);
        }

        private TokenValidationParameters GetTokenValidationParameters(bool validateLifeTime)
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = validateLifeTime,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _settings.Value.AccessTokenSettings.Issuer,
                ValidAudience = _settings.Value.AccessTokenSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_settings.Value.AccessTokenSettings.SecretKey)),
                ClockSkew = TimeSpan.FromMinutes(0)
            };
        }
    }
}
