namespace Application.Ports
{
    public interface IFieldWorkRuleRepository
    {
        Task<long> AddFieldWorkRule(Domain.FieldWorkRule fieldworkrule);
        Task<Domain.FieldWorkRule> GetFieldWorkRule(int id, long ruleId);
        Task RemoveFieldWorkRule(Domain.FieldWorkRule fieldWorkRule);
        Task<List<Domain.FieldWorkRule>> GetRuleByField(int id);
        Task<long> GetStatisticsByRule(long id);
    }
}
