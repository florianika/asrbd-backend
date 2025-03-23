﻿using Application.Exceptions;
using Application.Ports;
using Domain;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Json;
using Claim = System.Security.Claims.Claim;

namespace Infrastructure.Services
{
    public class JwtService : IAuthTokenService
    {
        private readonly IOptions<JwtSettings> _settings;
        public JwtService(IOptions<JwtSettings> settings)
        {
            _settings = settings;
        }
        private ClaimsIdentity AddAccessTokenClaims(User user) {
            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new System.Security.Claims.Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claimsIdentity.AddClaim(new System.Security.Claims.Claim(ClaimTypes.Role, user.AccountRole.ToString()));
            return claimsIdentity;
        }
        private ClaimsIdentity AddIdTokenClaims(User user) {
            var claimsIdentity = AddAccessTokenClaims(user);
            claimsIdentity.AddClaim(new System.Security.Claims.Claim(ClaimTypes.Name, user.Name));
            claimsIdentity.AddClaim(new System.Security.Claims.Claim(ClaimTypes.Email, user.Email));
            claimsIdentity.AddClaim(new System.Security.Claims.Claim(ClaimTypes.Surname, user.LastName));
            // Kontrollojmë nëse ekziston një claim me type "district"
            var districtClaim = user.Claims?.FirstOrDefault(c => c.Type == "district");
            if (districtClaim != null && int.TryParse(districtClaim.Value, out int districtId))
            {
                claimsIdentity.AddClaim(new System.Security.Claims.Claim("districtId", districtId.ToString())); // Shtojmë districtId
            }
            return claimsIdentity;
        }

        public Task<string> GenerateAccessToken(User user)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _settings.Value.AccessTokenSettings.SecretKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var claimsIdentity = AddAccessTokenClaims(user);
            // Access Token must only carry the user Id
            var jwtHandler = new JwtSecurityTokenHandler();

            var jwt = jwtHandler.CreateJwtSecurityToken(
                issuer: _settings.Value.AccessTokenSettings.Issuer,
                audience: _settings.Value.AccessTokenSettings.Audience,
                subject: claimsIdentity,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddSeconds(_settings.Value.AccessTokenSettings.LifeTimeInSeconds),
                issuedAt: DateTime.Now,
                signingCredentials: signingCredentials);

            return Task.FromResult(jwtHandler.WriteToken(jwt));
        }

        public Task<string> GenerateIdToken(User user)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _settings.Value.AccessTokenSettings.SecretKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var claimsIdentity = AddIdTokenClaims(user);

            var jwtHandler = new JwtSecurityTokenHandler();

            var jwt = jwtHandler.CreateJwtSecurityToken(
                issuer: _settings.Value.AccessTokenSettings.Issuer,
                audience: _settings.Value.AccessTokenSettings.Audience,
                subject: claimsIdentity,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddSeconds(_settings.Value.AccessTokenSettings.LifeTimeInSeconds),
                issuedAt: DateTime.Now,
                signingCredentials: signingCredentials);

            return Task.FromResult(jwtHandler.WriteToken(jwt));
        }

        public Task<string> GenerateRefreshToken()
        {
            var size = _settings.Value.RefreshTokenSettings.Length;
            var buffer = new byte[size];
            //using var rng = new RNGCryptoServiceProvider();
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(buffer);
            return Task.FromResult(Convert.ToBase64String(buffer));
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
            if(jwtHandler.CanReadToken(token)) {
                try
                {
                    TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters(validateLifeTime);
                    jwtHandler.ValidateToken(token, tokenValidationParameters, out _);
                    return Task.FromResult(true);
                }
                catch {
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
