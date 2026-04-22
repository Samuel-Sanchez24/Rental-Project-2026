using FluentValidation.Results;

namespace Rental_Project_2026.Application.Exceptions
{
    public class CustomValidationException : Exception
    {
        public List<string> Errors { get; set; } = [];

        public CustomValidationException(ValidationResult validationResult)
        {
            Errors.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        public CustomValidationException(string errorMessage)
        {
            Errors.Add(errorMessage);
        }
    }
}
