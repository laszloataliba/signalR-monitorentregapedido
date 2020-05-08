using Microsoft.AspNetCore.SignalR;
using OrderDeliveryMonitor.Facade.Interface.Operation;
using System;
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
    }
}
