using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OrderDeliveryMonitor.Api.Hubs;
using OrderDeliveryMonitor.Facade.Implementation.Operation.DTO;
using OrderDeliveryMonitor.Facade.Interface.Operation;
using OrderDeliveryMonitor.Model.Operation;
using OrderDeliveryMonitor.Utility;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderDeliveryMonitor.Api.Controllers.Operation
{
    /// <summary>
    /// Order controller.
    /// </summary>
    [Route("api/Operation/Orders"),
     ApiController]
    public class OrdersController : ControllerBase
    {
        #region :: Fields ::

        /// <summary>
        /// Hub access object.
        /// </summary>
        private readonly IHubContext<OrderDeliveryMonitorHub> _hubContext;

        /// <summary>
        /// Order model facade object.
        /// </summary>
        private readonly IFOrder fOrder;

        #endregion :: Fields ::

        #region :: Methods ::

        /// <summary>
        /// Constructor method.
        /// </summary>
        /// <param name="hubContext">Hub access obejct parameter.</param>
        /// <param name="pFOrder">Order model facade parameter.</param>
        public OrdersController(IHubContext<OrderDeliveryMonitorHub> hubContext, IFOrder pFOrder)
        {
            _hubContext = hubContext;
            fOrder = pFOrder;
        }

        #endregion :: Methods ::

        #region :: Actions ::

        /// <summary>
        /// Recovers the orders that is on waiting status.
        /// </summary>
        /// <returns>Order list.</returns>
        [HttpGet]
        public IEnumerable<OrderDTO> Get()
        {
            var vOrders = fOrder.GetListOrderDTO(pInclude: itm => itm.Items);

            return vOrders;
        }

        /// <summary>
        /// Recovers the order data by given identifier.
        /// </summary>
        /// <param name="pOrderId">Order identifier.</param>
        /// <returns>Order data.</returns>
        [HttpGet("{pOrderId}")]
        public OrderDTO Get(string pOrderId)
        {
            var vOrder = fOrder.GetOrderDTO(order => order.OrderId == int.Parse(pOrderId), items => items.Items);

            return vOrder;
        }

        /// <summary>
        /// Changes order status.
        /// </summary>
        /// <param name="pOrderId">Order identifier.</param>
        /// <returns></returns>
        [HttpPut("{pOrderId}")]
        public async Task Put(string pOrderId)
        {
            fOrder.ToAwaiting(new Order { OrderId = int.Parse(pOrderId) });

            await _hubContext.Clients.All.SendAsync($"{AppUtilities.RELOAD_AWAITING_CONTAINER}");
        }

        #endregion :: Actions ::
    }
}
