using System.ComponentModel.DataAnnotations;

namespace EventEase.Models;

public sealed class FutureOrTodayDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not DateTime date)
        {
            return ValidationResult.Success;
        }

        if (date.Date < DateTime.Today)
        {
            return new ValidationResult("Event date must be today or in the future.");
        }

        return ValidationResult.Success;
    }
}
