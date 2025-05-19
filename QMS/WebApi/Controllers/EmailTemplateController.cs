using Application.EmailTemplate.CreateEmailTemplate;
using Application.EmailTemplate.CreateEmailTemplate.Request;
using Application.EmailTemplate.CreateEmailTemplate.Response;
using Application.EmailTemplate.GetAllEmailTemplate.Response;
using Application.EmailTemplate.GetAllEmailTemplate;
using Application.EmailTemplate.GetEmailTemplate;
using Application.EmailTemplate.GetEmailTemplate.Request;
using Application.EmailTemplate.GetEmailTemplate.Response;
using Application.EmailTemplate.UpdateEmailTemplate;
using Application.EmailTemplate.UpdateEmailTemplate.Request;
using Application.EmailTemplate.UpdateEmailTemplate.Response;
using Application.Ports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/qms/emailtemplate")]
    public class EmailTemplateController : QmsControllerBase
    {
        private readonly IGetAllEmailTemplate _getAllEmailTemplateService;
        private readonly IGetEmailTemplate _getEmailTemplateService;
        private readonly ICreateEmailTemplate _createEmailTemplateService;
        private readonly IUpdateEmailTemplate _updateEmailTemplateService;
        private readonly IAuthTokenService _authTokenService;
        public EmailTemplateController(
            IGetEmailTemplate getEmailTemplateService,
            IGetAllEmailTemplate getAllEmailTemplateService,
            ICreateEmailTemplate createEmailTemplateService,
            IUpdateEmailTemplate updateEmailTemplateService,
            IAuthTokenService authTokenService)         
        {
            _getAllEmailTemplateService = getAllEmailTemplateService;
            _getEmailTemplateService = getEmailTemplateService;
            _createEmailTemplateService = createEmailTemplateService;
            _updateEmailTemplateService = updateEmailTemplateService;
            _authTokenService = authTokenService;
        }

        [HttpGet]
        [Route("")]
        public async Task<GetAllEmailTemplateResponse> GetAllEmailTemplate()
        {
            return await _getAllEmailTemplateService.Execute();
        }

        [HttpPost]
        [Route("")]
        public async Task<CreateEmailTemplateResponse> CreateEmailTemplate(CreateEmailTemplateRequest request)
        {
            var token = ExtractBearerToken();
            request.CreatedUser = await _authTokenService.GetUserIdFromToken(token);
            return await _createEmailTemplateService.Execute(request);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<GetEmailTemplateResponse> GetEmailTemplate(int id)
        {
            return await _getEmailTemplateService.Execute(new GetEmailTemplateRequest() { Id = id });
        }

        [HttpPut("{id:int}")]
        public async Task<UpdateEmailTemplateResponse> UpdateEmailTemplate(int id, UpdateEmailTemplateRequest request)
        {
            request.EmailTemplateId = id;
            var token = ExtractBearerToken();
            var updatedUser = await _authTokenService.GetUserIdFromToken(token);
            request.UpdatedUser = updatedUser;
            return await _updateEmailTemplateService.Execute(request);
        }

    }
}
