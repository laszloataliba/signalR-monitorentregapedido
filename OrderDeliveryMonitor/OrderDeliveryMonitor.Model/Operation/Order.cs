using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderDeliveryMonitor.Model.Operation
{
    [Table(nameof(Order))]
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public string EstacaoVendaCaixa { get; set; }
        public DateTime? AwaitingStart { get; set; }
        public DateTime? AwaitingEnd { get; set; }
        public DateTime? PreparingStart { get; set; }
        public DateTime? PreparingEnd { get; set; }
        public DateTime? Finished { get; set; }

        public IEnumerable<OrderItem> Items { get; set; } = new List<OrderItem>();

        [NotMapped]
        public string TicketLayout { get; set; }
    }
}
