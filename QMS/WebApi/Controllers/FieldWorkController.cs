using Application.FieldWork.GetAllFieldWork;
using Application.FieldWork.GetAllFieldWork.Response;
using Application.Ports;
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
        private readonly GetAllFieldWork _getAllFieldWork;
        private readonly IAuthTokenService _authTokenService;
        public FieldWorkController(GetAllFieldWork getAllFieldWork, IAuthTokenService authTokenService  )
        {
            _getAllFieldWork = getAllFieldWork;
            _authTokenService = authTokenService;
        }
        [HttpGet]
        [Route("")]
        public async Task<GetAllFieldWorkResponse> GetAllRules()
        {
            return await _getAllFieldWork.Execute();
        }
    }
}
