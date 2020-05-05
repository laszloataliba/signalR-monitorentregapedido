using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderDeliveryMonitor.Api.Hubs
{
    public class OrderDeliveryMonitorHub: Hub
    {
        public OrderDeliveryMonitorHub()
        {

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
