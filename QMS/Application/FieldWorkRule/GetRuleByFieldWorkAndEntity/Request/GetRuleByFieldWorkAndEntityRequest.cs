
using Domain.Enum;

namespace Application.FieldWorkRule.GetRuleByFieldWorkAndEntity.Request
{
    public class GetRuleByFieldWorkAndEntityRequest : FieldWorkRule.Request
    {
        public int Id { get; set; }
        public EntityType EntityType { get; set; }
    }
}
