
using Application.Rule.CreateRule.Request;
using Application.Rule.CreateRule.Response;

namespace Application.Rule.CreateRule
{
    public interface ICreateRule : IRule<CreateRuleRequest, CreateRuleResponse>
    {
    }
}
