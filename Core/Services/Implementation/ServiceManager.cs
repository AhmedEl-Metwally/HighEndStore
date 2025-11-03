using Services.Abstraction.Interface;

namespace Services.Implementation
{
    //public class ServiceManager(
    //                                IUnitOfWork _unitOfWork,
    //                                IMapper _mapper,
    //                                IBasketRepository _basketRepository,
    //                                UserManager<User> _userManager,
    //                                IOptions<JwtOption> _options,
    //                                IConfiguration _configuration
    //                            ) : IServiceManager
    //{
    //    private readonly Lazy<IProductService> _lazyProductService = new Lazy<IProductService>(() => new ProductService(_unitOfWork,_mapper));
    //    private readonly Lazy<IBasketService> _lazyBasketService = new Lazy<IBasketService>(() => new BasketService(_basketRepository,_mapper));
    //    private readonly Lazy<IAuthenticationService> _lazyAuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager, _options, _mapper));
    //    private readonly Lazy<IOrderService> _lazyOrderService = new Lazy<IOrderService>(() => new OrderService(_mapper,_basketRepository,_unitOfWork));
    //    private readonly Lazy<IPaymentService> _LazyPaymentService = new Lazy<IPaymentService>(() => new PaymentService(_configuration,_basketRepository,_unitOfWork,_mapper));

    //    public IProductService ProductService => _lazyProductService.Value;

    //    public IBasketService BasketService => _lazyBasketService.Value;

    //    public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;

    //    public IOrderService OrderService => _lazyOrderService.Value;

    //    public IPaymentService PaymentService => _LazyPaymentService.Value;
    //}



    /* Refactor ServiceManager */
    public class ServiceManager(
                                 Func<IProductService> _productFactory,
                                 Func<IBasketService> _basketFactory,
                                 Func<IAuthenticationService> _authenticationFactory,
                                 Func<IOrderService> _orderFactory,
                                 Func<IPaymentService> _paymentFactory,
                                 Func<ICacheService> _cacheFactory
                               ) : IServiceManager
    {

        public IProductService ProductService => _productFactory.Invoke();

        public IBasketService BasketService => _basketFactory.Invoke();

        public IAuthenticationService AuthenticationService => _authenticationFactory.Invoke();

        public IOrderService OrderService => _orderFactory.Invoke();

        public IPaymentService PaymentService => _paymentFactory.Invoke();

        public ICacheService CacheService => _cacheFactory.Invoke();
    }






}
