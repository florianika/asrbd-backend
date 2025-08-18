using Application.FieldWork;
using Application.FieldWork.AssociateEmailTemplateWithFieldWork;
using Application.FieldWork.AssociateEmailTemplateWithFieldWork.Request;
using Application.FieldWork.AssociateEmailTemplateWithFieldWork.Response;
using Application.FieldWork.CanBeClosed;
using Application.FieldWork.CanBeClosed.Request;
using Application.FieldWork.CanBeClosed.Response;
using Application.FieldWork.ConfirmFieldworkClosure;
using Application.FieldWork.ConfirmFieldworkClosure.Request;
using Application.FieldWork.ConfirmFieldworkClosure.Response;
using Application.FieldWork.CreateFieldWork;
using Application.FieldWork.CreateFieldWork.Request;
using Application.FieldWork.CreateFieldWork.Response;
using Application.FieldWork.ExecuteJob;
using Application.FieldWork.ExecuteJob.Request;
using Application.FieldWork.ExecuteJob.Response;
using Application.FieldWork.GetActiveFieldWork;
using Application.FieldWork.GetActiveFieldWork.Response;
using Application.FieldWork.GetAllFieldWork;
using Application.FieldWork.GetAllFieldWork.Response;
using Application.FieldWork.GetFieldWork;
using Application.FieldWork.GetFieldWork.Request;
using Application.FieldWork.GetFieldWork.Response;
using Application.FieldWork.GetJobResults;
using Application.FieldWork.GetJobResults.Request;
using Application.FieldWork.GetJobResults.Response;
using Application.FieldWork.GetJobStatus;
using Application.FieldWork.GetJobStatus.Request;
using Application.FieldWork.GetJobStatus.Response;
using Application.FieldWork.OpenFieldWork;
using Application.FieldWork.SendFieldWorkEmail;
using Application.FieldWork.TestUntestedBld;
using Application.FieldWork.TestUntestedBld.Request;
using Application.FieldWork.TestUntestedBld.Response;
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
using Application.FieldWorkRule.RemoveFieldWorkRule;
using Application.FieldWorkRule.RemoveFieldWorkRule.Request;
using Application.FieldWorkRule.RemoveFieldWorkRule.Response;
using Application.Ports;
using Application.Queries.GetBuildingSummaryStats;
using Application.Queries.GetBuildingSummaryStats.Response;
using Application.Queries.GetFieldworkProgressByMunicipality;
using Application.Queries.GetFieldworkProgressByMunicipality.Response;
using Domain;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using WebApi.DTOs;

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
        private readonly IAssociateEmailTemplateWithFieldWork _associateEmailTemplateWithFieldWorkService;
        private readonly IRemoveFieldWorkRule _removeFieldWorkRule;
        private readonly IAddFieldWorkRule _addFieldWorkRuleService;
        private readonly IGetFieldWorkRule _getFieldWorkRuleService;
        private readonly IGetRuleByFieldWork _getRuleByFieldWorkService;
        private readonly IGetRuleByFieldWorkAndEntity _getRuleByFieldWorkAndEntityService;
        private readonly IExecuteJob _executeJobService;
        private readonly IGetJobStatus _getJobStatusService;
        private readonly IGetJobResults _getJobResultsService;
        private readonly IUpdateBldReviewStatus _updateBldReviewStatus;
        private readonly ISendFieldWorkEmail _sendFieldWorkEmail;
        private readonly UpdateFieldworkStatus _updateFieldworkStatusService;
        private readonly ITestUntestedBld _testUntestedBldService;
        private readonly ICanBeClosed _canBeClosedService;
        private readonly IGetBuildingSummaryStatsQuery _getBuildingSummaryStatsQuery;
        private readonly IConfirmFieldworkClosure _confirmFieldworkClosureService;
        private readonly IGetFieldworkProgressByMunicipalityQuery _getFieldworkProgressByMunicipalityQuery;

        public FieldWorkController(GetAllFieldWork getAllFieldWorkService, CreateFieldWork createFieldWorkService,
            GetFieldWork getFieldWorkService,
            UpdateFieldWork updateFieldWorkService,
            GetActiveFieldWork getActiveFieldWorkService, 
            OpenFieldWork openFieldWorkService, 
            IExecuteJob executeJobService,
            IGetJobStatus getJobStatusService,
            IGetJobResults getJobResultsService,
            IGetRuleByFieldWork getRuleByFieldWork,
            IGetRuleByFieldWorkAndEntity getRuleByFieldWorkAndEntity,
            IGetFieldWorkRule getFieldWorkRule, 
            IRemoveFieldWorkRule removeFieldWorkRule, 
            IAddFieldWorkRule addFieldWorkRuleService,
            IUpdateBldReviewStatus updateBldReviewStatus,
            ISendFieldWorkEmail sendFieldWorkEmail,
            IAuthTokenService authTokenService,
            IAssociateEmailTemplateWithFieldWork associateEmailTemplateWithFieldWorkService,
            UpdateFieldworkStatus updateFieldworkStatusService,
            ITestUntestedBld testUntestedBldService,
            ICanBeClosed canBeClosedService, IGetBuildingSummaryStatsQuery getBuildingSummaryStatsQuery,
            IConfirmFieldworkClosure confirmFieldworkClosureService,
            IGetFieldworkProgressByMunicipalityQuery getFieldworkProgressByMunicipalityQuery)
        {
            _getAllFieldWorkService = getAllFieldWorkService;
            _createFieldWorkService = createFieldWorkService;
            _getFieldWorkService = getFieldWorkService;
            _updateFieldWorkService = updateFieldWorkService;
            _getActiveFieldWorkService = getActiveFieldWorkService;
            _openFieldWorkService = openFieldWorkService;
            _getRuleByFieldWorkService = getRuleByFieldWork;
            _getRuleByFieldWorkAndEntityService = getRuleByFieldWorkAndEntity;
            _getFieldWorkRuleService = getFieldWorkRule;
            _removeFieldWorkRule = removeFieldWorkRule;
            _addFieldWorkRuleService = addFieldWorkRuleService;
            _executeJobService = executeJobService;
            _getJobStatusService = getJobStatusService;
            _getJobResultsService = getJobResultsService;
            _updateBldReviewStatus = updateBldReviewStatus;
            _sendFieldWorkEmail = sendFieldWorkEmail;
            _authTokenService = authTokenService;
            _associateEmailTemplateWithFieldWorkService = associateEmailTemplateWithFieldWorkService;
            _updateFieldworkStatusService = updateFieldworkStatusService;
            _testUntestedBldService = testUntestedBldService;
            _canBeClosedService = canBeClosedService;
            _getBuildingSummaryStatsQuery = getBuildingSummaryStatsQuery;
            _confirmFieldworkClosureService = confirmFieldworkClosureService;
            _getFieldworkProgressByMunicipalityQuery = getFieldworkProgressByMunicipalityQuery;
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
        public async Task<AddFieldWorkRuleResponse> AddFieldWorkRule(int id, [FromBody] AddFieldWorkRuleRequestDTO rule)
        {
            var token = ExtractBearerToken();
            AddFieldWorkRuleRequest request = new AddFieldWorkRuleRequest
            {
                FieldWorkId = id,
                RuleId = rule.RuleId,
                CreatedUser = await _authTokenService.GetUserIdFromToken(token)

            };
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
            return await _getRuleByFieldWorkAndEntityService.Execute(new GetRuleByFieldWorkAndEntityRequest() { Id = id, EntityType = entityType});
        }

        [HttpPost]
        [Route("{id:int}/execute-job")]
        public async Task<ExecuteJobResponse> ExecuteStatistics(int id, [FromBody] ExecuteJobRequest request)
        {
            var token = ExtractBearerToken();
            request.CreatedUser = await _authTokenService.GetUserIdFromToken(token);
            request.Id = id;
            return await _executeJobService.Execute(request);
        }
        [HttpGet]
        [Route("job/{id:int}/status")]
        public async Task<GetJobStatusResponse> GetJobStatus(int id)
        {
            return await _getJobStatusService.Execute(new GetJobStatusRequest() { Id = id });
        }

        [HttpGet]
        [Route("job/{id:int}/results")]
        public async Task<GetJobResultsResponse> GetJobResults(int id)
        {
            return await _getJobResultsService.Execute(new GetJobResultsRequest() { Id = id });
        }

        
        [HttpPost]
        [Route("{id:int}/open")]
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

        //servis i perkohshem
        [HttpPatch("{id:int}/change-status")]
        public async Task<IActionResult> UpdateFieldWorkStatus(int id, [FromBody] UpdateFieldworkStatusRequest request)
        {
            try
            {
                var updated = await _updateFieldworkStatusService.ExecuteAsync(id, request.Status);
                if (updated)
                    return NoContent(); // 204 Success with no content
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost]
        [Route("{id:int}/run-test-job")]
        public async Task<TestUntestedBldResponse> TestUntestedBld(int id, [FromBody] TestUntestedBldRequest request)
        {
            var token = ExtractBearerToken();
            request.CreatedUser = await _authTokenService.GetUserIdFromToken(token);
            request.Id = id;
            return await _testUntestedBldService.Execute(request);
        }

        [HttpGet]
        [Route("{id:int}/can-be-closed")]
        public async Task<CanBeClosedResponse> CanBeClosed(int id)
        {
            return await _canBeClosedService.Execute(new CanBeClosedRequest() { Id = id });
        }

        [HttpGet]
        [Route("stats")]
        public async Task<GetBuildingSummaryStatsResponse> GetBuildingSummaryStats()
        {
            return await _getBuildingSummaryStatsQuery.Execute();
        }

        [HttpPatch("{id:int}/email/template/open")]
        public async Task<AssociateEmailTemplateWithFieldWorkResponse> AssociateEmailTemplateOpenWithFieldWork(int id, [FromBody] AssociateEmailTemplateWithFieldWorkRequestDTO emailtemplate)
        {
            var token = ExtractBearerToken();
            AssociateEmailTemplateWithFieldWorkRequest request = new AssociateEmailTemplateWithFieldWorkRequest
            {
                FieldWorkId = id,
                EmailTemplateId = emailtemplate.EmailTemplateId,
                UpdatedUser = await _authTokenService.GetUserIdFromToken(token),
                isOpen = true
            };
            return await _associateEmailTemplateWithFieldWorkService.Execute(request);
        }

        [HttpPatch("{id:int}/email/template/close")]
        public async Task<AssociateEmailTemplateWithFieldWorkResponse> AssociateEmailTemplateCloseWithFieldWork(int id, [FromBody] AssociateEmailTemplateWithFieldWorkRequestDTO emailtemplate)
        {
            var token = ExtractBearerToken();
            AssociateEmailTemplateWithFieldWorkRequest request = new AssociateEmailTemplateWithFieldWorkRequest
            {
                FieldWorkId = id,
                EmailTemplateId = emailtemplate.EmailTemplateId,
                UpdatedUser = await _authTokenService.GetUserIdFromToken(token),
                isOpen = false
            };
            return await _associateEmailTemplateWithFieldWorkService.Execute(request);
        }

        [HttpPost]
        [Route("close")]
        public async Task<ConfirmFieldworkClosureResponse> ConfirmFieldworkClosure(ConfirmFieldworkClosureRequest request)
        {
            var token = ExtractBearerToken();
            var updatedUser = await _authTokenService.GetUserIdFromToken(token);
            request.UpdatedUser = updatedUser;

            return await _confirmFieldworkClosureService.Execute(request);
        }

        [HttpGet]
        [Route("progress")]
        public async Task<GetFieldworkProgressByMunicipalityResponse> GetFieldworkProgressByMunicipality()
        {
            return await _getFieldworkProgressByMunicipalityQuery.Execute();
        }

    }
}
