using System.ComponentModel.DataAnnotations;

namespace DataLayer.ValidationAttributes
{
    public class ValidationPass : StringLengthAttribute
    {
        public ValidationPass() : base(20)
        {

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Şifre Zorunlu");

            if (value.ToString().Length < 5)
                return new ValidationResult("Şifre en az 5 karakter olmalıdır.");

            if (value.ToString().Length > 12)
                return new ValidationResult("Şifre en fazla 12 karakter olmalıdır.");

            //validationContext.ObjectInstance;

            return ValidationResult.Success;
        }
    }
}
