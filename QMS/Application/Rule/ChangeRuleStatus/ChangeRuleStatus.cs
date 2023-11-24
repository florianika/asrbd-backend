
using Application.Ports;
using Application.Rule.ChangeRuleStatus.Request;
using Application.Rule.ChangeRuleStatus.Response;
using Domain.Enum;
using Microsoft.Extensions.Logging;

namespace Application.Rule.ChangeRuleStatus
{
    public class ChangeRuleStatus : IChangeRuleStatus
    {
        private readonly ILogger _logger;
        private readonly IRuleRepository _ruleRepository;
        public ChangeRuleStatus(ILogger<ChangeRuleStatus> logger,
            IRuleRepository ruleRepository)
        {
            _logger = logger;
            _ruleRepository = ruleRepository;
        }
        public async Task<ChangeRuleStatusResponse> Execute(ChangeRuleStatusRequest request)
        {
            try
            {
                var rule = await _ruleRepository.GetRule(request.Id);
                var newStatus = rule.RuleStatus == RuleStatus.ACTIVE ? RuleStatus.DISABLED : RuleStatus.ACTIVE;

                await _ruleRepository.ChangeRuleStatus(request.Id, newStatus, request.UpdatedUser);

                var responseMessage = rule.RuleStatus == RuleStatus.ACTIVE ? "Rule activated" : "Rule disabled";

                return new ChangeRuleStatusSuccessResponse
                {
                    Message = responseMessage
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

    }
}
