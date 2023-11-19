

using Domain.Enum;

namespace Application.Rule.GetRulesByVarableAndEntity.Request
{
    public class GetRulesByVarableAndEntityRequest : Rule.Request
    {
        public string Variable { get; set; }
        public EntityType EntityType { get; set; }
    }
}
