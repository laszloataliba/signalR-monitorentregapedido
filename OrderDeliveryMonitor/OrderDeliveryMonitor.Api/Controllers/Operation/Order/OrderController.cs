using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OrderDeliveryMonitor.Api.Hubs;

namespace OrderDeliveryMonitor.Api.Controllers.Operation.Order
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IHubContext<OrderDeliveryMonitorHub> _hubContext;

        public OrderController(IHubContext<OrderDeliveryMonitorHub> hubContext)
        {
            _hubContext = hubContext;
        }

        // GET: api/Order
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Order/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
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
