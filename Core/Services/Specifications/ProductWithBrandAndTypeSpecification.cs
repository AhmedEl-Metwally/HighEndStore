using Domain.Entities.ProductModule;
using Shared.Enums;

namespace Services.Specifications
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecifications<Product,int>
    {
        public ProductWithBrandAndTypeSpecification(int? typeId, int? brandId, ProductSortingOptions productSorting) : base
                                                                                    (P =>(!typeId.HasValue || P.TypeId == typeId) &&
                                                                                                 (!brandId.HasValue || P.BrandId == brandId))
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);

            switch (productSorting)
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
