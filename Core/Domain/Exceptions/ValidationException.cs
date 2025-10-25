namespace Domain.Exceptions
{
    public sealed class ValidationException(IEnumerable<string> errors) : Exception("Validation failed")
    {
        public IEnumerable<string> Errors { get; set; } = errors;

        //public ValidationException(IEnumerable<string> errors) : base("Validation failed")
        //{
        //    Errors = errors;
        //}
    }
}
