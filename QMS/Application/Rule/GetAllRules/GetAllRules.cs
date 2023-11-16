
using Application.Common.Translators;
using Application.Ports;
using Application.Rule.GetAllRules.Response;

namespace Application.Rule.GetAllRules
{
    public class GetAllRules : IGetAllRules
    {
        private readonly IRuleRepository _ruleRepository;
        public GetAllRules(IRuleRepository ruleRepository)
        {
            _ruleRepository = ruleRepository;
        }
        public async Task<GetAllRulesResponse> Execute()
        {
            var rules = await _ruleRepository.GetAllRules();
            return new GetAllRulesSuccessResponse
            {
                RulesDTO = Translator.ToDTOList(rules)
            };
        }
    }
}
