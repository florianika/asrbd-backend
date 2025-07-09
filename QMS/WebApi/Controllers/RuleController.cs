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
using Application.Rule.GetRulesByQualityAction.Request;
using Application.Rule.GetRulesByVariableAndEntity;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using Application.Rule.GetRulesByVariableAndEntity.Response;
using Application.Rule.GetRulesByVariableAndEntity.Request;
using Application.Rule.GetRulesByQualityAction;
using Application.Rule.GetRulesByQualityAction.Response;
using Application.Rule.ChangeRuleStatus;
using Application.Rule.ChangeRuleStatus.Response;
using Application.Rule.ChangeRuleStatus.Request;
using Microsoft.AspNetCore.Authorization;
using Application.Rule.UpdateRule.Response;
using Application.Rule.UpdateRule.Request;
using Application.Rule.UpdateRule;
using Application.Ports;
using Application.Rule.GetActiveRules;
using Application.Rule.GetActiveRules.Response;
using Application.Rule.GetRulesByEntityAndStatus.Request;
using Application.Rule.GetRulesByEntityAndStatus;
using Application.Rule.GetRulesByEntityAndStatus.Response;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/qms/rules")]
    public class RuleController : QmsControllerBase
    {
        private readonly CreateRule _createRuleService;
        private readonly GetAllRules _getAllRulesService;
        private readonly GetActiveRules _getActiveRulesService;
        private readonly GetRulesByVariableAndEntity _getRulesByVariableAndEntityService;
        private readonly GetRulesByEntity _getRulesByEntityService;
        private readonly GetRule _getRuleService;
        private readonly GetRulesByQualityAction _getRulesByQualityActionService;
        private readonly ChangeRuleStatus _changeRuleStatusService;
        private readonly UpdateRule _updateRuleService;
        private readonly IAuthTokenService _authTokenService;
        private readonly IGetRulesByEntityAndStatus _getRulesByEntityAndStatusService;
        public RuleController(CreateRule createRuleService, GetAllRules getAllRulesService, GetActiveRules getActiveRulesService,
             GetRulesByVariableAndEntity getRulesByVariableAndEntityService, GetRulesByEntity getRulesByEntity, 
             GetRule getRuleService, GetRulesByQualityAction getRulesByQualityActionService,
             ChangeRuleStatus changeRuleStatusService, UpdateRule updateRuleService, IAuthTokenService authTokenService, IGetRulesByEntityAndStatus getRulesByEntityAndStatusService)
        {
            _createRuleService = createRuleService;
            _getAllRulesService = getAllRulesService;
            _getActiveRulesService = getActiveRulesService;
            _getRulesByVariableAndEntityService = getRulesByVariableAndEntityService;
            _getRulesByEntityService = getRulesByEntity;
            _getRuleService = getRuleService;
            _getRulesByQualityActionService = getRulesByQualityActionService;
            _changeRuleStatusService = changeRuleStatusService;
            _updateRuleService = updateRuleService;
            _authTokenService = authTokenService;   
            _getRulesByEntityAndStatusService = getRulesByEntityAndStatusService;

        }
        [HttpPost]
        [Route("")]
        public async Task<CreateRuleResponse> CreateRule(CreateRuleRequest request)
        {
            var token = ExtractBearerToken();
            request.CreatedUser = await _authTokenService.GetUserIdFromToken(token);
            return await _createRuleService.Execute(request);
        }

        [HttpGet]
        [Route("")]
        public async Task<GetAllRulesResponse> GetAllRules()
        {
            return await _getAllRulesService.Execute();
        }
        [HttpGet]
        [Route("active")]
        public async Task<GetActiveRulesResponse> GetActiveRules()
        {
            return await _getActiveRulesService.Execute();
        }

        [HttpGet]
        [Route("variable/{variable}/type/{entityType}")]
        public async Task<GetRulesByVariableAndEntityResponse> GetRulesByVariableAndEntityType(
            string variable, EntityType entityType)
        {
            return await _getRulesByVariableAndEntityService.Execute(
                new GetRulesByVariableAndEntityRequest() { Variable = variable, EntityType = entityType }
                );
        }

        [HttpGet]
        [Route("entity/{entityType}")]
        public async Task<GetRulesByEntityResponse> GetRulesByEntity(EntityType entityType)
        {
            return await _getRulesByEntityService.Execute(new GetRulesByEntityRequest() { EntityType = entityType });
        }

        [HttpGet]
        [Route("entity/{entityType}/status/{ruleStatus}")]
        public async Task<GetRulesByEntityAndStatusResponse> GetRulesByEntityAndStatus(EntityType entityType, RuleStatus ruleStatus)
        {
            return await _getRulesByEntityAndStatusService.Execute(new GetRulesByEntityAndStatusRequest() { EntityType = entityType, RuleStatus = ruleStatus });
        }

        [HttpGet]
        [Route("{id:long}")]
        public async Task<GetRuleResponse> GetRule(long id)
        {
            return await _getRuleService.Execute(new GetRuleRequest() { Id = id });
        }
        
        [HttpGet]
        [Route("qualityAction/{qualityAction}")]
        public async Task<GetRulesByQualityActionResponse> GetRulesByQualityAction(QualityAction qualityAction)
        {
            return await _getRulesByQualityActionService.Execute(new GetRulesByQualityActionRequest() { QualityAction = qualityAction });
        }

        [HttpPatch]
        [Route("{id:long}")]
        public async Task<ChangeRuleStatusResponse> ChangeRuleStatus(long id)
        {
            var token = ExtractBearerToken();
            var updatedUser = await _authTokenService.GetUserIdFromToken(token);
            return await _changeRuleStatusService.Execute(new ChangeRuleStatusRequest() { Id = id, UpdatedUser = updatedUser});
        }

        [HttpPut("{id:long}")]
        public async Task<UpdateRuleResponse> UpdateRule(long id, UpdateRuleRequest request)
        {
            request.Id = id;
            var token = ExtractBearerToken();
            var updatedUser = await _authTokenService.GetUserIdFromToken(token);
            request.UpdatedUser = updatedUser;
            return await _updateRuleService.Execute(request);
        }
    }
}
