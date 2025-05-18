using Application.Ports;
using Application.Rule.UpdateRule.Request;
using Application.Rule.UpdateRule.Response;
using Microsoft.Extensions.Logging;

namespace Application.Rule.UpdateRule
{
    public class UpdateRule : IUpdateRule
    {
        private readonly ILogger _logger;
        private readonly IRuleRepository _ruleRepository;
        public UpdateRule(ILogger<UpdateRule> logger, IRuleRepository ruleRepository)
        {
            _logger = logger;
            _ruleRepository = ruleRepository;
        }
        public async Task<UpdateRuleResponse> Execute(UpdateRuleRequest request)
        {
            var rule = await _ruleRepository.GetRule(request.Id);
            rule.LocalId = request.LocalId;
            rule.EntityType = request.EntityType;
            rule.Variable = request.Variable;
            rule.NameAl = request.NameAl;
            rule.NameEn = request.NameEn;
            rule.DescriptionAl = request.DescriptionAl;
            rule.DescriptionEn = request.DescriptionEn;
            rule.Version = request.Version;
            rule.VersionRationale = request.VersionRationale;
            rule.Expression = request.Expression;
            rule.QualityAction = request.QualityAction;
            rule.RuleRequirement = request.RuleRequirement;
            rule.Remark = request.Remark;
            rule.QualityMessageAl = request.QualityMessageAl;
            rule.QualityMessageEn = request.QualityMessageEn;
            rule.UpdatedUser = request.UpdatedUser;
            rule.UpdatedTimestamp = DateTime.Now;

            await _ruleRepository.UpdateRule(rule);

            return new UpdateRuleSuccessResponse
            {
                Message = "Rule updated"
            };
        }
    }
}
