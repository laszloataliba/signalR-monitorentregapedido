using System.ComponentModel.DataAnnotations.Schema;

namespace OrderDeliveryMonitor.Model.Operation
{
    [Table(nameof(OrderItem))]
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
    }
}
