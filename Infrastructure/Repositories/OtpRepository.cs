using Application.Ports;
using Domain;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Repositories
{
    public class OtpRepository : IOtpRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _cfg;
        private readonly IAuthRepository _authRepo;
        private readonly INotificationSender _email;
        public OtpRepository(DataContext context,IConfiguration cfg, IAuthRepository authRepo, INotificationSender email)
        {
            _context = context;
            _cfg = cfg;
            _authRepo = authRepo;
            _email = email;
        }
        public async Task SaveAsync(OtpRecord rec)
        {
            // optional hygiene: remove old
            var old = _context.OtpRecords.Where(x => x.UserId == rec.UserId);
            _context.OtpRecords.RemoveRange(old);

            await _context.OtpRecords.AddAsync(rec);
            await _context.SaveChangesAsync();
        }

        public Task<OtpRecord?> GetLatestAsync(Guid userId) =>
        _context.OtpRecords.Where(x => x.UserId == userId)
          .OrderByDescending(x => x.ExpiresAt)
          .FirstOrDefaultAsync();

        public async Task UpdateAsync(OtpRecord rec)
        {
            _context.OtpRecords.Update(rec);
            await _context.SaveChangesAsync();
        }

        public async Task GenerateOtp(Guid userId)
        {
            var ttlMinutes = int.Parse(_cfg["Otp:TtlMinutes"] ?? "10");
            var code = GenerateNumericCode(6);

            var rec = new OtpRecord
            {
                UserId = userId,
                CodeHash = Hash(code),
                ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(ttlMinutes)
            };
            await SaveAsync(rec);

            var user = await _authRepo.FindUserById(userId);
            var body = $@"<p>Përshëndetje {user.Name} {user.LastName},</p>
            <p>Kodi juaj i verifikimit është <b>{code}</b>. Skadon për {ttlMinutes} minuta.</p>";
            await _email.SendEmailAsync(user.Email, "Kodi i verifikimit", body);
        }

        public async Task<bool> VerifyOtp(Guid userId, string code)
        {
            var rec = await GetLatestAsync(userId);
            if (rec is null) return false;
            if (rec.ConsumedAt.HasValue) return false;
            if (rec.ExpiresAt < DateTimeOffset.UtcNow) return false;

            // constant-time compare
            var ok = CryptographicOperations.FixedTimeEquals(
                Convert.FromHexString(rec.CodeHash),
                Convert.FromHexString(Hash(code)));

            rec.Attempts++;
            if (!ok || rec.Attempts > 5)
            {
                await UpdateAsync(rec);
                return false;
            }

            rec.ConsumedAt = DateTimeOffset.UtcNow;
            await UpdateAsync(rec);
            return true;
        }
        private static string GenerateNumericCode(int digits)
        {
            var max = (int)Math.Pow(10, digits);
            int value;
            do
            {
                Span<byte> b = stackalloc byte[4];
                RandomNumberGenerator.Fill(b);
                value = Math.Abs(BitConverter.ToInt32(b)) % max;
            } while (value < max / 10); // ensure length
            return value.ToString(new string('0', digits));
        }

        private static string Hash(string code)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(code));
            return Convert.ToHexString(bytes); // store hex
        }
    }
}
