using Application.Ports;
using Application.User.SetUserMunicipality.Request;
using Application.User.SetUserMunicipality.Response;
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
        if (request == null) throw new ArgumentNullException(nameof(request));
        try
        {
            await _authRepository.SetUserMunicipality(request.UserId, request.MunicipalityCode);
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