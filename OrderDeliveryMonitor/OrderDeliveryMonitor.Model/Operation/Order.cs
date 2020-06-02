using System;
using System.Collections.Generic;

namespace OrderDeliveryMonitor.Model.Operation
{
    /// <summary>
    /// Order model.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Order relational key.
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// Order Code identifier.
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// Number to identify order.
        /// </summary>
        public string OrderNumber { get; set; }
        /// <summary>
        /// Identification for selling station.
        /// </summary>
        public int? SellingStation { get; set; }
        /// <summary>
        /// Cashier identifier.
        /// </summary>
        public string Cashier { get; set; }
        /// <summary>
        /// Operator identifier.
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Sale channel identification.
        /// </summary>
        public string SaleChannel { get; set; } = EOrderSaleChannel.APC.ToString();
        /// <summary>
        /// Current process (Awaiting | Preparing | Ready - Redeemed).
        /// </summary>
        public EOrderProcess Process { get; set; } = EOrderProcess.None;
        /// <summary>
        /// Style controller.
        /// </summary>
        public EOrderCommand Command { get; set; } = EOrderCommand.None;
        /// <summary>
        /// The moment when check-in is made.
        /// </summary>
        public DateTime? AwaitingStart { get; set; }
        /// <summary>
        /// When operator moves the order to preparing process.
        /// </summary>
        public DateTime? AwaitingEnd { get; set; }
        /// <summary>
        /// Moment when the order is moved to preparing process.
        /// </summary>
        public DateTime? PreparingStart { get; set; }
        /// <summary>
        /// When operator moves the order to ready.
        /// </summary>
        public DateTime? PreparingEnd { get; set; }
        /// <summary>
        /// Moment when the order is moved to ready.
        /// </summary>
        public DateTime? ReadyStart { get; set; }
        /// <summary>
        /// Moment when operator delivers the order to customer.
        /// </summary>
        public DateTime? ReadyEnd { get; set; }
        /// <summary>
        /// When order is redeemed by customer.
        /// </summary>
        public DateTime? RedeemDate { get; set; }
        /// <summary>
        /// Product list attached to order.
        /// </summary>
        public virtual IEnumerable<OrderItem> Items { get; set; }
    }

    /// <summary>
    /// Style controller.
    /// </summary>
    public enum EOrderCommand
    {
        /// <summary>
        /// No effect css class.
        /// </summary>
        None = 0,
        /// <summary>
        /// Applied when operator clicks to change process.
        /// </summary>
        Sent = 10,
        /// <summary>
        /// Applied when operator drags to change process.
        /// </summary>
        Dragged = 20,
        /// <summary>
        /// Applied when process has changed by click.
        /// </summary>
        Received = 30,
        /// <summary>
        /// Applied when process has changed by dragging.
        /// </summary>
        Dropped = 40
    }

    /// <summary>
    /// Order process.
    /// </summary>
    public enum EOrderProcess
    {
        /// <summary>
        /// Order was not putted in line yet(waiting for checkin).
        /// </summary>
        None = 0,
        /// <summary>
        /// Order is waiting for preparation.
        /// </summary>
        Awaiting = 10,
        /// <summary>
        /// Order is being prepared.
        /// </summary>
        Preparing = 20,
        /// <summary>
        /// Order is ready to redeem.
        /// </summary>
        Ready = 30,
        /// <summary>
        /// Order redeemed.
        /// </summary>
        Redeemed = 40
    }

    /// <summary>
    /// Sale channel.
    /// </summary>
    public enum EOrderSaleChannel
    {
        /// <summary>
        /// All.
        /// </summary>
        ALL,
        /// <summary>
        /// Box office.
        /// </summary>
        BIL,
        /// <summary>
        /// ATM.
        /// </summary>
        ATM,
        /// <summary>
        /// Bomboniere.
        /// </summary>
        BOM,
        /// <summary>
        /// e-Commerce.
        /// </summary>
        APC,
        /// <summary>
        /// Hybrid box office.
        /// </summary>
        HIB
    }
}