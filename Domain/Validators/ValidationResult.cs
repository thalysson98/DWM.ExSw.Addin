namespace DWM.ExSw.Addin.Base.Validation
{
    public class ValidationResult
    {
        public bool IsValid { get; private set; }
        public string Message { get; private set; }

        private ValidationResult(bool isValid, string message)
        {
            IsValid = isValid;
            Message = message;
        }

        public static ValidationResult Success()
            => new ValidationResult(true, string.Empty);

        public static ValidationResult Fail(string message)
            => new ValidationResult(false, message);
    }
}
