using CRUDSystem.Entities;

namespace CRUDSystem.Application.Validation
{
    public static class DetailValidator
    {
        public static ValidationResult Validate(Detail detail)
        {
            if (detail == null)
            {
                return ValidationResult.Failure("Detail is required.");
            }

            if (string.IsNullOrWhiteSpace(detail.Fname))
            {
                return ValidationResult.Failure("First name is required.");
            }

            if (string.IsNullOrWhiteSpace(detail.Lname))
            {
                return ValidationResult.Failure("Last name is required.");
            }

            if (detail.Age < 0)
            {
                return ValidationResult.Failure("Age must be a positive number.");
            }

            return ValidationResult.Success();
        }
    }
}
