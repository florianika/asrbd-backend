using Application.Rule.GetRule.Request;
using Application.Rule.GetRule.Response;
using Application.Rule.GetRulesByEntity.Request;

namespace Application.Rule.GetRule
{
    public interface IGetRule: IRule<GetRuleRequest, GetRuleResponse>
    {
    }
}
