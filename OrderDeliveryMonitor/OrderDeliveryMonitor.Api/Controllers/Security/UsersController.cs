using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OrderDeliveryMonitor.Api.Hubs;
using System.Collections.Generic;

namespace OrderDeliveryMonitor.Api.Controllers.Security
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/Security/Users"),
     ApiController]
    public class UsersController : ControllerBase
    {
        #region :: Fields ::

        /// <summary>
        /// 
        /// </summary>
        private readonly IHubContext<OrderDeliveryMonitorHub> _hubContext;

        #endregion :: Fields ::

        #region :: Methods ::

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hubContext"></param>
        public UsersController(IHubContext<OrderDeliveryMonitorHub> hubContext)
        {
            _hubContext = hubContext;
        }

        #endregion :: Methods ::

        #region :: Actions ::

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        #endregion :: Actions ::
    }
}
