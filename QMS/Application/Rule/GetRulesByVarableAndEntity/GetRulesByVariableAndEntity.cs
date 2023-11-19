

using Application.Common.Translators;
using Application.Ports;
using Application.Rule.GetRulesByVariableAndEntity.Request;
using Application.Rule.GetRulesByVariableAndEntity.Response;

namespace Application.Rule.GetRulesByVariableAndEntity
{
    public class GetRulesByVariableAndEntity : IGetRulesByVariableAndEntity
    {
        private readonly IRuleRepository _ruleRepository;
        public GetRulesByVariableAndEntity(IRuleRepository ruleRepository)
        {
            _ruleRepository = ruleRepository;
        }
        public async Task<GetRulesByVariableAndEntityResponse> Execute(GetRulesByVariableAndEntityRequest request)
        {
            var rules = await _ruleRepository.GetRulesByVariableAndEntity(request.Variable, request.EntityType);

            return new GetRulesByVariableAndEntitySuccessResponse
            {
                RulesDTO = Translator.ToDTOList(rules)
            };
        }
    }
}
