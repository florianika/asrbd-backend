using Application.Dtos.Quality;
using Application.Quality.BuildingQualityCheck.Request;
using Application.Quality.BuildingQualityCheck.Response;
using Application.Quality.RulesExecutor;
using Microsoft.Extensions.Logging;

namespace Application.Quality.BuildingQualityCheck
{
    public class BuidlingQualityCheck : IBuildingQualityCheck
    {
        private readonly ILogger _logger;
        private readonly Executor _executor;

        public BuidlingQualityCheck(ILogger<BuidlingQualityCheck> logger, 
                                    Executor executor) {
            _logger = logger;
            _executor = executor;
        }
        public async Task<BuildingQualityCheckResponse> Execute(BuildingQualityCheckRequest request)
        {
            //TODO 1. get buidling from geodatabase where id = request.BuildingId, using API 
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
            var buidling = new BuildingDto //TODO replcace this with api call to geodatabase
            {
                Entrances = GetEntrances(buildingId)
            };

            throw new NotImplementedException();
        }

        private static List<EntranceDto> GetEntrances(Guid buildingId) 
        {
            //new api call to the arcgis service
            //and get all entrance of the building
            var entrnces = new List<EntranceDto>(); //Todo replace this with api call to the geodatase
            foreach (var entrance in entrnces)
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
