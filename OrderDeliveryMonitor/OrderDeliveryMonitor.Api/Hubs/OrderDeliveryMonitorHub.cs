using Microsoft.AspNetCore.SignalR;
using OrderDeliveryMonitor.Facade.Interface.Operation;
using OrderDeliveryMonitor.Model.Operation;
using OrderDeliveryMonitor.Utility;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OrderDeliveryMonitor.Api.Hubs
{
    /// <summary>
    /// Hub for real time communication.
    /// </summary>
    public class OrderDeliveryMonitorHub : Hub
    {
        #region :: Fields ::

        /// <summary>
        /// Order model facade object.
        /// </summary>
        private readonly IFOrder fOrder;

        #endregion :: Fields ::

        #region :: Methods ::

        /// <summary>
        /// Constructor method.
        /// </summary>
        /// <param name="pFOrder">Order model facade parameter.</param>
        public OrderDeliveryMonitorHub(IFOrder pFOrder)
        {
            fOrder = pFOrder;
        }

        #endregion :: Methods ::

        #region :: Hub methods ::

        /// <summary>
        /// Orders already in monitoring and/or putted on waiting status.
        /// </summary>
        /// <param name="pOrders">Orders already in monitoring.</param>
        /// <returns>Reloads the respective containers with orders on waiting status.</returns>
        public async Task ReloadAwaitingContainer(int[] pOrders)
        {
            var vOrders = await fOrder.GetListOrderDTO(order => order.Process == EOrderProcess.Awaiting);

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

            await Clients.Caller.SendAsync($"{Utilities.LOAD_AWAITING_CONTAINER}", vOrders.OrderBy(o => o.AwaitingStart.Value).ToList());
            await Clients.Others.SendAsync($"{Utilities.LOAD_AWAITING_CONTAINER_FOR_CUSTOMERS}", vOrders.OrderBy(o => o.AwaitingStart.Value).ToList());
        }

        /// <summary>
        /// Changes order status to preparing and shows orders already in monitoring and/or putted on preparing status.
        /// </summary>
        /// <param name="pOrders">Orders already in monitoring.</param>
        /// <param name="pOrderId">Order identifier.</param>
        /// <param name="pCommand">Command to control applying styles.</param>
        /// <returns>Reloads the orders containers according to its respective status.</returns>
        public async Task FromAwaitingToPreparing(int[] pOrders, int pOrderId, EOrderCommand pCommand = EOrderCommand.Sent)
        {
            await fOrder.ToPreparing(new Order { OrderId = pOrderId }, pCommand);

            var vOrders = await fOrder.GetListOrderDTO(order => order.Process > EOrderProcess.None, item => item.Items);

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

                        if (order.OrderId == pOrderId)
                            order.Command = (pCommand == EOrderCommand.Sent ? EOrderCommand.Received : EOrderCommand.Dropped);
                    }
                );

            await Clients.All.SendAsync($"{Utilities.LOAD_AWAITING_CONTAINER}", vOrders.Where(o => o.Process == EOrderProcess.Awaiting).OrderBy(o => o.AwaitingStart.Value).ToList());
            await Clients.All.SendAsync($"{Utilities.LOAD_PREPARING_CONTAINER}", vOrders.Where(o => o.Process == EOrderProcess.Preparing).OrderBy(o => o.PreparingStart.Value).ToList());
            await Clients.Others.SendAsync($"{Utilities.LOAD_AWAITING_CONTAINER_FOR_CUSTOMERS}", vOrders.Where(o => o.Process == EOrderProcess.Awaiting).OrderBy(o => o.AwaitingStart.Value).ToList(), pOrderId, pCommand); //Antes de atualizar o container, aplicar a class css pro pOrderId.
            await Clients.Others.SendAsync($"{Utilities.LOAD_PREPARING_CONTAINER_FOR_CUSTOMERS}", vOrders.Where(o => o.Process == EOrderProcess.Preparing).OrderBy(o => o.PreparingStart.Value).ToList());
        }

        /// <summary>
        /// Changes order status to ready/redeemed and shows orders already in monitoring and/or putted on ready/redeemed status.
        /// </summary>
        /// <param name="pOrders">Orders already in monitoring.</param>
        /// <param name="pOrderId">Order identifier.</param>
        /// <param name="pCommand">Command to control applying styles.</param>
        /// <returns>Reloads the orders containers according to its respective status.</returns>
        public async Task FromPreparingToReady(int[] pOrders, int pOrderId, EOrderCommand pCommand = EOrderCommand.Sent)
        {
            await fOrder.ToReady(new Order { OrderId = pOrderId }, pCommand);

            var vOrders = await fOrder.GetListOrderDTO(order => order.Process > EOrderProcess.Awaiting, item => item.Items);

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

                        if (order.OrderId == pOrderId)
                            order.Command = (pCommand == EOrderCommand.Sent ? EOrderCommand.Received : EOrderCommand.Dropped);
                    }
                );

            await Clients.All.SendAsync($"{Utilities.LOAD_PREPARING_CONTAINER}", vOrders.Where(o => o.Process == EOrderProcess.Preparing).OrderBy(o => o.PreparingStart.Value).ToList());
            await Clients.All.SendAsync($"{Utilities.LOAD_READY_CONTAINER}", vOrders.Where(o => o.Process == EOrderProcess.Ready).OrderBy(o => o.ReadyStart.Value).ToList());
            await Clients.Others.SendAsync($"{Utilities.LOAD_PREPARING_CONTAINER_FOR_CUSTOMERS}", vOrders.Where(o => o.Process == EOrderProcess.Preparing).OrderBy(o => o.PreparingStart.Value).ToList(), pOrderId, pCommand); //Antes de atualizar o container, aplicar a class css pro pOrderId.
            await Clients.Others.SendAsync($"{Utilities.LOAD_READY_CONTAINER_FOR_CUSTOMER}", vOrders.Where(o => o.Process == EOrderProcess.Ready).OrderBy(o => o.ReadyStart.Value).ToList());
        }

        /// <summary>
        /// Reloads the containers for orders on ready/redeemed status that achieve its timed out defined.
        /// </summary>
        /// <param name="pOrderId">Order identifier.</param>
        /// <returns>Reloads the containers for orders on ready/redeemed status.</returns>
        public async Task HideReadyOrderByTimeOut(int pOrderId)
        {
            var vOrders = await fOrder.GetListOrderDTO(order => order.Process == EOrderProcess.Ready);

            vOrders = vOrders.Where(order => DateTime.Now.Subtract(order.ReadyStart.Value).Minutes >= 1).ToList();

            await Clients.All.SendAsync($"{Utilities.LOAD_READY_CONTAINER}", vOrders.OrderBy(o => o.ReadyStart.Value).ToList());
            await Clients.Others.SendAsync($"{Utilities.LOAD_READY_CONTAINER_FOR_CUSTOMER}", vOrders.OrderBy(o => o.ReadyStart.Value).ToList(), pOrderId); //Antes de atualizar o container, aplicar a class css pro pOrderId.
        }

        #endregion :: Hub methods ::
    }
}
