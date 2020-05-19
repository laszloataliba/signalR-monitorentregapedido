namespace OrderDeliveryMonitor.Model.Operation
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
    }
}
