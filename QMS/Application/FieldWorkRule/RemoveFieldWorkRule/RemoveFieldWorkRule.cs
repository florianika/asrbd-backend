
using Application.FieldWorkRule.RemoveFieldWorkRule.Request;
using Application.FieldWorkRule.RemoveFieldWorkRule.Response;
using Application.Note.DeleteNote.Response;
using Application.Ports;

namespace Application.FieldWorkRule.RemoveFieldWorkRule
{
    public class RemoveFieldWorkRule : IRemoveFieldWorkRule
    {
        private readonly IFieldWorkRuleRepository _fieldWorkRuleRepository;
        public RemoveFieldWorkRule(IFieldWorkRuleRepository fieldWorkRuleRepository)
        {
            _fieldWorkRuleRepository = fieldWorkRuleRepository;
        }
        public async Task<RemoveFieldWorkRuleResponse> Execute(RemoveFieldWorkRuleRequest request)
        {
            var fieldWorkRule = await _fieldWorkRuleRepository.GetFieldWorkRule(request.Id, request.RuleId);
            
            await _fieldWorkRuleRepository.RemoveFieldWorkRule(fieldWorkRule);
            return new RemoveFieldWorkRuleSuccessResponse
            {
                Message = "Record deleted"
            };
        }
    }
}
