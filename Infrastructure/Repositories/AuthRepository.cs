using Application.Common.Interfaces;
using Application.Exceptions;
using Application.Ports;
using Domain;
using Domain.Enum;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IUserLockSettings _userLockSettings;
        private const string Municipality = "municipality";
        public AuthRepository(DataContext context, IUserLockSettings userLockSettings)
        {
            _context = context;
            _userLockSettings = userLockSettings;
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
        public async Task AddClaim(Guid userId, Claim claim)
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
        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users
                .Include(u=>u.Claims)
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
                userToUpdate.Claims.Add(new Claim(){Type = Municipality, Value = municipalityCode});
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
    }
}