namespace OrderDeliveryMonitor.Utility
{
    /// <summary>
    /// Helper class for pagination.
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// Constructor method.
        /// </summary>
        /// <param name="pPageSize">Desired quantity of records per page.</param>
        /// <param name="pCurrentPage">Desired page to be shown.</param>
        public Pagination(int pPageSize = 10, int pCurrentPage = 1)
        {
            PageSize = pPageSize;
            CurrentPage = pCurrentPage;
        }

        /// <summary>
        /// Records per page.
        /// </summary>
        public int PageSize { get; private set; }
        /// <summary>
        /// Current page identifier.
        /// </summary>
        public int CurrentPage { get; private set; }
        /// <summary>
        /// Total records.
        /// </summary>
        public int TotalRecords { get; set; }
        /// <summary>
        /// Total pages.
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// Quantity of records to be taken in the request.
        /// </summary>
        public int Take => PageSize;
        /// <summary>
        /// Quantity of records to be skipped.
        /// </summary>
        public int Skip => ((CurrentPage - 1) * PageSize);
    }
}
