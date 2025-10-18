
using Domain.Entities.ProductModule;
using Shared.Specifications;

namespace Services.Specifications
{
    public class ProductCountSpecifications : BaseSpecifications<Product, int>
    {
        public ProductCountSpecifications(ProductSpecificationParameters parameters) : base
                                                   (P => (!parameters.typeId.HasValue || P.TypeId == parameters.typeId) &&
                                                                (!parameters.brandId.HasValue || P.BrandId == parameters.brandId) &&
                                                                (string.IsNullOrEmpty(parameters.Search) || P.Name.ToLower().Contains(parameters.Search.ToLower())))  
        {
            
        }
    }
}
