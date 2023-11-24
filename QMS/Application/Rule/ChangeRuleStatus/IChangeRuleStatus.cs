
using Application.Rule.ChangeRuleStatus.Request;
using Application.Rule.ChangeRuleStatus.Response;

namespace Application.Rule.ChangeRuleStatus
{
    public interface IChangeRuleStatus : IRule<ChangeRuleStatusRequest, ChangeRuleStatusResponse>
    {
    }
}
