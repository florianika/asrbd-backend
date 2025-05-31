namespace Application.FieldWorkRule.RemoveFieldWorkRule.Request
{
    public class RemoveFieldWorkRuleRequest : FieldWorkRule.Request
    {
        public int Id { get; set; } // This is the ID of the field work to which the rule belongs.
        public long RuleId { get; set; }
    }    
}
