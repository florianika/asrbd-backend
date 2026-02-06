using Application.Common.Interfaces;
using Application.Exceptions;
using Application.Ports;
using Application.User;
using Domain;
using Domain.Enum;
using Infrastructure.Context;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IUserLockSettings _userLockSettings;
        private readonly ICryptographyService _cryptographyService;
        private const string Municipality = "municipality";
        private readonly IConfiguration _cfg;
        private readonly INotificationSender _email;
        public AuthRepository(DataContext context, IUserLockSettings userLockSettings, ICryptographyService cryptographyService, IConfiguration cfg, INotificationSender email)
        {
            _context = context;
            _userLockSettings = userLockSettings;
            _cryptographyService = cryptographyService;
            _cfg = cfg;
            _email = email;
        }
        public async Task CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users
                                .Where(u => u.Email == email)
                                .Include(u => u.RefreshToken)
                                .Include(u => u.Claims)
                                .SingleOrDefaultAsync()
                                ?? throw new NotFoundException($"User with email {email} not found");         
        }

        public async Task<User> GetUserByUserId(Guid userId)
        {
            return await _context.Users
                                    .Where(u => u.Id == userId)
                                    .Include(u => u.Claims)
                                    .Include(u => u.RefreshToken)
                                    .SingleOrDefaultAsync()
                                    ?? throw new NotFoundException($"User with ID {userId} not found");
        }
        public Task UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
        public async Task UpdateRefreshToken(Guid userId, RefreshToken refreshToken) 
        {
            _context.RefreshToken.Update(refreshToken);
            await _context.SaveChangesAsync();
        }
        public async Task AddClaim(Guid userId, Domain.Claim claim)
        { 
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId)
                            ?? throw new NotFoundException($"User with ID {userId} not found");
            user.Claims.Add(claim);
            await _context.SaveChangesAsync();
        }

        public async Task AddRefreshToken(Guid userId, RefreshToken refreshToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId)
                            ?? throw new NotFoundException($"User with ID {userId} not found");
            user.RefreshToken = refreshToken;
            _context.RefreshToken.Add(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task LockAccount(User user)
        {
            user.SigninFails++;
            if(user.SigninFails == _userLockSettings.UserLockSettings.MaxSigninFails)
            {
                user.AccountStatus = AccountStatus.LOCKED;
                user.LockExpiration = DateTime.Now.AddHours(_userLockSettings.UserLockSettings.LockExpirationTime);
                
            }
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

        }
        public async Task UnlockAccount(User user)
        { 
            user.AccountStatus = AccountStatus.ACTIVE;
            user.SigninFails = 0;
            user.LockExpiration = null;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task<List<User>> GetAllUsers(Guid requestUserId)
        {
            
            return await _context.Users
                .Where(u => u.Id != requestUserId)
                .Include(u => u.Claims)
                .ToListAsync();
        }

        public async Task<List<User>> GetAllNonAdminUsers(Guid requestUserId)
        {
            return await _context.Users
                .Where(u => u.Id != requestUserId && u.AccountRole != AccountRole.ADMIN)
                .Include(u => u.Claims)
                .ToListAsync();
        }
        
        
        public async Task UpdateUserRole(Guid userId, AccountRole accountRole)
        {
            var userToUpdate = await _context.Users
                                   .FirstOrDefaultAsync(u => u.Id == userId) 
                               ?? throw new NotFoundException($"User with ID {userId} not found");
            userToUpdate.AccountRole = accountRole;
            await _context.SaveChangesAsync();  
        }

        public async Task SetUserMunicipality(Guid userId, string municipalityCode)
        {
            var userToUpdate= await _context.Users
                                  .Include(user => user.Claims)
                                  .SingleOrDefaultAsync(u => u.Id == userId) 
                              ?? throw new NotFoundException($"User with {userId} not found");
            
            
            var municipalityClaim = userToUpdate.Claims.SingleOrDefault(c => c.Type == Municipality);
            if (municipalityClaim != null)
            {
                municipalityClaim.Value = municipalityCode;
            }
            else
            { 
                userToUpdate.Claims.Add(new Domain.Claim(){Type = Municipality, Value = municipalityCode});
            }
            
            await _context.SaveChangesAsync();
        }
        
        public async Task<bool> CheckIfUserExists(Guid userId)
        {
            return await _context.Users.AnyAsync(u => u.Id == userId);
        }

        public async Task UpdateAccountUser(Guid userId, AccountStatus accountStatus)
        {
            var userToUpdate = await _context.Users
                                   .FirstOrDefaultAsync(u => u.Id == userId)
                            ?? throw new NotFoundException($"User with ID {userId} not found");
            userToUpdate.AccountStatus = accountStatus;
            await _context.SaveChangesAsync();
           
        }

        public async Task<User> FindUserById(Guid userId)
        { 
            return await _context.Users
                       .Include(u=>u.RefreshToken)
                       .Include(u=>u.Claims)
                       .FirstOrDefaultAsync(u => u.Id.Equals(userId))
                    ?? throw new NotFoundException($"User with ID {userId} not found");
        }

        public async Task<User> FindUserByEmail(string email)
        {
            return await _context.Users
                       .Include(u => u.RefreshToken)
                       .Include(u=>u.Claims)
                       .SingleOrDefaultAsync(u => u.Email == email && u.AccountStatus != AccountStatus.TERMINATED)
                    ?? throw new NotFoundException($"User with email {email} not found");
        }

        public async Task SavePasswordResetToken(PasswordResetToken token)
        {
            await _context.PasswordResetTokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task<PasswordResetToken?> GetPasswordResetTokenByHash(string tokenHash)
        {
            return await _context.PasswordResetTokens.FirstOrDefaultAsync(t => t.TokenHash == tokenHash);
        }

        public async Task InvalidatePasswordResetToken(long tokenId, Guid consumedBy)
        {
            var t = await _context.PasswordResetTokens.FindAsync(tokenId);
            if (t == null) return;
            t.ConsumedAt = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task InvalidateAllUserResetTokens(Guid userId)
        {
            var list = await _context.PasswordResetTokens.Where(x => x.UserId == userId && x.ConsumedAt == null).ToListAsync();
            foreach (var t in list) t.ConsumedAt = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserPassword(Guid userId, string newPassword)
        {
            var u = await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);
            if (u == null) throw new AppException("User not found");
            var salt = _cryptographyService.GenerateSalt();
            var hash = _cryptographyService.HashPassword(newPassword, salt);

            u.Password = hash;
            u.Salt = salt;
            u.UpdateDate = DateTime.Now;
            u.LockExpiration = null;

            await _context.SaveChangesAsync();
        }

        public async Task BuildAndSendResetPasswordEmail(PasswordResetToken token, User user, string rawToken)
        {
            var baseUrl = _cfg["ResetUrl"];
            var tokenEncoded = Uri.EscapeDataString(rawToken);
            var resetUrl = $"{baseUrl}?token={tokenEncoded}";
            var body = $@"
            <p>Përshëndetje {user.Name ?? ""},</p>
            <p>Eshtë bërë një kërkesë për të vendosur një fjalëkalim të ri. Kliko tek linku për të vendosur fjalëkalimin e ri. Linku është i aksesueshëm për 15 minuta.</p>
            <p><a href=""{resetUrl}"">Krijo fjalëkalim të ri</a></p>
            <p>Nëse nuk e keni kërkuar Ju krijimin e fjalëkalimit të ri, mos e konsideroni këtë email.</p>";

            await _email.SendEmailAsync(user.Email, "Vendosja e fjalëkalimit", body);
        }

    }
}