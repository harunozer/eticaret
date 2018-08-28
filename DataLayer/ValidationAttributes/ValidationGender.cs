using System.ComponentModel.DataAnnotations;

namespace DataLayer.ValidationAttributes
{
    public class ValidationGender:ValidationAttribute
    {
        public ValidationGender()
        {

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (value.ToString().ToUpper() != "E" && value.ToString().ToUpper() != "K")
                return new ValidationResult("Hatalı Cinsiyet formatı.");

            return ValidationResult.Success;
        }
    }
}
