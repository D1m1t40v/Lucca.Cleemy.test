using System.ComponentModel.DataAnnotations;

namespace ExpenseAPI.Models.Validators
{
    public class DateValidationRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // we don't work with hours/minutes/seconds so wee keep only value.Date and use DateTime.Today

            DateTime date = ((DateTime)value).Date;

            if (date > DateTime.Today)
            {
                return new ValidationResult("Expense cannot be in the future");
            }
            if (date <= DateTime.Today.AddMonths(-3))
            {
                return new ValidationResult("Expense cannot be older than 3 months");
            }

            return ValidationResult.Success;
        }
    }  
}
