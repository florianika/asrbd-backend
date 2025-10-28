using Application.Exceptions;
using Application.Ports;
using Application.User.ForgetPassword.Request;
using Application.User.ForgetPassword.Response;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace Application.User.ForgetPassword
{
    public class ForgetPassword : IForgetPassword
    {
        private readonly ILogger _logger;
        private readonly IAuthRepository _authRepository;
        public ForgetPassword(ILogger<ForgetPassword> logger,
            IAuthRepository authRepository)
        {
            _logger = logger;
            _authRepository = authRepository;
        }

        public async Task<ForgetPasswordResponse> Execute(ForgetPasswordRequest request)
        {
            var email = request.Email.Trim().ToLowerInvariant();
            Domain.User? user = null;

            try
            {
                user = await _authRepository.GetUserByEmail(email);
            }
            catch (NotFoundException)
            {
                // Do nothing intentionally — we don’t reveal that user doesn’t exist
            }

            if (user != null)
            {
                // generate secure random token (raw) and hashed version to store
                var rawToken = GenerateSecureToken(48); // base64url ~64 chars
                var tokenHash = ComputeSha256(rawToken);

                var ttlMinutes = 15; // token time to live in minutes
                var token = new Domain.PasswordResetToken
                {
                    UserId = user.Id,
                    TokenHash = tokenHash,
                    ExpiresAt = DateTime.Now.AddMinutes(ttlMinutes)
                };

                await _authRepository.SavePasswordResetToken(token);

                try
                {
                    await _authRepository.BuildAndSendResetPasswordEmail(token, user, rawToken);
                }
                catch (Exception ex)
                {
                    // You can log the exception for monitoring, but don’t return error to the user
                    _logger.LogError(ex, "Failed to send password reset email for {Email}", email);
                }
            }

            // Always return generic response (whether or not user exists)
            return new ForgetPasswordSuccessResponse
            {
                Message = "If the email exists in our system, a password reset link has been sent."
            };
        }

        private static string GenerateSecureToken(int byteLength)
        {
            var bytes = RandomNumberGenerator.GetBytes(byteLength);
            // base64url: replace +/ with -_ and trim =
            return Convert.ToBase64String(bytes).TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }
        private static string ComputeSha256(string raw)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(raw);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
