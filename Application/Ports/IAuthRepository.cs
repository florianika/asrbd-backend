using Application.User.GetAllUsers;
using Domain.Claim;
using Domain.Enum;
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
        Task LockAccount(Domain.User.User user);
        //Task UnLockAccount(Domain.User.User user);
        Task<List<Domain.User.User>> GetAllUsers();
        Task<bool> CheckIfUserExists(Guid userId);
        Task UpdateUserRole(Guid userId, AccountRole accountRole);
        Task UpdateAccountUser(Guid userId, AccountStatus accountStatus);
        Task<Domain.User.User> FindUserById(Guid userId);
        Task UnlockAccount(Domain.User.User user);
        Task<Domain.User.User> FindUserByEmail(string email);
    }
}
