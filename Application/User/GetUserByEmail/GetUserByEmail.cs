using Application.Common.Translators;
using Application.Ports;
using Application.User.GetUserByEmail.Request;
using Application.User.GetUserByEmail.Response;
using Microsoft.Extensions.Logging;

namespace Application.User.GetUserByEmail;

public class GetUserByEmail : IGetUserByEmail
{
    private readonly ILogger _logger;
    private readonly IAuthRepository _authRepository;

    public GetUserByEmail(ILogger<GetUserByEmail> logger, IAuthRepository authRepository)
    {
        _logger = logger;
        _authRepository = authRepository;
    }
    public async Task<GetUserByEmailResponse> Execute(GetUserByEmailRequest request)
    {
        try
        {
            var user = await _authRepository.GetUserByEmail(request.Email);
            return new GetUserByEmailSuccessResponse
            {
                UserDto = Translator.ToDTO(user)
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
        
    }
}