using AutoMapper;
using Domain.Contracts.UnitOfWorks;
using Domain.Entities.ProductModule;
using Services.Abstraction.Interface;
using Services.Specifications;
using Shared.Dtos.ProductsDto;

namespace Services.Implementation
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var brandRep = _unitOfWork.GetRepository<ProductBrand, int>();
            var brands = await brandRep.GetAllAsync();
            var brandsResult = _mapper.Map<IEnumerable<BrandResultDto>>(brands);
            return brandsResult;
        }

        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync()
        {
            var productRep = _unitOfWork.GetRepository<Product,int>() ;
            var specification = new ProductWithBrandAndTypeSpecification();
            var products = await productRep.GetAllAsync(specification);
            var productResult = _mapper.Map<IEnumerable<ProductResultDto>>(products);
            return productResult;
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var typeRep = _unitOfWork.GetRepository<ProductType,int>();
            var types = await typeRep.GetAllAsync();
            var typeResult = _mapper.Map<IEnumerable<TypeResultDto>>(types);
            return typeResult;
        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var productRep = _unitOfWork.GetRepository<Product, int>();
            var specificationId = new ProductWithBrandAndTypeSpecification(id);
            var products = await productRep.GetByIdAsync(specificationId);
            var productResult = _mapper.Map<ProductResultDto>(products);
            return productResult;
        }
    }
}
