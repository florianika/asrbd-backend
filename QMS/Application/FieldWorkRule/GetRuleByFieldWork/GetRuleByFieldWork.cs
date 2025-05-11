
using Application.Common.Translators;
using Application.FieldWorkRule.GetRuleByFieldWork.Request;
using Application.FieldWorkRule.GetRuleByFieldWork.Response;
using Application.Ports;
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingIdAndStatus.Response;

namespace Application.FieldWorkRule.GetRuleByFieldWork
{
    public class GetRuleByFieldWork : IGetRuleByFieldWork
    {
        private readonly IFieldWorkRuleRepository _fieldWorkRuleRepository;
        
        public GetRuleByFieldWork(IFieldWorkRuleRepository fieldWorkRuleRepository)
        {
            _fieldWorkRuleRepository = fieldWorkRuleRepository;
        }
        public async Task<GetRuleByFieldWorkResponse> Execute(GetRuleByFieldWorkRequest request)
        {
            var fieldWorkRules = await _fieldWorkRuleRepository.GetRuleByField(request.Id);

            return new GetRuleByFieldWorkSuccessResponse
            {
                FieldworkRulesDTO = Translator.ToDTOList(fieldWorkRules)
            };
        }
    }
}
