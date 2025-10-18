using Shared.Enums;

namespace Shared.Specifications
{
    public class ProductSpecificationParameters
    {
        public int? typeId { get; set; }
        public int? brandId { get; set; }
        public ProductSortingOptions productSorting { get; set; }
        public string? Search { get; set; }

        private const int defaultPageSize = 5;
        private const int maxPageSize = 5;
        public int pageIndex { get; set; }

        private int _pageSize = defaultPageSize ;

        public int pageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : value ; }
        }

    }
}
