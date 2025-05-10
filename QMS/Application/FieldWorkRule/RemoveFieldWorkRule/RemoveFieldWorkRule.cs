
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
            var fieldworkrule = await _fieldWorkRuleRepository.GetFieldWorkRule(request.Id);
            if (fieldworkrule == null)
            {
                return new RemoveFieldWorkRuleErrorResponse { Code = "404", Message = "Record not found" };
            }
            await _fieldWorkRuleRepository.RemoveFieldWorkRule(fieldworkrule);
            return new RemoveFieldWorkRuleSuccessResponse
            {
                Message = "Record deleted"
            };
        }
    }
}
