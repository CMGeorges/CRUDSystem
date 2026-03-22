namespace CRUDSystem.Application.Validation
{
    public class ValidationResult
    {
        public bool IsValid { get; private set; }

        public string ErrorMessage { get; private set; }

        public static ValidationResult Success()
        {
            return new ValidationResult
            {
                IsValid = true
            };
        }

        public static ValidationResult Failure(string errorMessage)
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = errorMessage
            };
        }
    }
}
