using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OrderDeliveryMonitor.Api.Hubs;

namespace OrderDeliveryMonitor.Api.Controllers.Security
{
    [Route("api/Security/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IHubContext<OrderDeliveryMonitorHub> _hubContext;

        public UsersController(IHubContext<OrderDeliveryMonitorHub> hubContext)
        {
            _hubContext = hubContext;
        }

        // GET: api/User
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/User/5
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
