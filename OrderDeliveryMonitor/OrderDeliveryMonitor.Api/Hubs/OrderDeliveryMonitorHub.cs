using Microsoft.AspNetCore.SignalR;
using OrderDeliveryMonitor.ApplicationConfig;
using OrderDeliveryMonitor.Facade.Interface.Operation;
using OrderDeliveryMonitor.Model.Operation;
using System;
using System.Collections.Generic;
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

        public async Task TesteViaScript()
        {
            var parametro = $"Executando teste com a chamada via script. {DateTime.Now:HH:mm:ss.fff}".Split();
            string chave = $"{DateTime.Now:HH:mm:ss.fff}";
;
            await Clients.All.SendAsync("TesteViaJavaScript", parametro, chave);
        }

        public async Task ReloadAwaitingContainer()
        {
            var vOrders = fOrder.GetList(order => order.Process == EOrderProcess.Awaiting);

            await Clients.All.SendAsync($"{AppUtilities.LOAD_AWAITING_CONTAINER}", vOrders);
            await Clients.All.SendAsync($"{AppUtilities.LOAD_AWAITING_FOR_CUSTOMERS}", vOrders);
        }

        public async Task FromAwaitingToPreparing(List<Order> pOrders, string pOrderId)
        {
            //Atualiza o pedido para Preparing

            await Clients.Caller.SendAsync($"{AppUtilities.LOAD_AWAITING_CONTAINER}", pOrders);
            await Clients.Others.SendAsync($"{AppUtilities.LOAD_PREPARING_CONTAINER}", pOrders);
            await Clients.Others.SendAsync($"{AppUtilities.LOAD_AWAITING_FOR_CUSTOMERS}", pOrders, pOrderId); //Antes de atualizar o container, aplicar a class css pro pOrderId.
            await Clients.Others.SendAsync($"{AppUtilities.LOAD_PREPARING_FOR_CUSTOMERS}", pOrders);
        }

        public async Task FromPreparingToFinished(List<Order> pOrders, string pOrderId)
        {
            //Atualiza o pedido para Finished

            await Clients.Caller.SendAsync($"{AppUtilities.LOAD_PREPARING_CONTAINER}", pOrders);
            await Clients.Others.SendAsync($"{AppUtilities.LOAD_FINISHED_CONTAINER}", pOrders);
            await Clients.Others.SendAsync($"{AppUtilities.LOAD_PREPARING_FOR_CUSTOMERS}", pOrders, pOrderId); //Antes de atualizar o container, aplicar a class css pro pOrderId.
            await Clients.Others.SendAsync($"{AppUtilities.LOAD_FINISHED_CONTAINER_FOR_CUSTOMER}", pOrders);
        }

        public async Task HideFinishedOrderByTimeOut(List<Order>pOrders,string pOrderId)
        {
            await Clients.Caller.SendAsync($"{AppUtilities.LOAD_FINISHED_CONTAINER}", pOrders);
            await Clients.Others.SendAsync($"{AppUtilities.LOAD_FINISHED_CONTAINER_FOR_CUSTOMER}", pOrders, pOrderId); //Antes de atualizar o container, aplicar a class css pro pOrderId.
        }
    }
}
