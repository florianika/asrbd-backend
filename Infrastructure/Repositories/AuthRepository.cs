using Application.Common.Interfaces;
using Application.Ports;
using Application.User.GetAllUsers;
using Application.User.Login;
using Domain.Claim;
using Domain.Enum;
using Domain.RefreshToken;
using Domain.User;
using FluentValidation;
using Infrastructure.Configurations;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;

namespace Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IUserLockSettings _userLockSettings;

        public AuthRepository(DataContext context, IUserLockSettings userLockSettings)
        {
            _context = context;
            _userLockSettings = userLockSettings;
        }
        public async Task CreateUser(User user)
        {
            var validator = new UserValidator();
            var validationResult = validator.Validate(user);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task<User> GetUserByEmail(string email)
        {
            var resut = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);            
            resut.RefreshToken = await _context.RefreshToken.SingleOrDefaultAsync(rt => rt.UserId == resut.Id);
            resut.Claims = await _context.Claim.Where(c=>c.UserId==resut.Id).ToListAsync();
            return resut;
        }
        public async Task<User> GetUserByUserId(Guid userId)
        {
            var resut = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
            resut.RefreshToken = await _context.RefreshToken.SingleOrDefaultAsync(rt => rt.UserId == resut.Id);
            resut.Claims = await _context.Claim.Where(c => c.UserId == resut.Id).ToListAsync();
            return resut;
        }
        public async Task UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
        public async Task UpdateRefreshToken(Guid userId, RefreshToken refreshToken) 
        {
            var user = await _context.Users.Include(u=>u.RefreshToken).SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                //handle the user not found error
                return;
            }
            user.RefreshToken = refreshToken;
            await _context.SaveChangesAsync();
        }
        public async Task AddClaim(Guid userId, Claim claim)
        { 
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null) {
                //Error
                return;
            }
            user.Claims.Add(claim);
            await _context.SaveChangesAsync();
        }
        public async Task AddRefreshToken(Guid userId, RefreshToken refreshToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                //Error
                return;
            }
            user.RefreshToken=refreshToken;
            await _context.SaveChangesAsync();
        }
        public async Task LockAccount(User user)
        {
            user.SigninFails++;
            if(user.SigninFails == _userLockSettings.UserLockSettings.MaxSigninFails)//TODO from configuration
            {
                user.AccountStatus = AccountStatus.LOCKED;
                user.LockExpiration = DateTime.Now.AddHours(_userLockSettings.UserLockSettings.LockExpirationTime);//from configuration
                
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
            return await _context.Users.ToListAsync();
        }
        public async Task UpdateUserRole(Guid userId, AccountRole accountRole)
        {
            var userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            userToUpdate.AccountRole = accountRole;
            await _context.SaveChangesAsync();
        }
        public async Task<bool> CheckIfUserExists(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user != null)
                return true;            
            else
                return false;
        }

        public async Task UpdateAccountUser(Guid userId, AccountStatus accountStatus)
        {
            var userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            userToUpdate.AccountStatus = accountStatus;
            _context.SaveChanges();
        }

        public async Task<User> FindUserById(Guid userId)
        { 
            return await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(userId));
        }

        public async Task<User> FindUserByEmail(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}