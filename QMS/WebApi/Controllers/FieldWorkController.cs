using Application.FieldWork.CreateFieldWork;
using Application.FieldWork.CreateFieldWork.Request;
using Application.FieldWork.CreateFieldWork.Response;
using Application.FieldWork.GetAllFieldWork;
using Application.FieldWork.GetAllFieldWork.Response;
using Application.Ports;
using Application.Rule.CreateRule.Request;
using Application.Rule.CreateRule.Response;
using Application.Rule.GetAllRules.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/qms/fieldwork")]
    public class FieldWorkController : ControllerBase
    {
        private readonly GetAllFieldWork _getAllFieldWorkService;
        private readonly CreateFieldWork _createFieldWorkService;
        private readonly IAuthTokenService _authTokenService;
        public FieldWorkController(GetAllFieldWork getAllFieldWorkService, CreateFieldWork createFieldWorkService, IAuthTokenService authTokenService  )
        {
            _getAllFieldWorkService = getAllFieldWorkService;
            _createFieldWorkService = createFieldWorkService;
            _authTokenService = authTokenService;
        }
        [HttpGet]
        [Route("")]
        public async Task<GetAllFieldWorkResponse> GetAllRules()
        {
            return await _getAllFieldWorkService.Execute();
        }
        [HttpPost]
        [Route("")]
        public async Task<CreateFieldWorkResponse> CreateFieldWork(CreateFieldWorkRequest request)
        {
            var token = Request.Headers["Authorization"].ToString();
            token = token.Replace("Bearer ", "");
            request.CreatedUser = await _authTokenService.GetUserIdFromToken(token);
            return await _createFieldWorkService.Execute(request);
        }

    }
}
