using Application.Common.Interfaces;
using Application.Configuration;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Configurations
{
    public class UserLockSettingsService : IUserLockSettings
    {
        private readonly IConfiguration _configuration;

        public UserLockSettingsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public UserLockSettings UserLockSettings =>
            _configuration.GetSection("userLockSettings").Get<UserLockSettings>()
            ?? new UserLockSettings(); // Provide a default instance to avoid null reference
    }
}
