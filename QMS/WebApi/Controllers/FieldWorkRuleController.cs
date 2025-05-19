using Application.FieldWorkRule.AddFieldWorkRule;
using Application.FieldWorkRule.AddFieldWorkRule.Request;
using Application.FieldWorkRule.AddFieldWorkRule.Response;
using Application.FieldWorkRule.GetFieldWorkRule;
using Application.FieldWorkRule.GetFieldWorkRule.Request;
using Application.FieldWorkRule.GetFieldWorkRule.Response;
using Application.FieldWorkRule.GetRuleByFieldWork;
using Application.FieldWorkRule.GetRuleByFieldWork.Request;
using Application.FieldWorkRule.GetRuleByFieldWork.Response;
using Application.FieldWorkRule.GetStatisticsByRule;
using Application.FieldWorkRule.GetStatisticsByRule.Request;
using Application.FieldWorkRule.GetStatisticsByRule.Response;
using Application.FieldWorkRule.RemoveFieldWorkRule;
using Application.FieldWorkRule.RemoveFieldWorkRule.Request;
using Application.FieldWorkRule.RemoveFieldWorkRule.Response;
using Application.Ports;
using Application.Queries.GetStatisticsFromBuilding;
using Application.Queries.GetStatisticsFromBuilding.Response;
using Application.Queries.GetStatisticsFromRules;
using Application.Queries.GetStatisticsFromRules.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/qms/fieldworkrule")]
    //TODO remove this controller and use the FieldWorkerController instead
    
    public class FieldWorkRuleController : QmsControllerBase
    {
        private readonly IRemoveFieldWorkRule _removeFieldWorkRule;
        private readonly IAddFieldWorkRule _addFieldWorkRuleService;
        private readonly IGetFieldWorkRule _getFieldWorkRuleService;
        private readonly IGetRuleByFieldWork _getRuleByFieldWorkService;
        private readonly IGetStatisticsFromRulesQuery _getStatisticsFromRuleQueryService;
        private readonly IGetStatisticsFromBuildingQuery _getStatisticsFromBuildingQueryService;
        private readonly IGetStatisticsByRule _getStatisticsByRuleService;
        private readonly IAuthTokenService _authTokenService;
        public FieldWorkRuleController(IGetStatisticsByRule getStatisticsByRuleService, IGetStatisticsFromBuildingQuery getStatisticsFromBuildingQuery, IGetStatisticsFromRulesQuery getStatisticsFromRulesQuery, IGetRuleByFieldWork getRuleByFieldWork,IGetFieldWorkRule getFieldWorkRule, IRemoveFieldWorkRule removeFieldWorkRule, IAddFieldWorkRule addFieldWorkRuleService, IAuthTokenService authTokenService)
        {
            _getRuleByFieldWorkService = getRuleByFieldWork;
            _getFieldWorkRuleService = getFieldWorkRule;
            _removeFieldWorkRule = removeFieldWorkRule;
            _addFieldWorkRuleService = addFieldWorkRuleService;
            _getStatisticsFromRuleQueryService = getStatisticsFromRulesQuery;
            _getStatisticsFromBuildingQueryService = getStatisticsFromBuildingQuery;
            _getStatisticsByRuleService = getStatisticsByRuleService;
            _authTokenService = authTokenService;
        }

        [HttpPost]
        [Route("")]
        //TODO this should be in teh FieldWorkerController
        //TODO route '/fieldwork/{id:int}/rules'
        public async Task<AddFieldWorkRuleResponse> AddFieldWorkRule(AddFieldWorkRuleRequest request)
        {
            var token = ExtractBearerToken();
            request.CreatedUser = await _authTokenService.GetUserIdFromToken(token);
            return await _addFieldWorkRuleService.Execute(request);
        }

        [HttpDelete("{id:long}")]
        //TODO this should be in teh FieldWorkerController
        //TODO route '/fieldwork/{id:int}/rules/{ruleId:long}'
        public async Task<RemoveFieldWorkRuleResponse> RemoveFieldWorkRule(long id)
        {
            return await _removeFieldWorkRule.Execute(new RemoveFieldWorkRuleRequest() { Id = id });
        }

        [HttpGet]
        [Route("{id:long}")]
        //TODO this should be in teh FieldWorkerController
        //TODO route '/fieldwork/{id:int}/rule/{ruleId:long}'
        public async Task<GetFieldWorkRuleResponse> GetFieldWorkRule(int id)
        {
            return await _getFieldWorkRuleService.Execute(new GetFieldWorkRuleRequest() { Id = id });
        }
        [HttpGet]
        [Route("fieldwork/{id:int}")]
        //TODO this should be in teh FieldWorkerController
        //TODO route '/fieldwork/{id:int}/rules'
        public async Task<GetRuleByFieldWorkResponse> GetRuleByFieldWork(int id)
        {
            return await _getRuleByFieldWorkService.Execute(new GetRuleByFieldWorkRequest() { Id = id });
        }
        [HttpGet]
        [Route("statistics/rule")]
        //TODO this should be in teh FieldWorkerController
        //TODO route '/fieldwork/{id:int}/rules/statistics'
        //TODO try to avoid methods with empty requests
        public async Task<GetStatisticsFromRulesResponse> GetStatisticsFromRules()
        {
            return await _getStatisticsFromRuleQueryService.Execute();
        }
        [HttpGet]
        [Route("statistics/building")]
        //TODO this should be in teh FieldWorkerController
        //TODO route '/fieldwork/{id:int}/building/statistics' or thing of better route 
        public async Task<GetStatisticsFromBuildingResponse> GetStatisticsFromBuilding()
        {
            return await _getStatisticsFromBuildingQueryService.Execute();
        }

        [HttpGet]
        [Route("statistics/rule/{id:long}")]
        //TODO this should be in teh FieldWorkerController
        //TODO route '/fieldwork/{id:int}/rules/statistics/{ruleId:long}' or thing of better route
        public async Task<GetStatisticsByRuleResponse> GetStatisticsByRule(long id)
        {
            return await _getStatisticsByRuleService.Execute(new GetStatisticsByRuleRequest { Id = id});
        }
    }
}
