using Application.Exceptions;
using Application.Quality.AllBuildingsAutomaticRules.Request;
using Application.Quality.AllBuildingsAutomaticRules.Response;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace Application.Quality.AllBuildingsAutomaticRules
{
    public class AllBuildingsAutomaticRules : IAllBuildingsAutomaticRules
    {
        private readonly ILogger _logger;
        private readonly RulesExecutor.RulesExecutor _rulesExecutor;
        public AllBuildingsAutomaticRules(ILogger<AllBuildingsAutomaticRules> logger,
                                          RulesExecutor.RulesExecutor rulesExecutor)
        {
            _logger = logger;
            _rulesExecutor = rulesExecutor;
        }
        public async Task<AllBuildingsAutomaticRulesResponse> Execute(AllBuildingsAutomaticRulesRequest request)
        {
            try
            {
                var emptyBldIds = new List<Guid>();
                BackgroundJob.Enqueue(() =>
                _rulesExecutor.ExecuteAutomaticRules(emptyBldIds, request.ExecutionUser));
                return new AllBuildingsAutomaticRulesSuccessResponse
                {
                    Message = "Automatic rules are being executed on all buildings"
                };
            }
            catch (AppException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
