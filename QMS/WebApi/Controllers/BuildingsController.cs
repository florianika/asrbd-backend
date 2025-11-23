using Application.Building.TestBuildings;
using Application.Building.TestBuildings.Request;
using Application.Building.TestBuildings.Response;
using Application.FieldWork.GetJobStatus;
using Application.FieldWork.GetJobStatus.Request;
using Application.FieldWork.GetJobStatus.Response;
using Application.Ports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/qms/buildings")]
    public class BuildingsController : QmsControllerBase
    {
        private readonly ITestBuildings _testBuildingsService;
        private readonly IAuthTokenService _authTokenService;
        private readonly IGetJobStatus _getJobStatusService;
        public BuildingsController(ITestBuildings testBuildingsService,
            IAuthTokenService authTokenService,
            IGetJobStatus getJobStatusService
            )
        {
            _testBuildingsService = testBuildingsService;
            _authTokenService = authTokenService;
            _getJobStatusService = getJobStatusService;
        }
        [HttpPost]
        [Route("/run-test-job/all")]
        public async Task<TestBuildingsResponse> TestAllBuildings()
        {
            TestBuildingsRequest request = new TestBuildingsRequest();
            var token = ExtractBearerToken();
            request.CreatedUser = await _authTokenService.GetUserIdFromToken(token);
            request.isAllBuildings = true; // all buildings
            return await _testBuildingsService.Execute(request);
        }

        [HttpPost]
        [Route("/run-test-job/untested")]
        public async Task<TestBuildingsResponse> TestUntestedBld()
        {
            TestBuildingsRequest request = new TestBuildingsRequest();
            var token = ExtractBearerToken();
            request.CreatedUser = await _authTokenService.GetUserIdFromToken(token);
            request.isAllBuildings = false; // untested buildings
            return await _testBuildingsService.Execute(request);
        }
        [HttpGet]
        [Route("job/{id:int}/status")]
        public async Task<GetJobStatusResponse> GetJobStatus(int id)
        {
            return await _getJobStatusService.Execute(new GetJobStatusRequest() { Id = id });
        }
    }
}
