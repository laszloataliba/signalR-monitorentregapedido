using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using OrderDeliveryMonitor.Api.Hubs;
using OrderDeliveryMonitor.Facade.Interface.Operation;
using OrderDeliveryMonitor.Model.Operation;
using OrderDeliveryMonitor.Utility;
using System;
using System.Linq;
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
        /// Recovers the orders according to given parameters.
        /// </summary>
        /// <param name="pProcess">Process that requested query must follow to recover an order list.</param>
        /// <param name="pPagination">Pagination parameters.</param>
        /// <returns>Order list.</returns>
        [HttpGet]
        public IActionResult Get(
            EOrderProcess pProcess = EOrderProcess.Awaiting, 
            [FromQuery] Pagination pPagination = null)
        {
            try
            {
                var vOrders = fOrder.GetListOrderDTO(
                        pInclude: itm => itm.Items,
                        pPagination: pPagination
                    );

                if (vOrders == null || vOrders.Count() == 0 || (pPagination != null && (pPagination.CurrentPage > pPagination.TotalPages)))
                    return NotFound();

                if (pPagination != null)
                    Response.Headers.Add($"X-{nameof(Pagination)}", JsonConvert.SerializeObject(pPagination));

                return Ok(vOrders);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Recovers the order data by given identifier.
        /// </summary>
        /// <param name="pOrderId">Order identifier.</param>
        /// <returns>Order data.</returns>
        [HttpGet("{pOrderId}")]
        public IActionResult Get(string pOrderId)
        {
            try
            {
                var vOrder = fOrder.GetOrderDTO(order => order.OrderId == int.Parse(pOrderId), items => items.Items);

                return Ok(vOrder);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Changes order status.
        /// </summary>
        /// <param name="pOrderId">Order identifier.</param>
        /// <returns></returns>
        [HttpPut("{pOrderId}")]
        public async Task<IActionResult> Put(string pOrderId)
        {
            try
            {
                fOrder.ToAwaiting(new Order { OrderId = int.Parse(pOrderId) });

                await _hubContext.Clients.All.SendAsync($"{AppUtilities.RELOAD_AWAITING_CONTAINER}");

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        #endregion :: Actions ::
    }
}
