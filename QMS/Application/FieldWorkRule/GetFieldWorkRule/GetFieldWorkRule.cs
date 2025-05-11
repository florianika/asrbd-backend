
using Application.Common.Translators;
using Application.FieldWorkRule.GetFieldWorkRule.Request;
using Application.FieldWorkRule.GetFieldWorkRule.Response;
using Application.Ports;

namespace Application.FieldWorkRule.GetFieldWorkRule
{
    public class GetFieldWorkRule : IGetFieldWorkRule
    {
        private readonly IFieldWorkRuleRepository _fieldWorkRuleRepository;
        public GetFieldWorkRule(IFieldWorkRuleRepository fieldWorkRuleRepository)
        {
            _fieldWorkRuleRepository = fieldWorkRuleRepository;
        }
        public async Task<GetFieldWorkRuleResponse> Execute(GetFieldWorkRuleRequest request)
        {
            var fieldWorkRules = await _fieldWorkRuleRepository.GetFieldWorkRule(request.Id);
            var fieldworkruleDto = Translator.ToDTO(fieldWorkRules);
            return new GetFieldWorkRuleSuccessResponse
            {
                FieldWorkRuleDTO = fieldworkruleDto
            };
        }
    }
}
