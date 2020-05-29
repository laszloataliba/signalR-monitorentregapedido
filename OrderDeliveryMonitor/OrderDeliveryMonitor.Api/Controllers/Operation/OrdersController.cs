using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using OrderDeliveryMonitor.Api.Hubs;
using OrderDeliveryMonitor.Facade.Interface.Operation;
using OrderDeliveryMonitor.Model.Operation;
using OrderDeliveryMonitor.Resources;
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
        public async Task<IActionResult> Get(
            EOrderProcess pProcess = EOrderProcess.None,
            [FromQuery] Pagination pPagination = null)
        {
            try
            {
                var vOrders = await fOrder.GetListOrderDTO(
                        pWhereClause: order => (order.Process > 0 && (order.Process == pProcess || pProcess == EOrderProcess.None)),
                        pInclude: itm => itm.Items,
                        pPagination: pPagination
                    );

                if (vOrders == null || vOrders.Count() == 0 || (pPagination != null && (pPagination.CurrentPage > pPagination.TotalPages)))
                    return NotFound(new { ErrorMessage = Resource.MSG_RECORDS_NOT_FOUND });

                if (pPagination != null)
                    Response.Headers.Add($"X-{nameof(Pagination)}", JsonConvert.SerializeObject(pPagination));

                return Ok(vOrders);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, new { ErrorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return
                    StatusCode
                    (
                        StatusCodes.Status500InternalServerError,
                        new
                        {
                            ErrorMessage = ex.Message
                        }
                    );
            }
        }

        /// <summary>
        /// Recovers the order data by given identifier.
        /// </summary>
        /// <param name="pOrderCode">Order identifier.</param>
        /// <returns>Order data.</returns>
        [HttpGet("{pOrderCode}")]
        public async Task<IActionResult> Get(string pOrderCode)
        {
            try
            {
                var vOrder = await fOrder.GetOrderDTO(order => order.OrderCode == pOrderCode, items => items.Items);

                return Ok(vOrder);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, new { ErroMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return
                    StatusCode
                    (
                        StatusCodes.Status500InternalServerError,
                        new
                        {
                            ErrorMessage = ex.Message
                        }
                    );
            }
        }

        /// <summary>
        /// Changes order status.
        /// </summary>
        /// <param name="pOrderCode">Order identifier.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Order pOrderCode)
        {
            try
            {
                await fOrder.ToAwaiting(new Order { OrderCode = pOrderCode.OrderCode });

                await _hubContext.Clients.All.SendAsync($"{Utilities.RELOAD_AWAITING_CONTAINER}");

                return Ok();
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, new { ErrorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return
                    StatusCode
                    (
                        StatusCodes.Status500InternalServerError,
                        new
                        {
                            ErrorMessage = ex.Message
                        }
                    );
            }
        }

        #endregion :: Actions ::
    }
}
