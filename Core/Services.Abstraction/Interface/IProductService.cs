using Shared.Dtos.ProductsDto;

namespace Services.Abstraction.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResultDto>> GetAllProductsAsync(int? typeId, int? brandId);
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
        Task<ProductResultDto> GetProductByIdAsync(int id);
    }
}
