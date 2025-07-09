using Application.Common.Translators;
using Application.Ports;
using Application.Rule.GetRulesByEntityAndStatus.Request;
using Application.Rule.GetRulesByEntityAndStatus.Response;
using Application.Rule.GetRulesByVariableAndEntity.Response;

namespace Application.Rule.GetRulesByEntityAndStatus
{
    public class GetRulesByEntityAndStatus : IGetRulesByEntityAndStatus
    {
        private readonly IRuleRepository _ruleRepository;
        public GetRulesByEntityAndStatus (IRuleRepository ruleRepository)
        {
            _ruleRepository = ruleRepository;
        }

        public async Task<GetRulesByEntityAndStatusResponse> Execute(GetRulesByEntityAndStatusRequest request)
        {
            var rules = await _ruleRepository.GetRulesByEntityAndStatus(request.EntityType, request.RuleStatus);

            return new GetRulesByEntityAndStatusSuccessResponse
            {
                RulesDTO = Translator.ToDTOList(rules)
            };
        }
    }
}
