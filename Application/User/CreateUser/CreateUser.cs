using Application.Enums;
using Application.Ports;
using Application.User.CreateUser.Request;
using Application.User.CreateUser.Response;
using Domain.RefreshToken;
using Microsoft.Extensions.Logging;
using d=Domain;

namespace Application.User.CreateUser
{
    public class CreateUser : ICreateUser
    {
        private readonly ILogger _logger;
        private readonly ICryptographyService _cryptographyService;
        private readonly IAuthRepository _authRepository;
        public CreateUser(ILogger<CreateUser> logger, ICryptographyService cryptographyService, IAuthRepository authRepository)
        {
            _logger = logger;
            _cryptographyService = cryptographyService;
            _authRepository = authRepository;
        }

        public async Task<CreateUserResponse> Execute(CreateUserRequest request)
        {
            try
            {
                var salt = _cryptographyService.GenerateSalt();
                var currentDate = DateTime.Now;
                var refreshToken = new Domain.RefreshToken.RefreshToken
                {
                    Value = "Empty",
                    Active = false,
                    ExpirationDate = currentDate,
                };

                Guid userId = Guid.NewGuid();

                var user = new d.User.User
                {
                    Id = userId,
                    Active = true,
                    Email = request.Email,
                    Password = _cryptographyService.HashPassword(request.Password, salt),
                    Salt = salt,
                    Name = request.Name,
                    LastName = request.LastName,
                    Claims = ToClaims(userId, request.Claims),
                    CreationDate = currentDate,
                    UpdateDate = currentDate,
                    RefreshToken = refreshToken
                };

                 await _authRepository.CreateUser(user);

                //foreach (var claim in user.Claims)
                //{
                //    await _authRepository.AddClaim(user.Id, claim);
                //}
                //await _authRepository.AddRefreshToken(user.Id, refreshToken);

                return new CreateUserSuccessResponse
                {
                    UserId = user.Id
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                var response = new CreateUserErrorResponse
                {
                    Message = Enum.GetName(ErrorCodes.AnUnexpectedErrorOcurred),
                    Code = ErrorCodes.AnUnexpectedErrorOcurred.ToString("D")
                };

                return response;
            }
        }

        private static IList<d.Claim.Claim> ToClaims(Guid userId, IList<Request.Claim> requestClaims)
        {
            if (requestClaims == null) return null;
            var claims = new List<d.Claim.Claim>();
            claims.AddRange(requestClaims.Select(r => new d.Claim.Claim { UserId=userId, Type = r.Type, Value = r.Value }).ToList());
            return claims;
        }
    }
}
