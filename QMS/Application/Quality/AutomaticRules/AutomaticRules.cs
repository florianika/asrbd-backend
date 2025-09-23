
using Application.Exceptions;
using Application.Quality.AutomaticRules.Request;
using Application.Quality.AutomaticRules.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Quality.AutomaticRules
{
    public class AutomaticRules : IAutomaticRules
    {
        private readonly ILogger _logger;
        private readonly RulesExecutor.RulesExecutor _rulesExecutor;
        private readonly IConfiguration _configuration;
        public AutomaticRules(ILogger<AutomaticRules> logger,
                                    RulesExecutor.RulesExecutor rulesExecutor,
                                    IConfiguration configuration)
        {
            _logger = logger;
            _rulesExecutor = rulesExecutor;
            _configuration = configuration;
        }
        public async Task<AutomaticRulesResponse> Execute(AutomaticRulesRequest request)
        {
            try
            {
                var success = await _rulesExecutor.ExecuteAutomaticRules(request.BuildingIds, request.ExecutionUser);
                if (success)
                {
                    return new AutomaticRulesSuccessResponse { Message = "Automatic Rules were executed" };
                }
                return new AutomaticRulesErrorResponse { Message = "There was an error", Code = "EXECUTION_FAILED" };
            }
            catch (AppException appEx)
            {
                throw new AppException(appEx.Message);
            }            
        }

    }
}
