
namespace Application.Ports
{
    public interface IAuthTokenService
    {
        Task<string> GenerateIdToken(Domain.User user);
        Task<string> GenerateAccessToken(Domain.User user);
        Task<string> GenerateRefreshToken();
        Task<Guid> GetUserIdFromToken(string token);
        Task<int> GetRefreshTokenLifetimeInMinutes();
        Task<bool> IsTokenValid(string accessToken, bool validateLifeTime);
        Task<string?> GetUserRoleFromToken(string token);
    }
}
