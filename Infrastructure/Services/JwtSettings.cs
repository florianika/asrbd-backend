
namespace Infrastructure.Services
{
    public class JwtSettings
    {
        public AccessTokenSettings AccessTokenSettings { get; set; }
        public RefreshTokenSettings RefreshTokenSettings { get; set; }
    }
    public class AccessTokenSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public long LifeTimeInSeconds { get; set; }
        public string SecretKey { get; set; }
    }

    public class RefreshTokenSettings
    {
        public int Length { get; set; }
        public int LifeTimeInMinutes { get; set; }
    }

    public class UserLockSettings
    { 
        public int MaxSigninFails { get; set; }
        public int LockExpirationTime { get; set; }
    }
}
