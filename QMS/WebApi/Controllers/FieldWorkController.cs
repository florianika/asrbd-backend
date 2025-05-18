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
using Application.FieldWork.OpenFieldWork.Request;
using Application.FieldWork.OpenFieldWork.Response;
using Application.FieldWork.UpdateFieldWork;
using Application.FieldWork.UpdateFieldWork.Request;
using Application.FieldWork.UpdateFieldWork.Response;
using Application.Ports;
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
        private readonly GetFieldWork _getFieldWorkService;
        private readonly UpdateFieldWork _updateFieldWorkService;
        private readonly GetActiveFieldWork _getActiveFieldWorkService;
        private readonly OpenFieldWork _openFieldWorkService;
        private readonly IAuthTokenService _authTokenService;
        public FieldWorkController(GetAllFieldWork getAllFieldWorkService, CreateFieldWork createFieldWorkService,
            GetFieldWork getFieldWorkService,
            UpdateFieldWork updateFieldWorkService,
            GetActiveFieldWork getActiveFieldWorkService, 
            OpenFieldWork openFieldWorkService,
            IAuthTokenService authTokenService  )
        {
            _getAllFieldWorkService = getAllFieldWorkService;
            _createFieldWorkService = createFieldWorkService;
            _getFieldWorkService = getFieldWorkService;
            _updateFieldWorkService = updateFieldWorkService;
            _getActiveFieldWorkService = getActiveFieldWorkService;
            _openFieldWorkService = openFieldWorkService;
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
            var token = Request.Headers["Authorization"].ToString();
            token = token.Replace("Bearer ", "");
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
            var token = Request.Headers["Authorization"].ToString();
            token = token.Replace("Bearer ", "");
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
        [Route("open")]
        //TODO change this method to patch 
        //TODO all methods where there is a partial update of the fields should be changed to patch
        public async Task<OpenFieldWorkResponse> OpenFieldWork(OpenFieldWorkRequest request)
        {
            var token = Request.Headers["Authorization"].ToString();
            token = token.Replace("Bearer ", "");
            request.UpdatedUser = await _authTokenService.GetUserIdFromToken(token);
            return await _openFieldWorkService.Execute(request);
        }

    }
}
