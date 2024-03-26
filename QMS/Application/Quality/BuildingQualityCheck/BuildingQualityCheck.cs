using Application.Dtos.Quality;
using Application.Quality.BuildingQualityCheck.Request;
using Application.Quality.BuildingQualityCheck.Response;
using Application.Quality.RulesExecutor;
using Microsoft.Extensions.Logging;

namespace Application.Quality.BuildingQualityCheck
{
    public class BuildingQualityCheck : IBuildingQualityCheck
    {
        private readonly ILogger _logger;
        private readonly Executor _executor;

        public BuildingQualityCheck(ILogger<BuildingQualityCheck> logger, Executor executor) 
        {
            _logger = logger;
            _executor = executor;
        }
        public async Task<BuildingQualityCheckResponse> Execute(BuildingQualityCheckRequest request)
        {
            //TODO 1. get building from geo database where id = request.BuildingId, using API 
            var building = GetBuilding(request.BuildingId);
            await _executor.ExecuteBuildingRules(building, request.ExecutionUser);
            
            throw new NotImplementedException();
        }

        private static BuildingDto GetBuilding(Guid buildingId) 
        {
            //new api call to the arcgis service 
            //and get the building
            //var httpClient = new HttpClient
            //{
            //    BaseAddress = System.Uri()
            //};
            var building = new BuildingDto //TODO replace this with api call to geo database
            {
                Entrances = GetEntrances(buildingId)
            };

            throw new NotImplementedException();
        }

        private static List<EntranceDto> GetEntrances(Guid buildingId) 
        {
            //new api call to the arcgis service
            //and get all entrance of the building
            var entrances = new List<EntranceDto>(); //Todo replace this with api call to the geo database
            foreach (var entrance in entrances)
            {
                entrance.Dwellings = GetDwellings(entrance.GlobalId);
            }
            throw new NotImplementedException();
        }

        private static List<DwellingDto> GetDwellings(Guid entranceId)
        {
            //new api call to the arcgis service
            //and get all dwelling of a building, all dwelling for each entrance
            throw new NotImplementedException();
        }
    }
}
