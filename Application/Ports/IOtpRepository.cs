using Domain;

namespace Application.Ports
{
    public interface IOtpRepository
    {
        Task GenerateOtp(Guid userId);
        Task<bool> VerifyOtp(Guid userId, string code);
        Task SaveAsync(OtpRecord rec);
        Task<OtpRecord?> GetLatestAsync(Guid userId);
        Task UpdateAsync(OtpRecord rec);

    }
}
