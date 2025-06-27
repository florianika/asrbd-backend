
using Application.FieldWorkRule.AddFieldWorkRule.Request;
using Application.FieldWorkRule.AddFieldWorkRule.Response;
using Application.Ports;
using Application.Rule;
using Domain;
using Microsoft.Extensions.Logging;

namespace Application.FieldWorkRule.AddFieldWorkRule
{
    public class AddFieldWorkRule : IAddFieldWorkRule
    {
        private readonly ILogger<AddFieldWorkRule> _logger;
        private readonly IFieldWorkRuleRepository _fieldWorkRuleRepository;
        private readonly IRuleRepository _ruleRepository;
        public AddFieldWorkRule(IFieldWorkRuleRepository fieldWorkRuleRepository, ILogger<AddFieldWorkRule> logger, IRuleRepository ruleRepository)
        {
            _fieldWorkRuleRepository = fieldWorkRuleRepository;
            _logger = logger;
            _ruleRepository = ruleRepository;
        }
        public async Task<AddFieldWorkRuleResponse> Execute(AddFieldWorkRuleRequest request)
        {
            try
            {
                // Check if the rule already exists for the given field work
                if (await _fieldWorkRuleRepository.ExistsRule(request.FieldWorkId, request.RuleId))
                {
                    throw new InvalidOperationException($"Rule already exists.");
                }
                //check if RuleId exists
                if(!await _ruleRepository.ExistsRule(request.RuleId))
                {
                    throw new InvalidOperationException($"Id Rule {request.RuleId} does not exist.");
                }
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
