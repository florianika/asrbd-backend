using Application.User.GetAllUsers;
using Domain;
using Domain.Enum;

namespace Application.Ports
{
    public interface IAuthRepository
    {
        Task<Domain.User> GetUserByEmail(string email);
        Task UpdateUser(Domain.User user);
        Task<Domain.User> GetUserByUserId(Guid userId);
        Task CreateUser(Domain.User user);
        Task AddClaim(Guid userId, Claim claim);
        Task AddRefreshToken (Guid userId, RefreshToken refreshToken);
        Task UpdateRefreshToken (Guid userId, RefreshToken refreshToken);
        Task LockAccount(Domain.User user);
        //Task UnLockAccount(Domain.User.User user);
        Task<List<Domain.User>> GetAllUsers();
        Task<bool> CheckIfUserExists(Guid userId);
        Task UpdateUserRole(Guid userId, AccountRole accountRole);
        Task UpdateAccountUser(Guid userId, AccountStatus accountStatus);
        Task<Domain.User> FindUserById(Guid userId);
        Task UnlockAccount(Domain.User user);
        Task<Domain.User> FindUserByEmail(string email);
    }
}
