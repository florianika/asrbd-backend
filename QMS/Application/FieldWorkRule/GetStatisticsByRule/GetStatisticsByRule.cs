
using Application.FieldWorkRule.GetStatisticsByRule.Request;
using Application.FieldWorkRule.GetStatisticsByRule.Response;
using Application.Ports;

namespace Application.FieldWorkRule.GetStatisticsByRule
{
    public class GetStatisticsByRule : IGetStatisticsByRule
    {
        private readonly IFieldWorkRuleRepository _fieldWorkRuleRepository;
        public GetStatisticsByRule(IFieldWorkRuleRepository fieldWorkRuleRepository)
        {
            _fieldWorkRuleRepository = fieldWorkRuleRepository;
        }

        public async Task<GetStatisticsByRuleResponse> Execute(GetStatisticsByRuleRequest request)
        {
            long count = await _fieldWorkRuleRepository.GetStatisticsByRule(request.Id);
            return new GetStatisticsByRuleSuccessResponse
            {
                Count = count
            };
        }
    }
}
