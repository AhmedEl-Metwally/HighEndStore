using AutoMapper;
using Domain.Contracts.UnitOfWorks;
using Services.Abstraction.Interface;



namespace Services.Implementation
{
    public class ServiceManager(IUnitOfWork _unitOfWork,IMapper _mapper) : IServiceManager
    {
        private readonly Lazy<ProductService> _lazyProductService = new Lazy<ProductService>(() => new ProductService(_unitOfWork,_mapper));

        public IProductService ProductService => _lazyProductService.Value;
    }
}
