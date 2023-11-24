
using Domain.Enum;

namespace Application.Rule.GetRulesByEntity.Request
{
    public class GetRulesByEntityRequest : Rule.Request
    {
        public EntityType EntityType { get; set; }  
    }
}
