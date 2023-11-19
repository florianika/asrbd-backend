using Application.Rule.CreateRule;
using Application.Rule.CreateRule.Request;
using Application.Rule.CreateRule.Response;
using Application.Rule.GetAllRules;
using Application.Rule.GetAllRules.Response;
using Application.Rule.GetRulesByVarableAndEntity;
using Application.Rule.GetRulesByVarableAndEntity.Request;
using Application.Rule.GetRulesByVarableAndEntity.Response;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/qms/rules")]
    public class RuleController : ControllerBase
    {
        private readonly CreateRule _createRuleService;
        private readonly GetAllRules _getAllRulesService;
        private readonly GetRulesByVarableAndEntity _getRulesByVariableAndEntityService;
        public RuleController(CreateRule createRuleService, GetAllRules getAllRulesService,
             GetRulesByVarableAndEntity getRulesByVariableAndEntityService)
        {
            _createRuleService = createRuleService;
            _getAllRulesService = getAllRulesService;
            _getRulesByVariableAndEntityService = getRulesByVariableAndEntityService;
        }
        [HttpPost]
        [Route("")]
        public async Task<CreateRuleResponse> CreateRule(CreateRuleRequest request)
        {   
            return await _createRuleService.Execute(request);
        }
        [HttpGet]
        [Route("")]
        public async Task<GetAllRulesResponse> GetAllRules()
        {
            return await _getAllRulesService.Execute();
        }
        [HttpGet]
        [Route("variable/{variable}/type/{entityType}")]
        public async Task<GetRulesByVarableAndEntityResponse> GetRulesByVarialeAndEntityType(string variable, EntityType entityType)
        {
            return await _getRulesByVariableAndEntityService.Execute(new GetRulesByVarableAndEntityRequest() { Variable = variable, EntityType = entityType });
        }


    }
}
