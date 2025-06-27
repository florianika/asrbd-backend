using Application.Common.Translators;
using Application.Ports;
using Application.Rule.GetActiveRules.Response;

namespace Application.Rule.GetActiveRules
{
    public class GetActiveRules : IGetActiveRules
    {
        private readonly IRuleRepository _ruleRepository;
        public GetActiveRules(IRuleRepository ruleRepository)
        {
            _ruleRepository = ruleRepository;
        }
        public async Task<GetActiveRulesResponse> Execute()
        {
            var rules = await _ruleRepository.GetAllRules();
            return new GetActiveRulesSuccessResponse
            {
                ShortRulesDTO = Translator.ToDTOShortRuleList(rules)
            };
        }
    }
}
