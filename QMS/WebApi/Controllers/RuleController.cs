using Application.Rule.CreateRule;
using Application.Rule.CreateRule.Request;
using Application.Rule.CreateRule.Response;
using Application.Rule.GetAllRules;
using Application.Rule.GetAllRules.Response;
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
        public RuleController(CreateRule createRuleService, GetAllRules getAllRulesService)
        {
            _createRuleService = createRuleService;
            _getAllRulesService = getAllRulesService;

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

    }
}
