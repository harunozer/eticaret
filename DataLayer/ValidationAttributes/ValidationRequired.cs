using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.ValidationAttributes
{
    public class ValidationRequired : RequiredAttribute
    {
        public string Alias { get; set; }
        public ValidationRequired(string Alias_ = null)
        {
            this.Alias = Alias_;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //this.Alias = String.IsNullOrEmpty(this.Alias) ? validationContext.DisplayName : this.Alias;
            this.Alias = String.IsNullOrEmpty(this.Alias) ? "" : this.Alias;

            //int notnull ise 0 olamaz.
            //if (value == null || (validationContext.GetType() == typeof(int) && value.ToString() == "0"))
            if (value == null)
                return new ValidationResult("Zorunlu Alan " + this.Alias);

            return base.IsValid(value, validationContext);
        }

    }
}
