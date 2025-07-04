﻿
namespace Application.Ports
{
    public interface IAuthTokenService
    {
        Task<bool> IsTokenValid(string accessToken, bool validateLifeTime);
        Task<Guid> GetUserIdFromToken(string token);
        Task<bool> IsUserAdmin(string token);
        Task<string?> GetUserRoleFromToken(string token);
    }
}
