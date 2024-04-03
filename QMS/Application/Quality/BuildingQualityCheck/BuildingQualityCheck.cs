using Application.Dtos.Quality;
using Application.Ports;
using Application.Quality.BuildingQualityCheck.Request;
using Application.Quality.BuildingQualityCheck.Response;
using Application.Quality.RulesExecutor;
using Domain;
using Domain.Enum;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections;
using System.Linq.Expressions;

namespace Application.Quality.BuildingQualityCheck
{
    public class BuildingQualityCheck : IBuildingQualityCheck
    {
        private readonly ILogger _logger;
        private readonly Executor _executor;
        private readonly IConfiguration _configuration;
        public BuildingQualityCheck(ILogger<BuildingQualityCheck> logger, 
                                    Executor executor,
                                    IConfiguration configuration)
        {
            _logger = logger;
            _executor = executor;
            _configuration = configuration;
        }
        public async Task<BuildingQualityCheckResponse> Execute(BuildingQualityCheckRequest request, string action)
        {
            if (action == "ExecuteRules")
            {
                try
                {
                    var success = await _executor.ExecuteRules(request.BuildingIds, request.ExecutionUser);

                    if (success)
                    {
                        return new BuildingQualityCheckSuccessResponse { Message = "Rules were executed" };
                    }
                    else
                    {
                        return new BuildingQualityCheckErrorResponse { Message = "There was an error" };
                    }
                }
                catch (Exception ex)
                {
                    return new BuildingQualityCheckErrorResponse { Message = "There was an error", Code = ex.GetType().Name };
                }
            }
            else if (action == "UpdateStatus")
            {
                try
                {
                    var success = await _executor.UpdateStatus(request.BuildingIds, request.ExecutionUser);

                    if (success)
                    {
                        return new BuildingQualityCheckSuccessResponse { Message = "Quality Status updated" };
                    }
                    else
                    {
                        return new BuildingQualityCheckErrorResponse { Message = "There was an error" };
                    }
                }
                catch (Exception ex)
                {
                    return new BuildingQualityCheckErrorResponse { Message = "There was an error", Code = ex.GetType().Name };
                }
            }
            else
            return new BuildingQualityCheckErrorResponse { Message = "There was an error" };
        }
       
    }
}
