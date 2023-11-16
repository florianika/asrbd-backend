using Application.Rule.CreateRule;
using Application.Rule.CreateRule.Request;
using Application.Rule.CreateRule.Response;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/qms/rules")]
    public class RuleController : ControllerBase
    {
        private readonly CreateRule _createRuleService;
        public RuleController(CreateRule createRuleService)
        {
            _createRuleService = createRuleService; 
        }
        [HttpPost]
        [Route("")]
        public async Task<CreateRuleResponse> CreateRule(CreateRuleRequest request)
        {
            return await _createRuleService.Execute(request);
        }

    }
}
