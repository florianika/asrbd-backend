using Domain.Enum;

namespace Application.Ports
{
    public interface IRuleRepository
    {
        Task ChangeRuleStatus(long id, RuleStatus Disabled, Guid UpdatedUser);
        Task<long> CreateRule(Domain.Rule rule);
        Task<List<Domain.Rule>> GetAllRules();
        Task<Domain.Rule> GetRule(long id);
        Task<List<Domain.Rule>> GetRulesByEntity(EntityType entityType);
        Task<List<Domain.Rule>> GetActiveRulesByEntity(EntityType entityType);
        Task<List<Domain.Rule>> GetRulesByQualityAction(QualityAction qualityAction);
        Task<List<Domain.Rule>> GetRulesByVariableAndEntity(string variable, EntityType entityType);
        Task UpdateRule(Domain.Rule rule);

        Task<bool> ExecuteRulesStoreProcedure(List<Guid> buildingIds, Guid CreatedUser);
        Task<bool> UpdateEntitiesStoredProcedure(List<Guid> buildingIds, Guid CreatedUser);
        
    }
}
