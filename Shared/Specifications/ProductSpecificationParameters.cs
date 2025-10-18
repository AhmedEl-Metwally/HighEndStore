using Shared.Enums;

namespace Shared.Specifications
{
    public class ProductSpecificationParameters
    {
        public int? typeId { get; set; }
        public int? brandId { get; set; }
        public ProductSortingOptions productSorting { get; set; }
        public string? Search { get; set; }
    }
}
