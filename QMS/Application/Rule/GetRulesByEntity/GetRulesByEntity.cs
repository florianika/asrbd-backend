
using Application.Common.Translators;
using Application.Ports;
using Application.Rule.GetRulesByEntity.Request;
using Application.Rule.GetRulesByEntity.Response;

namespace Application.Rule.GetRulesByEntity
{
    public class GetRulesByEntity : IGetRulesByEntity
    {
        private readonly IRuleRepository _ruleRepository;
        public GetRulesByEntity(IRuleRepository ruleRepository)
        {
            _ruleRepository = ruleRepository;
        }
        public async Task<GetRulesByEntityResponse> Execute(GetRulesByEntityRequest request)
        {
            var rules = await _ruleRepository.GetRulesByEntity(request.EntityType);

            return new GetRulesByEntitySuccessResponse
            {
                RulesDTO = Translator.ToDTOList(rules)
            };
        }
    }
}
