using AutoMapper;
using Domain.Contracts.BasketRepositorys;
using Domain.Contracts.UnitOfWorks;
using Services.Abstraction.Interface;

namespace Services.Implementation
{
    public class ServiceManager(IUnitOfWork _unitOfWork,IMapper _mapper,IBasketRepository _basketRepository) : IServiceManager
    {
        private readonly Lazy<IProductService> _lazyProductService = new Lazy<IProductService>(() => new ProductService(_unitOfWork,_mapper));
        private readonly Lazy<IBasketService> _lazyBasketService = new Lazy<IBasketService>(() => new BasketService(_basketRepository,_mapper));

        public IProductService ProductService => _lazyProductService.Value;

        public IBasketService BasketService => _lazyBasketService.Value;
    }
}
