using Microsoft.AspNetCore.SignalR;
using OrderDeliveryMonitor.ApplicationConfig;
using OrderDeliveryMonitor.Facade.Interface.Operation;
using OrderDeliveryMonitor.Model.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderDeliveryMonitor.Api.Hubs
{
    public class OrderDeliveryMonitorHub: Hub
    {
        private readonly IFOrder fOrder;

        public OrderDeliveryMonitorHub(IFOrder pFOrder)
        {
            fOrder = pFOrder;
        }

        public async Task ReloadAwaitingContainer(List<Order> pOrders)
        {
            var vOrders = fOrder.GetList(order => order.Process == EOrderProcess.Awaiting);

            vOrders.ToList()
                .ForEach(order =>
                    {
                        if
                        (
                            (pOrders != null)
                                &&
                            (pOrders.Count() > 0)
                                &&
                            pOrders.Exists(o => order.OrderId == o.OrderId)
                        )
                        {
                            order.Command = EOrderCommand.None;
                        }
                    }
                );

            await Clients.All.SendAsync($"{AppUtilities.LOAD_AWAITING_CONTAINER}", vOrders);
            await Clients.All.SendAsync($"{AppUtilities.LOAD_AWAITING_CONTAINER_FOR_CUSTOMERS}", vOrders);
        }

        public async Task FromAwaitingToPreparing(List<Order> pOrders, string pOrderId, EOrderCommand pCommand = EOrderCommand.Sent)
        {
            fOrder.ToPreparing(new Order { OrderId = int.Parse(pOrderId) }, pCommand);

            var vOrders = fOrder.GetList(order => order.Process > EOrderProcess.None, item => item.Items);

            vOrders.ToList()
                .ForEach(order => {
                    if
                    (
                        (pOrders != null)
                            &&
                        (pOrders.Count() > 0)
                            &&
                        pOrders.Exists(o => order.OrderId == o.OrderId)
                    )
                    {
                        order.Command = EOrderCommand.None;
                    }

                    if (order.OrderId == int.Parse(pOrderId))
                        order.Command = (pCommand == EOrderCommand.Sent ? EOrderCommand.Received : EOrderCommand.Dropped);
                });

            await Clients.Caller.SendAsync($"{AppUtilities.LOAD_AWAITING_CONTAINER}", vOrders.Where(o => o.Process == EOrderProcess.Awaiting).ToList());
            await Clients.Caller.SendAsync($"{AppUtilities.LOAD_PREPARING_CONTAINER}", vOrders.Where(o => o.Process == EOrderProcess.Preparing).ToList());
            await Clients.Others.SendAsync($"{AppUtilities.LOAD_AWAITING_CONTAINER_FOR_CUSTOMERS}", vOrders.Where(o => o.Process == EOrderProcess.Awaiting).ToList(), pOrderId, pCommand); //Antes de atualizar o container, aplicar a class css pro pOrderId.
            await Clients.Others.SendAsync($"{AppUtilities.LOAD_PREPARING_CONTAINER_FOR_CUSTOMERS}", vOrders.Where(o => o.Process == EOrderProcess.Preparing).ToList());
        }

        public async Task FromPreparingToFinished(List<Order> pOrders, string pOrderId, EOrderCommand pCommand = EOrderCommand.Sent)
        {
            fOrder.ToFinished(new Order { OrderId = int.Parse(pOrderId) }, pCommand);

            var vOrders = fOrder.GetList(order => order.Process > EOrderProcess.Awaiting, item => item.Items);

            vOrders.ToList()
                .ForEach(order => {
                    if
                    (
                        (pOrders != null)
                            &&
                        (pOrders.Count() > 0)
                            &&
                        pOrders.Exists(o => order.OrderId == o.OrderId)
                    )
                    {
                        order.Command = EOrderCommand.None;
                    }

                    if (order.OrderId == int.Parse(pOrderId))
                        order.Command = (pCommand == EOrderCommand.Sent ? EOrderCommand.Received : EOrderCommand.Dropped);
                });

            await Clients.Caller.SendAsync($"{AppUtilities.LOAD_PREPARING_CONTAINER}", vOrders.Where(o => o.Process == EOrderProcess.Preparing).ToList());
            await Clients.Caller.SendAsync($"{AppUtilities.LOAD_FINISHED_CONTAINER}", vOrders.Where(o => o.Process == EOrderProcess.Finished).ToList());
            await Clients.Others.SendAsync($"{AppUtilities.LOAD_PREPARING_CONTAINER_FOR_CUSTOMERS}", vOrders.Where(o => o.Process == EOrderProcess.Preparing).ToList(), pOrderId, pCommand); //Antes de atualizar o container, aplicar a class css pro pOrderId.
            await Clients.Others.SendAsync($"{AppUtilities.LOAD_FINISHED_CONTAINER_FOR_CUSTOMER}", vOrders.Where(o => o.Process == EOrderProcess.Finished).ToList());
        }

        public async Task HideFinishedOrderByTimeOut(string pOrderId)
        {
            var vOrders = fOrder.GetList(order => order.Process == EOrderProcess.Finished);

            vOrders = vOrders.Where(order => DateTime.Now.Subtract(order.Finished.Value).Minutes >= 1).ToList();

            await Clients.Caller.SendAsync($"{AppUtilities.LOAD_FINISHED_CONTAINER}", vOrders);
            await Clients.Others.SendAsync($"{AppUtilities.LOAD_FINISHED_CONTAINER_FOR_CUSTOMER}", vOrders, pOrderId); //Antes de atualizar o container, aplicar a class css pro pOrderId.
        }
    }
}
