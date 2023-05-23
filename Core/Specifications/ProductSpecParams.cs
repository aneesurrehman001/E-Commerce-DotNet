namespace Core.Specifications
{
    public class ProductSpecParams
    {
        // it can have maximum 50 products on one page
        private const int MaxPageSize = 50;

        // it is going to return first page by default.
        public int PageIndex { get; set; } = 1;

        // We are setting it as 6 products on one page but can't exceed 50 which is the max
        public int _pageSize { get; set; } = 6;

        //
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string Sort { get; set; }

        private string _search;
        public string Search { get => _search; set => _search = value.ToLower(); }


    }
}