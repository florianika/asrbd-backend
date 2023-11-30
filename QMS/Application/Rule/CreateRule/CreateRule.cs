
using Application.Ports;
using Application.Rule.CreateRule.Request;
using Application.Rule.CreateRule.Response;
using Microsoft.Extensions.Logging;

namespace Application.Rule.CreateRule
{
    public class CreateRule : ICreateRule
    {
        private readonly ILogger _logger;
        private readonly IRuleRepository _ruleRepository;

        public CreateRule(ILogger<CreateRule> logger, IRuleRepository ruleRepository)
        {
            _logger = logger;
            _ruleRepository = ruleRepository;

        }
        public async Task<CreateRuleResponse> Execute(CreateRuleRequest request)
        {
            
            try
            {
               var rule = new Domain.Rule
               {
                  LocalId = request.LocalId,
                  EntityType = request.EntityType,
                  Variable = request.Variable,
                  NameAl = request.NameAl,
                  NameEn = request.NameEn,
                  DescriptionAl = request.DescriptionAl,
                  DescriptionEn = request.DescriptionEn,
                  Expression = request.Expression,
                  QualityAction = request.QualityAction,
                  RuleStatus = request.RuleStatus,
                  RuleRequirement = request.RuleRequirement,
                  Remark  = request.Remark,
                  QualityMessageAl = request.QualityMessageAl,
                  QualityMessageEn = request.QualityMessageEn,
                  CreatedUser = request.CreatedUser,
                  CreatedTimestamp = DateTime.Now,
                  UpdatedUser = null,
                  UpdatedTimestamp = null                   
               };
                var result = await _ruleRepository.CreateRule(rule);
                return new CreateRuleSuccessResponse
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
