using Shared.Dtos.ProductsDto;
using Shared.Enums;
using Shared.Specifications;

namespace Services.Abstraction.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParameters parameters);
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
        Task<ProductResultDto> GetProductByIdAsync(int id);
    }
}
