using Microsoft.AspNetCore.SignalR;
using OrderDeliveryMonitor.Facade.Interface.Operation;
using OrderDeliveryMonitor.Model.Operation;
using OrderDeliveryMonitor.Utility;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OrderDeliveryMonitor.Api.Hubs
{
    public class OrderDeliveryMonitorHub : Hub
    {
        private readonly IFOrder fOrder;

        public OrderDeliveryMonitorHub(IFOrder pFOrder)
        {
            fOrder = pFOrder;
        }

        public async Task ReloadAwaitingContainer(int[] pOrders)
        {
            var vOrders = fOrder.GetList(order => order.Process == EOrderProcess.Awaiting);

            vOrders.ToList()
                .ForEach(order =>
                    {
                        if
                        (
                            (pOrders != null)
                                &&
                            (pOrders.Length > 0)
                                &&
                            pOrders.ToList().Exists(o => order.OrderId == o)
                        )
                        {
                            order.Command = EOrderCommand.None;
                        }
                    }
                );

            await Clients.Caller.SendAsync($"{AppUtilities.LOAD_AWAITING_CONTAINER}", vOrders.OrderBy(o => o.AwaitingStart.Value).ToList());
            await Clients.Others.SendAsync($"{AppUtilities.LOAD_AWAITING_CONTAINER_FOR_CUSTOMERS}", vOrders.OrderBy(o => o.AwaitingStart.Value).ToList());
        }

        public async Task FromAwaitingToPreparing(int[] pOrders, string pOrderId, EOrderCommand pCommand = EOrderCommand.Sent)
        {
            fOrder.ToPreparing(new Order { OrderId = int.Parse(pOrderId) }, pCommand);

            var vOrders = fOrder.GetList(order => order.Process > EOrderProcess.None, item => item.Items);

            vOrders.ToList()
                .ForEach(order =>
                    {
                        if
                        (
                            (pOrders != null)
                                &&
                            (pOrders.Length > 0)
                                &&
                            pOrders.ToList().Exists(o => order.OrderId == o)
                        )
                        {
                            order.Command = EOrderCommand.None;
                        }

                        if (order.OrderId == int.Parse(pOrderId))
                            order.Command = (pCommand == EOrderCommand.Sent ? EOrderCommand.Received : EOrderCommand.Dropped);
                    }
                );

            await Clients.All.SendAsync($"{AppUtilities.LOAD_AWAITING_CONTAINER}", vOrders.Where(o => o.Process == EOrderProcess.Awaiting).OrderBy(o => o.AwaitingStart.Value).ToList());
            await Clients.All.SendAsync($"{AppUtilities.LOAD_PREPARING_CONTAINER}", vOrders.Where(o => o.Process == EOrderProcess.Preparing).OrderBy(o => o.PreparingStart.Value).ToList());
            await Clients.Others.SendAsync($"{AppUtilities.LOAD_AWAITING_CONTAINER_FOR_CUSTOMERS}", vOrders.Where(o => o.Process == EOrderProcess.Awaiting).OrderBy(o => o.AwaitingStart.Value).ToList(), pOrderId, pCommand); //Antes de atualizar o container, aplicar a class css pro pOrderId.
            await Clients.Others.SendAsync($"{AppUtilities.LOAD_PREPARING_CONTAINER_FOR_CUSTOMERS}", vOrders.Where(o => o.Process == EOrderProcess.Preparing).OrderBy(o => o.PreparingStart.Value).ToList());
        }

        public async Task FromPreparingToReady(int[] pOrders, string pOrderId, EOrderCommand pCommand = EOrderCommand.Sent)
        {
            fOrder.ToReady(new Order { OrderId = int.Parse(pOrderId) }, pCommand);

            var vOrders = fOrder.GetList(order => order.Process > EOrderProcess.Awaiting, item => item.Items);

            vOrders.ToList()
                .ForEach(order =>
                    {
                        if
                        (
                            (pOrders != null)
                                &&
                            (pOrders.Length > 0)
                                &&
                            pOrders.ToList().Exists(o => order.OrderId == o)
                        )
                        {
                            order.Command = EOrderCommand.None;
                        }

                        if (order.OrderId == int.Parse(pOrderId))
                            order.Command = (pCommand == EOrderCommand.Sent ? EOrderCommand.Received : EOrderCommand.Dropped);
                    }
                );

            await Clients.All.SendAsync($"{AppUtilities.LOAD_PREPARING_CONTAINER}", vOrders.Where(o => o.Process == EOrderProcess.Preparing).OrderBy(o => o.PreparingStart.Value).ToList());
            await Clients.All.SendAsync($"{AppUtilities.LOAD_READY_CONTAINER}", vOrders.Where(o => o.Process == EOrderProcess.Ready).OrderBy(o => o.ReadyStart.Value).ToList());
            await Clients.Others.SendAsync($"{AppUtilities.LOAD_PREPARING_CONTAINER_FOR_CUSTOMERS}", vOrders.Where(o => o.Process == EOrderProcess.Preparing).OrderBy(o => o.PreparingStart.Value).ToList(), pOrderId, pCommand); //Antes de atualizar o container, aplicar a class css pro pOrderId.
            await Clients.Others.SendAsync($"{AppUtilities.LOAD_READY_CONTAINER_FOR_CUSTOMER}", vOrders.Where(o => o.Process == EOrderProcess.Ready).OrderBy(o => o.ReadyStart.Value).ToList());
        }

        public async Task HideReadyOrderByTimeOut(string pOrderId)
        {
            var vOrders = fOrder.GetList(order => order.Process == EOrderProcess.Ready);

            vOrders = vOrders.Where(order => DateTime.Now.Subtract(order.ReadyStart.Value).Minutes >= 1).ToList();

            await Clients.All.SendAsync($"{AppUtilities.LOAD_READY_CONTAINER}", vOrders.OrderBy(o => o.ReadyStart.Value).ToList());
            await Clients.Others.SendAsync($"{AppUtilities.LOAD_READY_CONTAINER_FOR_CUSTOMER}", vOrders.OrderBy(o => o.ReadyStart.Value).ToList(), pOrderId); //Antes de atualizar o container, aplicar a class css pro pOrderId.
        }
    }
}
