using Application.Rule.CreateRule;
using Application.Rule.CreateRule.Request;
using Application.Rule.CreateRule.Response;
using Application.Rule.GetAllRules;
using Application.Rule.GetAllRules.Response;
using Application.Rule.GetRule;
using Application.Rule.GetRule.Request;
using Application.Rule.GetRule.Response;
using Application.Rule.GetRulesByEntity;
using Application.Rule.GetRulesByEntity.Request;
using Application.Rule.GetRulesByEntity.Response;
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
        private readonly GetRulesByEntity _getRulesByEntityService;
        private readonly GetRule _getRuleService;
        public RuleController(CreateRule createRuleService, GetAllRules getAllRulesService,
             GetRulesByVarableAndEntity getRulesByVariableAndEntityService, GetRulesByEntity getRulesByEntity, 
             GetRule getRuleService)
        {
            _createRuleService = createRuleService;
            _getAllRulesService = getAllRulesService;
            _getRulesByVariableAndEntityService = getRulesByVariableAndEntityService;
            _getRulesByEntityService = getRulesByEntity;
            _getRuleService = getRuleService;
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

        [HttpGet]
        [Route("entity/{entityType}")]
        public async Task<GetRulesByEntityResponse> GetRulesByEntity(EntityType entityType)
        {
            return await _getRulesByEntityService.Execute(new GetRulesByEntityRequest() { EntityType = entityType });
        }
        [HttpGet]
        [Route("/{id}")]
        public async Task<GetRuleResponse> GetRule(long id)
        {
            return await _getRuleService.Execute(new GetRuleRequest() { Id = id });
        }
    }
}
