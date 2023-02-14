namespace ShopBridge.Models
{
    public class PagingModel
    {
        const int maxPageSize = 10;
        private int _pageSize { get; set; }
        public int pageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }
        public int pageNumber { get; set; }

    }
}