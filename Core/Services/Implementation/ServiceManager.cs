using AutoMapper;
using Domain.Contracts.BasketRepositorys;
using Domain.Contracts.UnitOfWorks;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Services.Abstraction.Interface;

namespace Services.Implementation
{
    public class ServiceManager(
                                    IUnitOfWork _unitOfWork,
                                    IMapper _mapper,
                                    IBasketRepository _basketRepository,
                                    UserManager<User> _userManager
                                ) : IServiceManager
    {
        private readonly Lazy<IProductService> _lazyProductService = new Lazy<IProductService>(() => new ProductService(_unitOfWork,_mapper));
        private readonly Lazy<IBasketService> _lazyBasketService = new Lazy<IBasketService>(() => new BasketService(_basketRepository,_mapper));
        private readonly Lazy<IAuthenticationService> _lazyAuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager));

        public IProductService ProductService => _lazyProductService.Value;

        public IBasketService BasketService => _lazyBasketService.Value;

        public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;
    }
}
