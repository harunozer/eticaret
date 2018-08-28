using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DataLayer.ValidationAttributes
{
    public class ValidationGsm : ValidationAttribute
    {
        public ValidationGsm()
        {

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (!Regex.IsMatch("545-5766266", @"(5)(\d{2})-(\d{3})\s{1}(\d{4})$"))
                return new ValidationResult("Hatalı Gsm formatı. Örn. 5XX-123 4567");
            
            return ValidationResult.Success;
        }
    }
}
