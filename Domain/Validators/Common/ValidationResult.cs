public class ValidationResult<T>
{
    public T FinalValue { get; set; }
    public ValidationErrorType Error { get; set; }

    public bool IsValid => Error == ValidationErrorType.None;
}
