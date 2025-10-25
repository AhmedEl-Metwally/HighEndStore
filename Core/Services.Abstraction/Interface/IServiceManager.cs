
namespace Services.Abstraction.Interface
{
    public interface IServiceManager
    {
        public IProductService ProductService  { get;  }
        public IBasketService BasketService { get;  }
    }
}
