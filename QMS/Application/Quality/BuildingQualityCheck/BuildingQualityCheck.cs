using Application.Quality.BuildingQualityCheck.Request;
using Application.Quality.BuildingQualityCheck.Response;
using Application.Quality.RulesExecutor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace Application.Quality.BuildingQualityCheck
{
    public class BuildingQualityCheck : IBuildingQualityCheck
    {
        private readonly ILogger _logger;
        private readonly RulesExecutor.RulesExecutor _rulesExecutor;
        private readonly IConfiguration _configuration;
        public BuildingQualityCheck(ILogger<BuildingQualityCheck> logger, 
                                    RulesExecutor.RulesExecutor rulesExecutor,
                                    IConfiguration configuration)
        {
            _logger = logger;
            _rulesExecutor = rulesExecutor;
            _configuration = configuration;
        }
        public async Task<BuildingQualityCheckResponse> Execute(BuildingQualityCheckRequest request)
        {
            try
            {
                var success = await _rulesExecutor.ExecuteRules(request.BuildingIds, request.ExecutionUser);
                if (success)
                {
                    return new BuildingQualityCheckSuccessResponse { Message = "Rules were executed" };
                }
                return new BuildingQualityCheckErrorResponse { Message = "There was an error" };
            }
            catch (Exception ex)
            {
                return new BuildingQualityCheckErrorResponse
                    { Message = "There was an error", Code = ex.GetType().Name };
            }
        }
    }
}
