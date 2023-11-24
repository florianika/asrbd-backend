
namespace Infrastructure.Services
{
    public class JwtSettings
    {
        public AccessTokenSettings AccessTokenSettings { get; set; } = new AccessTokenSettings();
        public RefreshTokenSettings RefreshTokenSettings { get; set; } = new RefreshTokenSettings();
    }
    public class AccessTokenSettings
    {
        public string Issuer { get; set; } = "issuer";
        public string Audience { get; set; } = "audience";
        public long LifeTimeInSeconds { get; set; } = 300;
        public string SecretKey { get; set; } = "IdentityApiKey";
    }

    public class RefreshTokenSettings
    {
        public int Length { get; set; } = 64;
        public int LifeTimeInMinutes { get; set; } = 60;
    }

    public class UserLockSettings
    { 
        public int MaxSigninFails { get; set; } = 3;
        public int LockExpirationTime { get; set; } = 3; //min
    }
}
