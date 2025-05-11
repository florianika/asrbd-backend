using Application.FieldWorkRule.AddFieldWorkRule;
using Application.FieldWorkRule.AddFieldWorkRule.Request;
using Application.FieldWorkRule.AddFieldWorkRule.Response;
using Application.FieldWorkRule.GetFieldWorkRule;
using Application.FieldWorkRule.GetFieldWorkRule.Request;
using Application.FieldWorkRule.GetFieldWorkRule.Response;
using Application.FieldWorkRule.GetRuleByFieldWork;
using Application.FieldWorkRule.GetRuleByFieldWork.Request;
using Application.FieldWorkRule.GetRuleByFieldWork.Response;
using Application.FieldWorkRule.RemoveFieldWorkRule;
using Application.FieldWorkRule.RemoveFieldWorkRule.Request;
using Application.FieldWorkRule.RemoveFieldWorkRule.Response;
using Application.Ports;
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId.Request;
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/qms/fieldworkrule")]
    public class FieldWorkRuleController : ControllerBase
    {
        private readonly IRemoveFieldWorkRule _removeFieldWorkRule;
        private readonly IAddFieldWorkRule _addFieldWorkRuleService;
        private readonly IGetFieldWorkRule _getFieldWorkRuleService;
        private readonly IGetRuleByFieldWork _getRuleByFieldWorkService;
        private readonly IAuthTokenService _authTokenService;
        public FieldWorkRuleController( IGetRuleByFieldWork getRuleByFieldWork,IGetFieldWorkRule getFieldWorkRule, IRemoveFieldWorkRule removeFieldWorkRule, IAddFieldWorkRule addFieldWorkRuleService, IAuthTokenService authTokenService)
        {
            _getRuleByFieldWorkService = getRuleByFieldWork;
            _getFieldWorkRuleService = getFieldWorkRule;
            _removeFieldWorkRule = removeFieldWorkRule;
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

        [HttpDelete("{id:long}")]
        public async Task<RemoveFieldWorkRuleResponse> RemoveFieldWorkRule(long id)
        {
            return await _removeFieldWorkRule.Execute(new RemoveFieldWorkRuleRequest() { Id = id });
        }

        [HttpGet]
        [Route("{id:long}")]
        public async Task<GetFieldWorkRuleResponse> GetFieldWorkRule(int id)
        {
            return await _getFieldWorkRuleService.Execute(new GetFieldWorkRuleRequest() { Id = id });
        }
        [HttpGet]
        [Route("fieldwork/{id:int}")]
        public async Task<GetRuleByFieldWorkResponse> GetRuleByFieldWork(int id)
        {
            return await _getRuleByFieldWorkService.Execute(new GetRuleByFieldWorkRequest() { Id = id });
        }
    }
}
