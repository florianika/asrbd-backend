
namespace Application.FieldWorkRule.AddFieldWorkRule.Request
{
    public class AddFieldWorkRuleRequest : FieldWorkRule.Request
    {
        public int FieldWorkId { get; set; }
        public long RuleId { get; set; }
        public Guid CreatedUser { get; set; }
    }
    //TODO add request validator class
}
