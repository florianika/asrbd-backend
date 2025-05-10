namespace Application.Ports
{
    public interface IFieldWorkRuleRepository
    {
        Task<long> AddFieldWorkRule(Domain.FieldWorkRule fieldworkrule);
        Task<Domain.FieldWorkRule> GetFieldWorkRule(long id);
        Task RemoveFieldWorkRule(Domain.FieldWorkRule fieldWorkRule);
    }
}
