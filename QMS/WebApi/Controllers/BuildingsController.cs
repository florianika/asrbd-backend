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
using Application.Exceptions;
using Application.Queries.GetBuildingWithQuePendingLogs.Response;
using Application.Queries.GetBuildingWithQuePendingLogs.Request;
using Application.Queries.GetBuildingWithQuePendingLogs;

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
        private readonly IGetBuildingWithQuePendingLogs _getBuildingWithQuePendingLogsService;
        public BuildingsController(ITestBuildings testBuildingsService,
            IAuthTokenService authTokenService,
            IGetBldJobStatus getJobStatusService,
            IGetBuildingQualityStatsQuery getBuildingQualityStatsQuery,
            IGetDwellingQualityStatsQuery getDwellingQualityStatsQuery,
            ICreateAnnualSnapshot createAnnualSnapshotService,
            IGetAllAnnualSnapshots getAllAnnualSnapshotsService,
            IGetAnnualSnapshotById getAnnualSnapshotByIdService,
            IWebHostEnvironment env,
            IGetBuildingWithQuePendingLogs getBuildingWithQuePendingLogsService
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
            _getBuildingWithQuePendingLogsService = getBuildingWithQuePendingLogsService;
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
            request.RunUpdates = dto.RunUpdates;
            return await _testBuildingsService.Execute(request);
        }

        [HttpPost]
        [Route("run-test-job/untested")]
        public async Task<TestBuildingsResponse> TestUntestedBld([FromBody] TestBuildingRequestDTO dto) //ketu do shtoj runUpdates
        {
            TestBuildingsRequest request = new TestBuildingsRequest();
            var token = ExtractBearerToken();
            request.CreatedUser = await _authTokenService.GetUserIdFromToken(token);
            request.isAllBuildings = false; // untested buildings
            request.StartAt = dto?.StartAt;
            request.RunUpdates = dto.RunUpdates;
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

        [HttpPost("annual-snapshot/download/{referenceYear}")]
        public FileResult Download(string referenceYear)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "exports", $"annual-snapshot_{referenceYear}.zip");

            if (!System.IO.File.Exists(fullPath))
                throw new NotFoundException("File not found.");

            var fileName = Path.GetFileName(fullPath);
            var zipFile = System.IO.File.ReadAllBytes(fullPath);
            

            return File(zipFile, "application/zip", fileName);
        }

        [HttpGet]
        [Route("que/pending/municipality/{municipalityCode}")]
        public async Task<GetBuildingWithQuePendingLogsResponse> GetBuildingWithQuePendingLogsResponse(string municipalityCode)
        {
            return await _getBuildingWithQuePendingLogsService.Execute(new GetBuildingWithQuePendingLogsRequest() {  MunicipalityCode = municipalityCode });
        }


    }
}
