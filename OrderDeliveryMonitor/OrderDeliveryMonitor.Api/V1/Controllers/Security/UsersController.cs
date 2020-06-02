using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OrderDeliveryMonitor.Api.Hubs;

namespace OrderDeliveryMonitor.Api.V1.Controllers.Security
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController,
     ApiVersion("1.0"),
     Produces("application/json", "application/xml"),
     Route("api/v{version:apiVersion}/Security/[controller]")]
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

        #endregion :: Actions ::
    }
}
