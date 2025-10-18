using Domain.Entities.ProductModule;

namespace Services.Specifications
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecifications<Product,int>
    {
        public ProductWithBrandAndTypeSpecification() : base(null)
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
