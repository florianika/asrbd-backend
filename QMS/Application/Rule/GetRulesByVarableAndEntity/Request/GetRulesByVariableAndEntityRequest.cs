

using Domain.Enum;

namespace Application.Rule.GetRulesByVariableAndEntity.Request
{
    public class GetRulesByVariableAndEntityRequest : Rule.Request
    {
        public string Variable { get; set; }
        public EntityType EntityType { get; set; }
    }
}
