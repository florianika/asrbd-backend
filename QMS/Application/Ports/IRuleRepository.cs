﻿using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Ports
{
    public interface IRuleRepository
    {
        Task<long> CreateRule(Domain.Rule rule);
        Task<List<Domain.Rule>> GetAllRules();
        Task<List<Domain.Rule>> GetRulesByVarableAndEntity(string variable, EntityType entityType);

    }
}
