using System.Linq.Dynamic.Core;
using Application.Common.Converter;
using Application.Dtos.Quality;
using Application.Ports;
using Domain.Enum;

namespace Application.Quality.RulesExecutor
{
    public class Executor
    {
        private readonly IRuleRepository _ruleRepository;
        private readonly ProcessOutputLogs.Logger _processOutputLogger;

        public Executor(IRuleRepository ruleRepository, ProcessOutputLogs.Logger processOutputLogger) 
        {
            _ruleRepository = ruleRepository;
            _processOutputLogger = processOutputLogger;
        }

        public async Task ExecuteBuildingRules(BuildingDto building, Guid user)
        {
            var rules = await _ruleRepository.GetActiveRulesByEntity(EntityType.BUILDING);
            await ExecuteEntranceRules(building.Entrances, building.GlobalId, user);
            var buildings = new List<BuildingDto>
            {
                building
            };
            if(rules != null && rules.Count > 0)
            {
                foreach (var rule in rules)
                {
                    if(String.IsNullOrEmpty(rule.Expression)) {
                        break;
                    }
                    var expression = PythonToCSharpConverter.WhereClouseToLinqString(rule.Expression);
                    var buidlingsWithErrors = buildings.AsQueryable().Where(expression);
                    foreach (var errorBuilding in buidlingsWithErrors)
                    {
                        //TODO set building status
                        await _processOutputLogger.AddRecursiveProcessOutputLogAsync(errorBuilding.GlobalId, null, null, EntityType.BUILDING, ErrorLevel.OWN, rule, user);
                    }
                }
            }
        }

        private async Task ExecuteEntranceRules(List<EntranceDto> entrances, Guid buildingId, Guid user)
        {
            var rules = await _ruleRepository.GetActiveRulesByEntity(EntityType.ENTRANCE);
            foreach (var entrance in entrances)
            {
                await ExecuteDwellingRules(entrance.Dwellings, entrance.GlobalId, buildingId, user);
            }
            if(rules != null && rules.Count > 0) 
            {
                foreach (var rule in rules)
                {
                    if(String.IsNullOrEmpty(rule.Expression))
                    {
                        break;
                    }
                    var expression = PythonToCSharpConverter.WhereClouseToLinqString(rule.Expression);
                    var entrancesWithErrors = entrances.AsQueryable().Where(expression);
                    foreach (var errorEntrance in entrancesWithErrors)
                    {
                        //TODO set entrance status
                        //TODO set building status
                        await _processOutputLogger.AddRecursiveProcessOutputLogAsync(buildingId, errorEntrance.GlobalId, null, EntityType.DWELLING, ErrorLevel.OWN, rule, user);
                    }
                }
            }
        }

        private async Task ExecuteDwellingRules(List<DwellingDto> dwellings, Guid entranceId, Guid buildingId, Guid user)
        {
            var rules = await _ruleRepository.GetActiveRulesByEntity(EntityType.DWELLING);
            if (rules != null && rules.Count > 0)
            {
                foreach (var rule in rules)
                {
                    if (String.IsNullOrEmpty(rule.Expression))
                    {
                        break;
                    }
                    var expression = PythonToCSharpConverter.WhereClouseToLinqString(rule.Expression);
                    var dwellingWithErrors = dwellings.AsQueryable().Where(expression);
                    foreach (var errorDwelling in dwellingWithErrors)
                    {
                        //TODO Set dwelling status
                        //TODO set entrance status
                        //TODO set building status
                        await _processOutputLogger.AddRecursiveProcessOutputLogAsync(buildingId, entranceId, errorDwelling.GlobalId, EntityType.DWELLING, ErrorLevel.OWN, rule, user);
                    }
                }
            }
        }
    }
}
