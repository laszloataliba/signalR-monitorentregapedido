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
        public string UserId { get; set; }
        public string SaleChannel { get; set; } = EOrderSaleChannel.APC.ToString();
        public EOrderProcess Process { get; set; } = EOrderProcess.None;
        public EOrderCommand Command { get; set; } = EOrderCommand.None;
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
        ALL,
        BIL,
        ATM,
        BOM,
        APC,
        HIB
    }
}