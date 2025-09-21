using Application.Exceptions;
using Application.Ports;
using Application.User.Login.Request;
using Application.User.Login.Response;
using Application.User.Login2fa.Request;
using Application.User.Login2fa.Response;
using Domain.Enum;
using Microsoft.Extensions.Logging;

namespace Application.User.Login2fa
{
    public class Login2fa : ILogin2fa
    {
        private readonly ILogger _logger;
        private readonly IAuthRepository _authRepository;
        private readonly ICryptographyService _cryptographyService;
        private readonly IAuthTokenService _authTokenService;
        private readonly IOtpRepository _otpRepository;

        public Login2fa(ILogger<Login2fa> logger,
            IAuthRepository authRepository,
            ICryptographyService cryptographyService, IAuthTokenService authTokenService, IOtpRepository otpRepository)
        {
            _logger = logger;
            _authRepository = authRepository;
            _cryptographyService = cryptographyService;
            _authTokenService = authTokenService;
            _otpRepository = otpRepository;
        }
        public async Task<Login2faResponse> Execute(Login2faRequest request)
        {
            var user = await _authRepository.FindUserByEmail(request.Email);
            if (user.AccountStatus == AccountStatus.LOCKED && user.LockExpiration > DateTime.Now)
            {
                throw new ForbidenException("Account locked");
            }

            ValidatePassword(request.Password, user);

            await _otpRepository.GenerateOtp(user.Id);

            var response = new Login2faSuccessResponse
            {
                UserId = user.Id,
                Message = "A verification code has been sent to your email."
            };
            return response;
        }

        private void ValidatePassword(string testPassword, Domain.User user)
        {
            var hash = _cryptographyService.HashPassword(testPassword, user.Salt);
            if (hash != user.Password)
            {
                throw new ForbidenException("Email or password wrong");
            }
        }
    }
}
