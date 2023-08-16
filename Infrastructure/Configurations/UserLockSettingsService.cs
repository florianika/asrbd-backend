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

        //public UserLockSettings UserLockSettings => throw new NotImplementedException();

        //public UserLockSettings GetUserLockSettings()
        //{
        //    var userLockSettings = new UserLockSettings();
        //    _configuration.GetSection("userLockSettings").Bind(userLockSettings);
        //    return userLockSettings;
        //}
        public UserLockSettings UserLockSettings =>
            _configuration.GetSection("userLockSettings").Get<UserLockSettings>();
    }
}
