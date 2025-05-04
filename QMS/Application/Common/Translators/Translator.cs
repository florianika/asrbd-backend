
using Application.FieldWork;
using Application.ProcessOutputLog;
using Application.Rule;
using Domain.Enum;
using System.Linq.Expressions;

namespace Application.Common.Translators
{
    public static class Translator
    {
        public static List<RuleDTO> ToDTOList(List<Domain.Rule> rule)
        {
            List<RuleDTO> rules = new();
            rule.ForEach((rule) => {
                rules.Add(ToDTO(rule));
            });
            return rules;
        }

        public static RuleDTO ToDTO(Domain.Rule rule)
        {
            return new RuleDTO()
            {
                Id = rule.Id,
                LocalId = rule.LocalId,
                EntityType = rule.EntityType,
                Variable = rule.Variable,
                NameAl = rule.NameAl,
                NameEn = rule.NameEn,
                DescriptionAl = rule.DescriptionAl,
                DescriptionEn = rule.DescriptionEn,
                Version = rule.Version,
                VersionRationale = rule.VersionRationale,
                Expression = rule.Expression,
                QualityAction = rule.QualityAction,
                RuleStatus = rule.RuleStatus,
                RuleRequirement = rule.RuleRequirement,
                Remark = rule.Remark,
                QualityMessageAl = rule.QualityMessageAl,
                QualityMessageEn = rule.QualityMessageEn,
                CreatedUser = rule.CreatedUser,
                CreatedTimestamp = rule.CreatedTimestamp,
                UpdatedUser = rule.UpdatedUser,
                UpdatedTimestamp = rule.UpdatedTimestamp
            };
        }

        public static List<ProcessOutputLogDTO> ToDTOList(List<Domain.ProcessOutputLog> processOutputLog)
        {
            List<ProcessOutputLogDTO> processOutputLogs = new();
            processOutputLog.ForEach((processOutputLog) => {
                processOutputLogs.Add(ToDTO(processOutputLog));
            });
            return processOutputLogs;
        }

        public static ProcessOutputLogDTO ToDTO(Domain.ProcessOutputLog processOutputLog)
        {
            return new ProcessOutputLogDTO()
            {
                Id = processOutputLog.Id,
                RuleId = processOutputLog.RuleId,
                Reference = processOutputLog.Rule.LocalId,
                BldId = processOutputLog.BldId,
                EntId = processOutputLog.EntId,
                DwlId = processOutputLog.DwlId,
                EntityType = processOutputLog.EntityType,
                Variable = processOutputLog.Variable,
                QualityAction = processOutputLog.QualityAction,
                QualityStatus = processOutputLog.QualityStatus,
                QualityMessageAl = processOutputLog.QualityMessageAl,
                QualityMessageEn = processOutputLog.QualityMessageEn,
                ErrorLevel = processOutputLog.ErrorLevel,
                CreatedUser = processOutputLog.CreatedUser,
                CreatedTimestamp = processOutputLog.CreatedTimestamp
            };
        }

        public static List<FieldWorkDTO> ToDTOList(List<Domain.FieldWork> fieldwork)
        {
            List<FieldWorkDTO> fieldworks = new();
            fieldwork.ForEach((fieldwork) => {
                fieldworks.Add(ToDTO(fieldwork));
            });
            return fieldworks;
        }
        public static FieldWorkDTO ToDTO(Domain.FieldWork fieldWork)
        {
            return new FieldWorkDTO()
            {
                FieldWorkId = fieldWork.FieldWorkId,
                StartDate = fieldWork.StartDate,
                EndDate = fieldWork.EndDate,
                fieldWorkStatus = fieldWork.fieldWorkStatus,
                Description = fieldWork.Description,
                EmailTemplateId = fieldWork.EmailTemplateId,
                Remarks = fieldWork.Remarks,
                CreatedUser = fieldWork.CreatedUser,
                CreatedTimestamp = fieldWork.CreatedTimestamp,
                UpdatedUser = fieldWork.UpdatedUser,
                UpdatedTimestamp = fieldWork.UpdatedTimestamp
            };
        }
    }
}
