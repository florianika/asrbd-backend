namespace Application.Ports
{
    public interface IFieldWorkRuleRepository
    {
        Task<long> AddFieldWorkRule(Domain.FieldWorkRule fieldworkrule);
    }
}
