using Application.Ports;

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
        public async Task<bool> ExecuteRules(List<Guid> buildingIds, Guid user)
        {
            return await _ruleRepository.ExecuteRulesStoreProcedure(buildingIds, user);
        }
    }
}
