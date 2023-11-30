

using Domain.Enum;

namespace Application.Rule.GetRulesByVariableAndEntity.Request
{
    #nullable disable
    public class GetRulesByVariableAndEntityRequest : Rule.Request
    {
        public string Variable { get; set; }
        public EntityType EntityType { get; set; }
    }
}
