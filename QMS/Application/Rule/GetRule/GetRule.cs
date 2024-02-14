
using Application.Common.Translators;
using Application.Ports;
using Application.Rule.GetRule.Request;
using Application.Rule.GetRule.Response;

namespace Application.Rule.GetRule
{
    public class GetRule : IGetRule
    {
        private readonly IRuleRepository _ruleRepository;
        public GetRule(IRuleRepository ruleRepository)
        {
            _ruleRepository = ruleRepository;
        }
        public async Task<GetRuleResponse> Execute(GetRuleRequest request)
        {
            var rule = await _ruleRepository.GetRule(request.Id);
            var ruleDto = Translator.ToDTO(rule);
            return new GetRuleSuccessResponse
            {
                RulesDTO = ruleDto
            };
        }
    }
}
