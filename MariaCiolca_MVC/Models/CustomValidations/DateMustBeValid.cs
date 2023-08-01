using System.ComponentModel.DataAnnotations;

namespace MariaCiolca_MVC.Models.CustomValidations
{
    public class DateMustBeValid : ValidationAttribute
    {
        private readonly string _validFrom;

        public DateMustBeValid(string validFrom) 
        { 
        _validFrom = validFrom;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) 
        {
            var validFromProperty = validationContext.ObjectType.GetProperty(_validFrom);

            var validFromValue = validFromProperty.GetValue(validationContext.ObjectInstance, null);

            var validTo = value; 

            //Convertim cele 2 la DateTime 
            //IS returneaza true sau false daca poate face conversia

            if(validFromValue is DateTime validFromDate && validTo is DateTime validToDate )
            {
                if (validFromDate > validToDate)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success; 
        }

    }
}
