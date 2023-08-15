using Application.Ports;
using Domain.Claim;
using Domain.RefreshToken;
using Domain.User;
using FluentValidation;
using Infrastructure.Configurations;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
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

    }
}
//IEnumerable<Domain.AccountRole.AccountRole>