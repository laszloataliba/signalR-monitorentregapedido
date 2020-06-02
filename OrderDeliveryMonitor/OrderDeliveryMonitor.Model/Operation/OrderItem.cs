namespace OrderDeliveryMonitor.Model.Operation
{
    /// <summary>
    /// Model for Order items.
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// Order item relational key.
        /// </summary>
        public int OrderItemId { get; set; }
        /// <summary>
        /// Order identification.
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Order Order { get; set; }
        /// <summary>
        /// Product desciption.
        /// </summary>
        public string Product { get; set; }
        /// <summary>
        /// Product quantity.
        /// </summary>
        public int Quantity { get; set; }
    }
}
