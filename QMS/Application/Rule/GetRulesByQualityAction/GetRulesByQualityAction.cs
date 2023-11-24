
using Application.Common.Translators;
using Application.Ports;
using Application.Rule.GetRulesByQualityAction.Request;
using Application.Rule.GetRulesByQualityAction.Response;

namespace Application.Rule.GetRulesByQualityAction
{
    public class GetRulesByQualityAction: IGetRulesByQualityAction
    {
        private readonly IRuleRepository _ruleRepository;
        public GetRulesByQualityAction(IRuleRepository ruleRepository)
        {
            _ruleRepository = ruleRepository;
        }
        public async Task<GetRulesByQualityActionResponse> Execute(GetRulesByQualityActionRequest request)
        {
            var rules = await _ruleRepository.GetRulesByQualityAction(request.QualityAction);

            return new GetRulesByQualityActionSuccessResponse
            {
                RulesDTO = Translator.ToDTOList(rules)
            };
        }
    }
}
