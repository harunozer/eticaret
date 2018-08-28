using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DataLayer.ValidationAttributes
{
    public class ValidationEmail : ValidationAttribute
    {
        public ValidationEmail()
        {

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (!Regex.IsMatch(value.ToString(), @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                return new ValidationResult("Hatalı E-Mail formatı.");

            return ValidationResult.Success;
        }
    }
}
