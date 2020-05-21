﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OrderDeliveryMonitor.Api.Hubs;
using OrderDeliveryMonitor.Facade.Implementation.Operation.DTO;
using OrderDeliveryMonitor.Facade.Interface.Operation;
using OrderDeliveryMonitor.Utility;
using System.Collections.Generic;
using System.Threading.Tasks;
using nsOrderModel = OrderDeliveryMonitor.Model.Operation;

namespace OrderDeliveryMonitor.Api.Controllers.Operation
{
    [Route("api/Operation/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IHubContext<OrderDeliveryMonitorHub> _hubContext;
        private readonly IFOrder fOrder;

        public OrderController(IHubContext<OrderDeliveryMonitorHub> hubContext, IFOrder pFOrder)
        {
            _hubContext = hubContext;
            fOrder = pFOrder;
        }

        // GET: api/Order
        [HttpGet]
        public IEnumerable<OrderDTO> Get()
        {
            var vOrders = fOrder.GetListOrderDTO(pInclude: itm => itm.Items);

            return vOrders;
        }

        [HttpGet("{id}")]
        public OrderDTO Get(int id)
        {
            var vOrder = fOrder.GetOrderDTO(order => order.OrderId == id, items => items.Items);

            return vOrder;
        }

        [HttpPut("{pOrderId}")]
        public async Task Put(string pOrderId)
        {
            fOrder.ToAwaiting(new nsOrderModel.Order { OrderId = int.Parse(pOrderId) });

            await _hubContext.Clients.All.SendAsync($"{AppUtilities.RELOAD_AWAITING_CONTAINER}");
        }
    }
}
