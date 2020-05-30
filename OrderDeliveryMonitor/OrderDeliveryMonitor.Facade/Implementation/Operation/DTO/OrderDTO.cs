using OrderDeliveryMonitor.Model.Operation;
using OrderDeliveryMonitor.Utility;
using System;
using System.Collections.Generic;

namespace OrderDeliveryMonitor.Facade.Implementation.Operation.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public string OrderCode { get; set; }
        public string OrderNumber { get; set; }
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
        public DateTime? ReadEnd { get; set; }
        public DateTime? RedeemDate { get; set; }

        public IEnumerable<OrderItem> Items { get; set; }

        public string TicketLayout => Layout();

        public string CustomerTicketLayout =>
            $@"
                <div id='tck{OrderId}' class='quote-container {CommandStyle()}'>
                    <i class='pin'></i>
                    <div class='note {ProcessColor()}'>
                        <div class='note-body'>
                            <span>
                                #{OrderNumber}
                            </span>
                        </div>
                        <div class='note-footer'>
                            <div class='details'></div>
                            <div class='timer'></div>
                            <div class='command'></div>
                        </div>
                    </div>
                </div>
            ";

        private string Layout()
        {
            string sLayout =
                $@"
                    <div id='tck{OrderId}' class='quote-container {CommandStyle()}'>
                        <i class='pin'></i>
                        <div class='note {ProcessColor()}'>
                            <div class='note-body'>
                                <span>
                                    #{OrderNumber}
                                </span>
                            </div>
                            <div class='note-footer'>
                                <div class='details'>
                                    >
                                </div>
                                <div class='timer' 
                                     data-startdate='{SetStartTime()}'>
                                    {SetTimer()}
                                </div>
                                <div class='command' 
                                     style='visibility: {(Process == EOrderProcess.Ready ? "hidden" : "visible")}' 
                                     data-orderid='{OrderId}' 
                                     onclick='{SetCommand()}'>
                                    >
                                </div>
                            </div>
                        </div>
                    </div>
                ";

            return sLayout;
        }

        private string ProcessColor()
        {
            switch (Process)
            {
                case EOrderProcess.Preparing:
                    return Utilities.PREPARING_CLASS;

                case EOrderProcess.Ready:
                    return Utilities.READY_CLASS;

                default:
                    return Utilities.AWAITING_CLASS;
            }
        }

        private string CommandStyle()
        {
            switch (Command)
            {
                case EOrderCommand.Sent:
                    return "sent";

                case EOrderCommand.Dragged:
                    return "dragged";

                case EOrderCommand.Received:
                    return "received";

                case EOrderCommand.Dropped:
                    return "dropped";

                case EOrderCommand.None:
                default:
                    return "";
            }
        }

        private string SetCommand()
        {
            switch (Process)
            {
                case EOrderProcess.Awaiting:
                    return "ToPreparingByClick(this)";

                case EOrderProcess.Preparing:
                    return "ToFinishedByClick(this)";

                case EOrderProcess.Ready:
                case EOrderProcess.None:
                default:
                    return "";
            }
        }

        private string SetTimer()
        {
            switch (Process)
            {
                case EOrderProcess.Awaiting:
                    return $"{((!AwaitingEnd.HasValue) ? DateTime.Now.Subtract(AwaitingStart.Value).Duration().ToString("mm\\mss\\s") : "")}";

                case EOrderProcess.Preparing:
                    return $"{((!PreparingEnd.HasValue) ? DateTime.Now.Subtract(PreparingStart.Value).Duration().ToString("mm\\mss\\s") : "")}";

                case EOrderProcess.Ready:
                    return $"{(ReadyStart.HasValue ? DateTime.Now.Subtract(ReadyStart.Value).Duration().ToString("mm\\mss\\s") : "")}";

                case EOrderProcess.None:
                default:
                    return "";
            }
        }

        private string SetStartTime()
        {
            switch (Process)
            {
                case EOrderProcess.Awaiting:
                    return $"{(AwaitingStart.HasValue ? AwaitingStart.ToString() : "")}";

                case EOrderProcess.Preparing:
                    return $"{(PreparingStart.HasValue ? PreparingStart.ToString() : "")}";

                case EOrderProcess.Ready:
                    return $"{(ReadyStart.HasValue ? ReadyStart.ToString() : "")}";

                case EOrderProcess.None:
                default:
                    return "";
            }
        }
    }
}
