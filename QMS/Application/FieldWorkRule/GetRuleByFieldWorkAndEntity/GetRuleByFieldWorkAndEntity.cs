
using Application.Common.Translators;
using Application.FieldWorkRule.GetRuleByFieldWork.Request;
using Application.FieldWorkRule.GetRuleByFieldWork.Response;
using Application.FieldWorkRule.GetRuleByFieldWorkAndEntity.Request;
using Application.FieldWorkRule.GetRuleByFieldWorkAndEntity.Response;
using Application.Ports;

namespace Application.FieldWorkRule.GetRuleByFieldWorkAndEntity
{
    public class GetRuleByFieldWorkAndEntity : IGetRuleByFieldWorkAndEntity
    {
        private readonly IFieldWorkRuleRepository _fieldWorkRuleRepository;
        public GetRuleByFieldWorkAndEntity(IFieldWorkRuleRepository fieldWorkRuleRepository)
        {
            _fieldWorkRuleRepository = fieldWorkRuleRepository;
        }

        public async Task<GetRuleByFieldWorkAndEntityResponse> Execute(GetRuleByFieldWorkAndEntityRequest request)
        {
            var fieldWorkRules = await _fieldWorkRuleRepository.GetRuleByFieldWorkAndEntity(request.Id, request.EntityType);

            return new GetRuleByFieldWorkAndEntitySuccessResponse
            {
                FieldworkRulesDTO = Translator.ToDTOList(fieldWorkRules)
            };
        }
    }
}
