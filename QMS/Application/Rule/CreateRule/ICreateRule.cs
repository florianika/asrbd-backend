
using Application.Rule.CreateRule.Request;
using Application.Rule.CreateRule.Response;
using Domain;

namespace Application.Rule.CreateRule
{
    public interface ICreateRule //: IRule<CreateRuleRequest, CreateRuleResponse>
    {
        public Task<CreateRuleResponse> Execute(Domain.Rule rule);
    }
}
