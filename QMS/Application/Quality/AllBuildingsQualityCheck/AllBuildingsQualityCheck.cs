
using Application.Exceptions;
using Application.FieldWork.ExecuteJob.Response;
using Application.Ports;
using Application.Quality.AllBuildingsQualityCheck.Request;
using Application.Quality.AllBuildingsQualityCheck.Response;
using Application.Quality.RulesExecutor;
using Hangfire;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Application.Quality.AllBuildingsQualityCheck
{
    public class AllBuildingsQualityCheck : IAllBuildingsQualityCheck
    {
        private readonly ILogger _logger;
        private readonly RulesExecutor.RulesExecutor _rulesExecutor;
        public AllBuildingsQualityCheck (ILogger<AllBuildingsQualityCheck> logger,
                                    RulesExecutor.RulesExecutor rulesExecutor)
        {
            _logger = logger;
            _rulesExecutor = rulesExecutor;
        }
        public async Task<AllBuildingsQualityCheckResponse> Execute(AllBuildingsQualityCheckRequest request)
        {
            try
            {
                var emptyBldIds = new List<Guid>();
                BackgroundJob.Enqueue(() =>
                _rulesExecutor.ExecuteRules(emptyBldIds, request.ExecutionUser));

                return new AllBuildingsQualityCheckSuccessResponse
                {
                    Message = "Rules are being executed on all buildings"
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
