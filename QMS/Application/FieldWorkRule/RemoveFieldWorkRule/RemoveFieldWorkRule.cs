
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
            var fieldWorkRule = await _fieldWorkRuleRepository.GetFieldWorkRule(request.Id);
            //TODO remove this, this code is not reachable as it will rise a NotFoundException in the repository
            //TODO Do this for all the remaining if any
            //TODO no need for Error response usually we return the exception
            /*if (fieldwWorkRule == null)
            {
                return new RemoveFieldWorkRuleErrorResponse { Code = "404", Message = "Record not found" };
            }*/
            await _fieldWorkRuleRepository.RemoveFieldWorkRule(fieldWorkRule);
            return new RemoveFieldWorkRuleSuccessResponse
            {
                Message = "Record deleted"
            };
        }
    }
}
