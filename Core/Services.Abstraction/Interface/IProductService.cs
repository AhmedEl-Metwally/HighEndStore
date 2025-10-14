using Shared.Dtos.ProductsDto;

namespace Services.Abstraction.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResultDto>> GetAllProductsAsync();
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
        Task<ProductResultDto> GetProductByIdAsync(int id);
    }
}
