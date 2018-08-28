using System.ComponentModel.DataAnnotations;

namespace DataLayer.ValidationAttributes
{
    public class ValidationStringLength : StringLengthAttribute
    {
        public ValidationStringLength(int maximumLength, string alias = null) : base(maximumLength)
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (value.ToString().Length > MaximumLength)
                return new ValidationResult("Max " + MaximumLength + " karakter olmalıdır.");

            return ValidationResult.Success;

            //return base.IsValid(value, validationContext);
        }

    }
}
