using Application.Exceptions;
using Application.Ports;
using Application.User.ResetPassword.Request;
using Application.User.ResetPassword.Response;
using Microsoft.Extensions.Logging;

namespace Application.User.ResetPassword
{
    public class ResetPassword : IResetPassword
    {
        private readonly ILogger _logger;
        private readonly IAuthRepository _authRepository;
        public ResetPassword(ILogger<ResetPassword> logger, IAuthRepository authRepository)
        {
            _logger = logger;
            _authRepository = authRepository;
        }

        public async Task<ResetPasswordResponse> Execute(ResetPasswordRequest request)
        {
            var tokenHash = ComputeSha256(request.Token);
            var tokenRec = await _authRepository.GetPasswordResetTokenByHash(tokenHash);
            if (tokenRec == null)
                throw new NotFoundException("Invalid or expired token.");

            if (tokenRec.ConsumedAt != null)
                throw new NotFoundException("Token already used." );

            if (tokenRec.ExpiresAt < DateTime.Now)
                throw new NotFoundException("Token expired.");

            if (tokenRec.Attempts > 10)
                throw new NotFoundException("Too many attempts.");

           
            await _authRepository.UpdateUserPassword(tokenRec.UserId, request.NewPassword);

            await _authRepository.InvalidatePasswordResetToken(tokenRec.Id, tokenRec.UserId);
            await _authRepository.InvalidateAllUserResetTokens(tokenRec.UserId);

            return new ResetPasswordSuccessResponse { Message = "Password updated successfully." };

        }
        private static string ComputeSha256(string raw)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(raw);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
