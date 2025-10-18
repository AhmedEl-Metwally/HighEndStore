using Domain.Entities.ProductModule;
using Shared.Enums;
using Shared.Specifications;

namespace Services.Specifications
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecifications<Product,int>
    {
        public ProductWithBrandAndTypeSpecification(ProductSpecificationParameters parameters) : base
                                                                                    (P =>(!parameters.typeId.HasValue || P.TypeId == parameters.typeId) &&
                                                                                                 (!parameters.brandId.HasValue || P.BrandId == parameters.brandId))
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);

            switch (parameters.productSorting)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderDescending(p => p.Name);
                    break;
                case ProductSortingOptions.priceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.priceDesc:
                    AddOrderDescending(p => p.Price);
                    break;
                     default:
                    break;
            }
        }

        public ProductWithBrandAndTypeSpecification(int id ) : base(p => p.Id == id)
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
        }
    }
}
