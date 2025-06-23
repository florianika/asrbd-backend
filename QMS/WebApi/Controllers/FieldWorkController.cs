using Application.FieldWork.CreateFieldWork;
using Application.FieldWork.CreateFieldWork.Request;
using Application.FieldWork.CreateFieldWork.Response;
using Application.FieldWork.GetActiveFieldWork;
using Application.FieldWork.GetActiveFieldWork.Response;
using Application.FieldWork.GetAllFieldWork;
using Application.FieldWork.GetAllFieldWork.Response;
using Application.FieldWork.GetFieldWork;
using Application.FieldWork.GetFieldWork.Request;
using Application.FieldWork.GetFieldWork.Response;
using Application.FieldWork.OpenFieldWork;
using Application.FieldWork.SendFieldWorkEmail;
using Application.FieldWork.SendFieldWorkEmail.Request;
using Application.FieldWork.SendFieldWorkEmail.Response;
using Application.FieldWork.UpdateBldReviewStatus;
using Application.FieldWork.UpdateBldReviewStatus.Request;
using Application.FieldWork.UpdateBldReviewStatus.Response;
using Application.FieldWork.UpdateFieldWork;
using Application.FieldWork.UpdateFieldWork.Request;
using Application.FieldWork.UpdateFieldWork.Response;
using Application.FieldWorkRule.AddFieldWorkRule;
using Application.FieldWorkRule.AddFieldWorkRule.Request;
using Application.FieldWorkRule.AddFieldWorkRule.Response;
using Application.FieldWorkRule.GetFieldWorkRule;
using Application.FieldWorkRule.GetFieldWorkRule.Request;
using Application.FieldWorkRule.GetFieldWorkRule.Response;
using Application.FieldWorkRule.GetRuleByFieldWork;
using Application.FieldWorkRule.GetRuleByFieldWork.Request;
using Application.FieldWorkRule.GetRuleByFieldWork.Response;
using Application.FieldWorkRule.GetRuleByFieldWorkAndEntity;
using Application.FieldWorkRule.GetRuleByFieldWorkAndEntity.Request;
using Application.FieldWorkRule.GetRuleByFieldWorkAndEntity.Response;
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
using Application.Queries.GetStatisticsFromRules.Request;
using Application.Queries.GetStatisticsFromRules.Response;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/qms/fieldwork")]
    public class FieldWorkController : QmsControllerBase
    {
        private readonly GetAllFieldWork _getAllFieldWorkService;
        private readonly CreateFieldWork _createFieldWorkService;
        private readonly GetFieldWork _getFieldWorkService;
        private readonly UpdateFieldWork _updateFieldWorkService;
        private readonly GetActiveFieldWork _getActiveFieldWorkService;
        private readonly OpenFieldWork _openFieldWorkService;
        private readonly IAuthTokenService _authTokenService;

        private readonly IRemoveFieldWorkRule _removeFieldWorkRule;
        private readonly IAddFieldWorkRule _addFieldWorkRuleService;
        private readonly IGetFieldWorkRule _getFieldWorkRuleService;
        private readonly IGetRuleByFieldWork _getRuleByFieldWorkService;
        private readonly IGetRuleByFieldWorkAndEntity getRuleByFieldWorkAndEntityService;
        private readonly IGetStatisticsFromRulesQuery _getStatisticsFromRuleQueryService;
        private readonly IGetStatisticsFromBuildingQuery _getStatisticsFromBuildingQueryService;
        private readonly IGetStatisticsByRule _getStatisticsByRuleService;
        private readonly IUpdateBldReviewStatus _updateBldReviewStatus;
        private readonly ISendFieldWorkEmail _sendFieldWorkEmail;

        public FieldWorkController(GetAllFieldWork getAllFieldWorkService, CreateFieldWork createFieldWorkService,
            GetFieldWork getFieldWorkService,
            UpdateFieldWork updateFieldWorkService,
            GetActiveFieldWork getActiveFieldWorkService, 
            OpenFieldWork openFieldWorkService, 
            IGetStatisticsByRule getStatisticsByRuleService, 
            IGetStatisticsFromBuildingQuery getStatisticsFromBuildingQuery, 
            IGetStatisticsFromRulesQuery getStatisticsFromRulesQuery, 
            IGetRuleByFieldWork getRuleByFieldWork,
            IGetRuleByFieldWorkAndEntity getRuleByFieldWorkAndEntity,
            IGetFieldWorkRule getFieldWorkRule, 
            IRemoveFieldWorkRule removeFieldWorkRule, 
            IAddFieldWorkRule addFieldWorkRuleService,
            IUpdateBldReviewStatus updateBldReviewStatus,
            ISendFieldWorkEmail sendFieldWorkEmail,
            IAuthTokenService authTokenService  )
        {
            _getAllFieldWorkService = getAllFieldWorkService;
            _createFieldWorkService = createFieldWorkService;
            _getFieldWorkService = getFieldWorkService;
            _updateFieldWorkService = updateFieldWorkService;
            _getActiveFieldWorkService = getActiveFieldWorkService;
            _openFieldWorkService = openFieldWorkService;
            _getRuleByFieldWorkService = getRuleByFieldWork;
            getRuleByFieldWorkAndEntityService = getRuleByFieldWorkAndEntity;
            _getFieldWorkRuleService = getFieldWorkRule;
            _removeFieldWorkRule = removeFieldWorkRule;
            _addFieldWorkRuleService = addFieldWorkRuleService;
            _getStatisticsFromRuleQueryService = getStatisticsFromRulesQuery;
            _getStatisticsFromBuildingQueryService = getStatisticsFromBuildingQuery;
            _getStatisticsByRuleService = getStatisticsByRuleService;
            _updateBldReviewStatus = updateBldReviewStatus;
            _sendFieldWorkEmail = sendFieldWorkEmail;
            _authTokenService = authTokenService;
        }

        [HttpGet]
        [Route("")]
        public async Task<GetAllFieldWorkResponse> GetAllFieldWork()
        {
            return await _getAllFieldWorkService.Execute();
        }

        [HttpPost]
        [Route("")]
        public async Task<CreateFieldWorkResponse> CreateFieldWork(CreateFieldWorkRequest request)
        {
            var token = ExtractBearerToken();
            request.CreatedUser = await _authTokenService.GetUserIdFromToken(token);
            return await _createFieldWorkService.Execute(request);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<GetFieldWorkResponse> GetFieldWork(int id)
        {
            return await _getFieldWorkService.Execute(new GetFieldWorkRequest() { Id = id });
        }

        [HttpPut("{id:int}")]
        public async Task<UpdateFieldWorkResponse> UpdateFieldWork(int id, UpdateFieldWorkRequest request)
        {
            request.FieldWorkId = id;
            var token = ExtractBearerToken();
            var updatedUser = await _authTokenService.GetUserIdFromToken(token);
            request.UpdatedUser = updatedUser;
            return await _updateFieldWorkService.Execute(request);
        }
        [HttpGet]
        [Route("active")]
        public async Task<GetActiveFieldWorkResponse> GetActiveFieldWork()
        {
            return await _getActiveFieldWorkService.Execute();
        }

        [HttpPost]
        [Route("{id:int}/rules")]
        public async Task<AddFieldWorkRuleResponse> AddFieldWorkRule(int id, [FromBody]AddFieldWorkRuleRequest request)
        {
            var token = ExtractBearerToken();
            request.CreatedUser = await _authTokenService.GetUserIdFromToken(token);
            request.FieldWorkId = id;
            return await _addFieldWorkRuleService.Execute(request);
        }

        [HttpDelete("{id:int}/rules/{ruleId:long}")]
        public async Task<RemoveFieldWorkRuleResponse> RemoveFieldWorkRule(int id, long ruleId)
        {
            return await _removeFieldWorkRule.Execute(new RemoveFieldWorkRuleRequest() { Id = id, RuleId=ruleId });
        }

        [HttpGet]
        [Route("{id:int}/rule/{ruleId:long}")]
        public async Task<GetFieldWorkRuleResponse> GetFieldWorkRule(int id, long ruleId)
        {
            return await _getFieldWorkRuleService.Execute(new GetFieldWorkRuleRequest() { Id = id, RuleId=ruleId });
        }

        [HttpGet]
        [Route("{id:int}/rules")]
        public async Task<GetRuleByFieldWorkResponse> GetRuleByFieldWork(int id)
        {
            return await _getRuleByFieldWorkService.Execute(new GetRuleByFieldWorkRequest() { Id = id });
        }

        [HttpGet]
        [Route("{id:int}/rules/entity/{entityType}")]
        public async Task<GetRuleByFieldWorkAndEntityResponse> GetRuleByFieldWorkAndEntity(int id, EntityType entityType)
        {
            return await getRuleByFieldWorkAndEntityService.Execute(new GetRuleByFieldWorkAndEntityRequest() { Id = id, EntityType = entityType});
        }


        [HttpGet]
        [Route("{id:int}/rules/statistics")]
        public async Task<GetStatisticsFromRulesResponse> GetStatisticsFromRules(int id)
        {
            return await _getStatisticsFromRuleQueryService.Execute(new GetStatisticsFromRulesRequest() { Id=id});
        }

        [HttpGet]
        [Route("buildings/statistics")]
        public async Task<GetStatisticsFromBuildingResponse> GetStatisticsFromBuilding()
        {
            return await _getStatisticsFromBuildingQueryService.Execute();
        }

        [HttpGet]
        [Route("rules/statistics/{ruleId:long}")]
        public async Task<GetStatisticsByRuleResponse> GetStatisticsByRule(long id)
        {
            return await _getStatisticsByRuleService.Execute(new GetStatisticsByRuleRequest { Id = id });
        }

        //[HttpPatch]
        //[Route("{id:int}/open")]
        //public async Task<OpenFieldWorkResponse> OpenFieldWork(int id)
        //{
        //    var token = ExtractBearerToken();
        //    var request = new OpenFieldWorkRequest
        //    {
        //        FieldWorkId = id,
        //        UpdatedUser = await _authTokenService.GetUserIdFromToken(token)
        //    };
        //    return await _openFieldWorkService.Execute(request);
        //}

        /// <summary>
        /// Updates the BldReviewStatus to required in the geodatabase for the given fieldwork id and sets the fieldwork status to OPEN.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("fieldwork/{id:int}/updatebldreviewstatus")]
        public async Task<UpdateBldReviewStatusResponse> UpdateReviewStatus(int id)
        {
            var token = ExtractBearerToken();
            var updatedUser = await _authTokenService.GetUserIdFromToken(token);

            var request = new UpdateBldReviewStatusRequest
            {
                Id = id,
                UpdatedUser = updatedUser
            };

            return await _updateBldReviewStatus.Execute(request);
        }

        [HttpPost]
        [Route("fieldwork/{id:int}/sendfieldworkemail")]
        public async Task<SendFieldWorkEmailResponse> SendFieldWorkEmail(int id)
        {
            
            var request = new SendFieldWorkEmailRequest
            {
                FieldWorkId = id
            };

            return await _sendFieldWorkEmail.Execute(request);
        }
    }
}
