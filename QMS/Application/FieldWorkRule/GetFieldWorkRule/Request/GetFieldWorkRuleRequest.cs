
namespace Application.FieldWorkRule.GetFieldWorkRule.Request
{
    public class GetFieldWorkRuleRequest : FieldWorkRule.Request
    {
        public int Id { get; set; }
        public long RuleId { get; set; } // This is the ID of the rule to be retrieved for the specified field work.
    }
}
