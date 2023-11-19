using Application.Rule;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Ports
{
    public interface IRuleRepository
    {
        Task ChangeRuleStatus(long id, RuleStatus dISABLED);
        Task<long> CreateRule(Domain.Rule rule);
        Task<List<Domain.Rule>> GetAllRules();
        Task<Domain.Rule> GetRule(long id);
        Task<List<Domain.Rule>> GetRulesByEntity(EntityType entityType);
        Task<List<Domain.Rule>> GetRulesByQualityAction(QualityAction qualityAction);
        Task<List<Domain.Rule>> GetRulesByVariableAndEntity(string variable, EntityType entityType);

    }
}
