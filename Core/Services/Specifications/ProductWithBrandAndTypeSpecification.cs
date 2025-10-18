using Domain.Entities.ProductModule;

namespace Services.Specifications
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecifications<Product,int>
    {
        public ProductWithBrandAndTypeSpecification(int? typeId, int? brandId) : base
                                                                                    (P =>(!typeId.HasValue || P.TypeId == typeId) &&
                                                                                                 (!brandId.HasValue || P.BrandId == brandId))
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
        }

        public ProductWithBrandAndTypeSpecification(int id ) : base(p => p.Id == id)
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
        }
    }
}
