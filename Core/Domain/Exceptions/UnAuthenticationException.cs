
namespace Domain.Exceptions
{
    public sealed class UnAuthenticationException : Exception
    {
        public UnAuthenticationException(string message = "Invalid email or password") : base(message)
        {
        }
    }
}
