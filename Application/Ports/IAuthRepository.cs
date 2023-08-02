using Domain.Claim;
using Domain.RefreshToken;

namespace Application.Ports
{
    public interface IAuthRepository
    {
        Task<Domain.User.User> GetUserByEmail(string email);
        Task UpdateUser(Domain.User.User user);
        Task<Domain.User.User> GetUserByUserId(Guid userId);
        Task CreateUser(Domain.User.User user);
        Task AddClaim(Guid userId, Claim claim);
        Task AddRefreshToken (Guid userId, RefreshToken refreshToken);
        Task UpdateRefreshToken (Guid userId, RefreshToken refreshToken);
    }
}
