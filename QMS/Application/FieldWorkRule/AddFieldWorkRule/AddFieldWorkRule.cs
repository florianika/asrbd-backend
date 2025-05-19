
using Application.FieldWorkRule.AddFieldWorkRule.Request;
using Application.FieldWorkRule.AddFieldWorkRule.Response;
using Application.Ports;
using Domain;
using Microsoft.Extensions.Logging;

namespace Application.FieldWorkRule.AddFieldWorkRule
{
    public class AddFieldWorkRule : IAddFieldWorkRule
    {
        private readonly ILogger<AddFieldWorkRule> _logger;
        private readonly IFieldWorkRuleRepository _fieldWorkRuleRepository;
        public AddFieldWorkRule(IFieldWorkRuleRepository fieldWorkRuleRepository, ILogger<AddFieldWorkRule> logger)
        {
            _fieldWorkRuleRepository = fieldWorkRuleRepository;
            _logger = logger;
        }
        public async Task<AddFieldWorkRuleResponse> Execute(AddFieldWorkRuleRequest request)
        {
            try
            {
                var fieldWorkRule = new Domain.FieldWorkRule()
                {
                    RuleId = request.RuleId,
                    FieldWorkId = request.FieldWorkId,
                    CreatedUser = request.CreatedUser,
                    CreatedTimestamp = DateTime.Now,
                };
                var result = await _fieldWorkRuleRepository.AddFieldWorkRule(fieldWorkRule);
                return new AddFieldWorkRuleSuccessResponse
                {
                    Id = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
