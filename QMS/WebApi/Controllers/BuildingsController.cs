using Application.Building.TestBuildings;
using Application.Building.TestBuildings.Request;
using Application.Building.TestBuildings.Response;
using Application.Ports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Building.GetBldJobStatus.Response;
using Application.Building.GetBldJobStatus.Request;
using Application.Building.GetBldJobStatus;
using Application.Building;
using Application.Queries.GetBuildingQualityStats.Response;
using Application.Queries.GetDwellingQualityStats.Response;
using Application.Queries.GetBuildingQualityStats;
using Application.Queries.GetDwellingQualityStats;
using Application.Building.CreateAnnualSnapshot.Response;
using Application.Building.CreateAnnualSnapshot.Request;
using Application.Building.CreateAnnualSnapshot;
using Application.Building.GetAllAnnualSnapshots.Response;
using Application.Building.GetAnnualSnapshotById.Request;
using Application.Building.GetAnnualSnapshotById.Response;
using Application.Building.GetAllAnnualSnapshots;
using Application.Building.GetAnnualSnapshotById;
using System.IO;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/qms/buildings")]
    public class BuildingsController : QmsControllerBase
    {
        private readonly ITestBuildings _testBuildingsService;
        private readonly IAuthTokenService _authTokenService;
        private readonly IGetBldJobStatus _getJobStatusService;
        private readonly IGetBuildingQualityStatsQuery _getBuildingQualityStatsQuery;
        private readonly IGetDwellingQualityStatsQuery _getDwellingQualaityStatsQuery;
        private readonly ICreateAnnualSnapshot _createAnnualSnapshotService;
        private readonly IGetAllAnnualSnapshots _getAllAnnualSnapshotsService;
        private readonly IGetAnnualSnapshotById _getAnnualSnapshotByIdService;
        private readonly IWebHostEnvironment _env;
        public BuildingsController(ITestBuildings testBuildingsService,
            IAuthTokenService authTokenService,
            IGetBldJobStatus getJobStatusService,
            IGetBuildingQualityStatsQuery getBuildingQualityStatsQuery,
            IGetDwellingQualityStatsQuery getDwellingQualityStatsQuery,
            ICreateAnnualSnapshot createAnnualSnapshotService,
            IGetAllAnnualSnapshots getAllAnnualSnapshotsService,
            IGetAnnualSnapshotById getAnnualSnapshotByIdService,
            IWebHostEnvironment env
            )
        {
            _testBuildingsService = testBuildingsService;
            _authTokenService = authTokenService;
            _getJobStatusService = getJobStatusService;
            _getBuildingQualityStatsQuery = getBuildingQualityStatsQuery;
            _getDwellingQualaityStatsQuery = getDwellingQualityStatsQuery;
            _createAnnualSnapshotService = createAnnualSnapshotService;
            _getAllAnnualSnapshotsService = getAllAnnualSnapshotsService;
            _getAnnualSnapshotByIdService = getAnnualSnapshotByIdService;
            _env = env;
        }
        [HttpPost]
        [Route("run-test-job/all")]
        public async Task<TestBuildingsResponse> TestAllBuildings([FromBody] TestBuildingRequestDTO dto)
        {
            TestBuildingsRequest request = new TestBuildingsRequest();
            var token = ExtractBearerToken();
            request.CreatedUser = await _authTokenService.GetUserIdFromToken(token);
            request.isAllBuildings = true; // all buildings
            request.StartAt = dto?.StartAt;
            return await _testBuildingsService.Execute(request);
        }

        [HttpPost]
        [Route("run-test-job/untested")]
        public async Task<TestBuildingsResponse> TestUntestedBld([FromBody] TestBuildingRequestDTO dto)
        {
            TestBuildingsRequest request = new TestBuildingsRequest();
            var token = ExtractBearerToken();
            request.CreatedUser = await _authTokenService.GetUserIdFromToken(token);
            request.isAllBuildings = false; // untested buildings
            request.StartAt = dto?.StartAt;
            return await _testBuildingsService.Execute(request);
        }
        [HttpGet]
        [Route("status-test-job/{id:int}")]
        public async Task<GetBldJobStatusResponse> BldGetJobStatus(int id)
        {
            return await _getJobStatusService.Execute(new GetBldJobStatusRequest() { Id = id });
        }

        [HttpGet]
        [Route("bld-quality-stats")]
        public async Task<GetBuildingQualityStatsResponse> GetBuildingQualityStats()
        {
            return await _getBuildingQualityStatsQuery.Execute();
        }

        [HttpGet]
        [Route("dwl-quality-stats")]
        public async Task<GetDwellingQualityStatsResponse> GetDwellingQualityStats()
        {
            return await _getDwellingQualaityStatsQuery.Execute();
        }

        [HttpPost]
        [Route("annual-snapshot")]
        public async Task<CreateAnnualSnapshotResponse> CreateAnnualSnapshot(CreateAnnualSnapshotRequest request)
        {
            var token = ExtractBearerToken();
            request.CreatedBy = await _authTokenService.GetUserIdFromToken(token);
            return await _createAnnualSnapshotService.Execute(request);
        }

        [HttpGet]
        [Route("annual-snapshots")]
        public async Task<GetAllAnnualSnapshotsResponse> GetAllAnnualSnapshots()
        {
            return await _getAllAnnualSnapshotsService.Execute();
        }

        [HttpGet]
        [Route("annual-snapshot/{id:int}")]
        public async Task<GetAnnualSnapshotByIdResponse> GetAnnualSnapshotById(int id)
        {
            return await _getAnnualSnapshotByIdService.Execute(new GetAnnualSnapshotByIdRequest() { Id = id });
        }

        [HttpGet("annual-snapshot/download/{*path}")]
        public IActionResult Download(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return BadRequest("Path is required.");

            path = Uri.UnescapeDataString(path).TrimStart('/');

            if (path.Contains(".."))
                return BadRequest("Invalid path.");

            // Root bazuar në ContentRoot (aty ku është Program.cs)
            var root = _env.ContentRootPath;

            var fullPath = Path.Combine(root, path.Replace("/", Path.DirectorySeparatorChar.ToString()));

            if (!System.IO.File.Exists(fullPath))
                return NotFound("File not found.");

            var fileName = Path.GetFileName(fullPath);
            var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read);

            return File(stream, "application/octet-stream", fileName);
        }

    }
}
