
using Application.Rule.UpdateRule.Request;
using Application.Rule.UpdateRule.Response;

namespace Application.Rule.UpdateRule
{
    public interface IUpdateRule : IRule<UpdateRuleRequest, UpdateRuleResponse>
    {
    }
}
