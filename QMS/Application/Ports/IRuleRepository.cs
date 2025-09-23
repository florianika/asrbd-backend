using Domain.Enum;

namespace Application.Ports
{
    public interface IRuleRepository
    {
        Task ChangeRuleStatus(long id, RuleStatus disabled, Guid updatedUser);
        Task<long> CreateRule(Domain.Rule rule);
        Task<List<Domain.Rule>> GetAllRules();
        Task<Domain.Rule> GetRule(long id);
        Task<List<Domain.Rule>> GetRulesByEntity(EntityType entityType);
        Task<List<Domain.Rule>> GetActiveRulesByEntity(EntityType entityType);
        Task<List<Domain.Rule>> GetRulesByQualityAction(QualityAction qualityAction);
        Task<List<Domain.Rule>> GetRulesByVariableAndEntity(string variable, EntityType entityType);
        Task UpdateRule(Domain.Rule rule);
        Task<bool> ExecuteRulesStoreProcedure(List<Guid> buildingIds, Guid createdUser);
        Task<bool> ExecuteAutomaticRulesStoreProcedure(List<Guid> buildingIds, Guid createdUser);
        Task<List<Domain.Rule>> GetActiveRules();
        Task<bool> ExistsRule(long ruleId);
        Task<List<Domain.Rule>> GetRulesByEntityAndStatus(EntityType entityType, RuleStatus ruleStatus);
        Task<bool> SetBldToUntestedStoreProcedure(List<Guid> buildingIds);
    }
}
