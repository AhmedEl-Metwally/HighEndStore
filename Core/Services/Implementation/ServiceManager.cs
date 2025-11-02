using AutoMapper;
using Domain.Contracts.BasketRepositorys;
using Domain.Contracts.UnitOfWorks;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Abstraction.Interface;
using Shared.Common;

namespace Services.Implementation
{
    public class ServiceManager(
                                    IUnitOfWork _unitOfWork,
                                    IMapper _mapper,
                                    IBasketRepository _basketRepository,
                                    UserManager<User> _userManager,
                                    IOptions<JwtOption> _options,
                                    IConfiguration _configuration
                                ) : IServiceManager
    {
        private readonly Lazy<IProductService> _lazyProductService = new Lazy<IProductService>(() => new ProductService(_unitOfWork,_mapper));
        private readonly Lazy<IBasketService> _lazyBasketService = new Lazy<IBasketService>(() => new BasketService(_basketRepository,_mapper));
        private readonly Lazy<IAuthenticationService> _lazyAuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager, _options, _mapper));
        private readonly Lazy<IOrderService> _lazyOrderService = new Lazy<IOrderService>(() => new OrderService(_mapper,_basketRepository,_unitOfWork));
        private readonly Lazy<IPaymentService> _LazyPaymentService = new Lazy<IPaymentService>(() => new PaymentService(_configuration,_basketRepository,_unitOfWork,_mapper));

        public IProductService ProductService => _lazyProductService.Value;

        public IBasketService BasketService => _lazyBasketService.Value;

        public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;

        public IOrderService OrderService => _lazyOrderService.Value;

        public IPaymentService PaymentService => _LazyPaymentService.Value;
    }
}
