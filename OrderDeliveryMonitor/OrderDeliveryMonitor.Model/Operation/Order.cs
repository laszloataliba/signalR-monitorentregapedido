using System;
using System.Collections.Generic;

namespace OrderDeliveryMonitor.Model.Operation
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public string OrderCode { get; set; }
        public int? SellingStation { get; set; }
        public string Cashier { get; set; }
        public EOrderProcess Process { get; set; } = EOrderProcess.None;
        public EOrderCommand Command { get; set; } = EOrderCommand.None;
        public EOrderSaleChannel SaleChannel { get; set; } = EOrderSaleChannel.BoxOffice;
        public DateTime? AwaitingStart { get; set; }
        public DateTime? AwaitingEnd { get; set; }
        public DateTime? PreparingStart { get; set; }
        public DateTime? PreparingEnd { get; set; }
        public DateTime? ReadyStart { get; set; }
        public DateTime? ReadyEnd { get; set; }
        public DateTime? RedeemDate { get; set; }

        public virtual IEnumerable<OrderItem> Items { get; set; }
    }

    public enum EOrderCommand
    {
        None = 0,
        Sent = 10,
        Dragged = 20,
        Received = 30,
        Dropped = 40
    }

    public enum EOrderProcess
    {
        None = 0,
        Awaiting = 10,
        Preparing = 20,
        Ready = 30,
        Redeemed = 40
    }

    public enum EOrderSaleChannel
    {
        BoxOffice = 10,
        Hybrid = 20,
        ATM = 30,
        APP = 40
    }
}
