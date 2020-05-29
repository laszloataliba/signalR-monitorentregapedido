using Newtonsoft.Json;

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
        public Pagination()
        {
        }

        /// <summary>
        /// Records per page.
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Current page identifier.
        /// </summary>
        public int CurrentPage { get; set; }
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
        [JsonIgnore]
        public int Take => PageSize;
        /// <summary>
        /// Quantity of records to be skipped.
        /// </summary>
        [JsonIgnore]
        public int Skip => ((CurrentPage - 1) * PageSize);
    }
}
