
namespace Domain.Exceptions
{
    public sealed class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(int id ) : base($"Product with id{id} not Found")
        { }
    }
}
