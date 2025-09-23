using Application.Exceptions;
using Application.Quality.AutomaticRules.Response;
using Application.Quality.SetBldToUntested.Request;
using Application.Quality.SetBldToUntested.Response;
using Microsoft.Extensions.Logging;

namespace Application.Quality.SetBldToUntested
{
    public class SetBldToUntested : ISetBldToUntested
    {
        private readonly ILogger _logger;
        private readonly RulesExecutor.RulesExecutor _rulesExecutor;
        public SetBldToUntested(ILogger<SetBldToUntested> logger, RulesExecutor.RulesExecutor rulesExecutor)
        {
            _logger = logger;
            _rulesExecutor = rulesExecutor;
        }
        public async Task<SetBldToUntestedResponse> Execute(SetBldToUntestedRequest request)
        {
            try
            {
                var success = await _rulesExecutor.SetBldToUntested(request.BuildingIds);
                if (success)
                {
                    return new SetBldToUntestedSuccessResponse { Message = "Quality status of the buildings was set to untested" };
                }
                return new SetBldToUntestedErrorResponse { Message = "There was an error", Code = "EXECUTION_FAILED" };
            }
            catch (AppException appEx)
            {
                throw new AppException(appEx.Message);
            }
        }
    }
}
