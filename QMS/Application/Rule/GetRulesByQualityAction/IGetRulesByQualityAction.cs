
using Application.Rule.GetRulesByQualityAction.Request;
using Application.Rule.GetRulesByQualityAction.Response;

namespace Application.Rule.GetRulesByQualityAction
{
    public interface IGetRulesByQualityAction : Rule.IRule<GetRulesByQualityActionRequest, GetRulesByQualityActionResponse>
    {
    }
}
