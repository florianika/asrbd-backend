
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Common.Validations
{
    public class ValidEnumValueAttribute : ValidationAttribute
    {
        private readonly Type _enumType;
        public ValidEnumValueAttribute(Type enumType)
        {
            _enumType = enumType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!Enum.IsDefined(_enumType, value))
            {
                return new ValidationResult($"Invalid {_enumType.Name} value.");
            }

            return ValidationResult.Success;
        }

    }
}
