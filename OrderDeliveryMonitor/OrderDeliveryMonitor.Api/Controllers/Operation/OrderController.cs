using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OrderDeliveryMonitor.Api.Hubs;
using OrderDeliveryMonitor.Facade.Interface.Operation;
using nsOrderModel = OrderDeliveryMonitor.Model.Operation;

namespace OrderDeliveryMonitor.Api.Controllers.Operation
{
    [Route("Operation/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IHubContext<OrderDeliveryMonitorHub> _hubContext;
        private readonly IFOrder fOrder;

        public OrderController(IHubContext<OrderDeliveryMonitorHub> hubContext, IFOrder pfOrder)
        {
            _hubContext = hubContext;
            fOrder = pfOrder;
        }

        // GET: api/Order
        [HttpGet]
        public IEnumerable<nsOrderModel.Order> Get()
        {
            var vOrders = fOrder.GetList();

            return vOrders;
        }

        // GET: api/Order/5
        [HttpGet("{id}", Name = "Get")]
        public nsOrderModel.Order Get(int id)
        {
            var vOrder = fOrder.Get(order => order.OrderId == id);

            return vOrder;
        }

        // POST: api/Order
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
