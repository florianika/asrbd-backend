using Application.FieldWorkRule.AddFieldWorkRule;
using Application.FieldWorkRule.AddFieldWorkRule.Request;
using Application.FieldWorkRule.AddFieldWorkRule.Response;
using Application.Ports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/qms/fieldworkrule")]
    public class FieldWorkRuleController : ControllerBase
    {
        private readonly IAddFieldWorkRule _addFieldWorkRuleService;
        private readonly IAuthTokenService _authTokenService;
        public FieldWorkRuleController(IAddFieldWorkRule addFieldWorkRuleService, IAuthTokenService authTokenService)
        {
            _addFieldWorkRuleService = addFieldWorkRuleService;
            _authTokenService = authTokenService;
        }
        [HttpPost]
        [Route("")]
        public async Task<AddFieldWorkRuleResponse> AddFieldWorkRule(AddFieldWorkRuleRequest request)
        {
            var token = Request.Headers["Authorization"].ToString();
            token = token.Replace("Bearer ", "");
            request.CreatedUser = await _authTokenService.GetUserIdFromToken(token);
            return await _addFieldWorkRuleService.Execute(request);
        }
    }
}
