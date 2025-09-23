using Application.Ports;

namespace Application.Quality.RulesExecutor
{
    public class RulesExecutor
    {
        private readonly IRuleRepository _ruleRepository;

        public RulesExecutor(IRuleRepository ruleRepository) 
        {
            _ruleRepository = ruleRepository;
        }
        public async Task<bool> ExecuteRules(List<Guid> buildingIds, Guid user)
        {
            return await _ruleRepository.ExecuteRulesStoreProcedure(buildingIds, user);
        }
        public async Task<bool> ExecuteAutomaticRules(List<Guid> buildingIds, Guid user)
        {
            return await _ruleRepository.ExecuteAutomaticRulesStoreProcedure(buildingIds, user);
        }

        public async Task<bool> SetBldToUntested(List<Guid> buildingIds)
        {
            return await _ruleRepository.SetBldToUntestedStoreProcedure(buildingIds);
        }
    }
}
