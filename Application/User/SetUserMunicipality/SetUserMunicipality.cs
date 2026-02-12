using Application.Exceptions;
using Application.Ports;
using Application.User.SetUserMunicipality.Request;
using Application.User.SetUserMunicipality.Response;
using Domain.Enum;
using Microsoft.Extensions.Logging;

namespace Application.User.SetUserMunicipality;

public class SetUserMunicipality : ISetUserMunicipality
{
    private readonly ILogger _logger;
    private readonly IAuthRepository _authRepository;
    
    public SetUserMunicipality (ILogger<SetUserMunicipality> logger,
        IAuthRepository authRepository)
    {
        _logger = logger;
        _authRepository = authRepository;
    }

    public async Task<SetUserMunicipalityResponse> Execute(SetUserMunicipalityRequest request)
    {
        try
        {
            if (request.UserId == request.RequestUserId)
                throw new ForbidenException("Cannot set municipality for yourself.");
            await _authRepository.SetUserMunicipality(request.UserId, request.MunicipalityCode,
                    (AccountRole)request.RequestUserRole!);
            return new SetUserMunicipalitySuccessResponse
            {
                Message = "User municipality set."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
    
}