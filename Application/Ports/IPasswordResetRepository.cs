using Domain;

namespace Application.Ports
{
    public interface IPasswordResetRepository
    {
        Task SavePasswordToken(PasswordResetToken token);
        Task<PasswordResetToken?> GetLatestToken(Guid userId);
        Task<PasswordResetToken?> FindByUserAndHash(Guid userId, string tokenHash);
        Task MarkConsumed(PasswordResetToken token);
        Task RemoveAllPasswordTokensForUser(Guid userId); // hygiene
    }
}
