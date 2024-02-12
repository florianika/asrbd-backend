using System;
using Application.Ports;
using Domain.Enum;

namespace Application.Quality.ProcessOutputLogs
{
    public class Logger
    {
        private readonly IProcessOutputLogRepository _processOutputLogRepository;

        public Logger(IProcessOutputLogRepository processOutputLogRepository)
        {
            _processOutputLogRepository = processOutputLogRepository;
        }
    	

        public async Task AddRecursiveProcessOutputLogAsync(Guid builingId, Guid? entranceId, Guid? dwellingId, EntityType entityType, ErrorLevel errorLevel, Domain.Rule rule, Guid user)
        {
            if(entityType == EntityType.BUILDING) 
            {
                var processOutputLog = CreateProcesOutpuLog(builingId, entranceId, dwellingId, entityType, errorLevel, rule, user);
                await _processOutputLogRepository.CreateProcessOutputLog(processOutputLog);
                return;
            } 
            else
            {
                var processOutputLog = CreateProcesOutpuLog(builingId, entranceId, dwellingId, entityType, errorLevel, rule, user);
                await _processOutputLogRepository.CreateProcessOutputLog(processOutputLog);
                await AddRecursiveProcessOutputLogAsync(builingId, entranceId, dwellingId, GetPreviousEnityType(entityType), GetPreviousErrorLevel(errorLevel), rule, user);
            } 

        }

        private static Domain.ProcessOutputLog CreateProcesOutpuLog(Guid builingId, Guid? entranceId, Guid? dwellingId, EntityType entityType, ErrorLevel errorLevel, Domain.Rule rule, Guid user)
        {
            return new Domain.ProcessOutputLog 
            {
                BldId = builingId,
                EntId = entranceId,
                DwlId = dwellingId,
                CreatedTimestamp = DateTime.Now,
                CreatedUser = user, //TODO get the authenticate user here
                EntityType = entityType,
                ErrorLevel = errorLevel,
                Variable = rule.Variable,
                Rule = rule,
                RuleId = rule.Id,
                Id = Guid.NewGuid(),
                QualityAction = rule.QualityAction,
                QualityMessageAl = errorLevel == ErrorLevel.OWN ? rule.QualityMessageAl : "Gabime në hyrje ose banesë.",
                QualityMessageEn = errorLevel == ErrorLevel.OWN ? rule.QualityMessageEn : "There are errors in entrances or dwellings.",
                QualityStatus = QualityStatus.PENDING
            };
        }
        
        private static EntityType GetPreviousEnityType(EntityType entityType) 
        {
            if(entityType == EntityType.DWELLING) 
            {
                return EntityType.ENTRANCE;
            }
            if(entityType == EntityType.ENTRANCE) {
                return EntityType.BUILDING;
            }
            return EntityType.DWELLING;
        }

        private static ErrorLevel GetPreviousErrorLevel(ErrorLevel errorLevel) 
        {
            if(errorLevel == ErrorLevel.OWN) 
            {
                return ErrorLevel.CHILD;
            }
            if(errorLevel == ErrorLevel.CHILD) {
                return ErrorLevel.GRANDCHILD;
            }
            return ErrorLevel.OWN;
        }
    }
}
