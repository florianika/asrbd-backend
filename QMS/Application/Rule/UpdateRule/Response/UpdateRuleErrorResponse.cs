﻿
namespace Application.Rule.UpdateRule.Response
{
    public class UpdateRuleErrorResponse : UpdateRuleResponse
    {
        public string Message { get; internal set; }
        public string Code { get; internal set; }
    }
}
