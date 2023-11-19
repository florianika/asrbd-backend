

using Application.Common.Translators;
using Application.Ports;
using Application.Rule.GetRulesByVarableAndEntity.Request;
using Application.Rule.GetRulesByVarableAndEntity.Response;

namespace Application.Rule.GetRulesByVarableAndEntity
{
    public class GetRulesByVarableAndEntity : IGetRulesByVarableAndEntity
    {
        private readonly IRuleRepository _ruleRepository;
        public GetRulesByVarableAndEntity(IRuleRepository ruleRepository)
        {
            _ruleRepository = ruleRepository;
        }
        public async Task<GetRulesByVarableAndEntityResponse> Execute(GetRulesByVarableAndEntityRequest request)
        {
            var rules = await _ruleRepository.GetRulesByVarableAndEntity(request.Variable, request.EntityType);

            return new GetRulesByVarableAndEntitySuccessResponse
            {
                RulesDTO = Translator.ToDTOList(rules)
            };
        }
    }
}
