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

namespace OrderDeliveryMonitor.Api.V1.Controllers.Operation
{
    /// <summary>
    /// API to handling monitored orders.
    /// </summary>
    [ApiController,
     ApiVersion("1.0"),
     Produces("application/json", "application/xml"),
     Route("api/v{version:apiVersion}/Operation/[controller]")]
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
        /// <response code="200">When api works correctly.</response>
        /// <response code="404">When orders were not found.</response>
        /// <response code="500">If an unexpected error occurs.</response>
        [HttpGet,
         Consumes("application/json"),
         ProducesResponseType(StatusCodes.Status200OK),
         ProducesResponseType(StatusCodes.Status404NotFound),
         ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(
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
        /// <response code="200">When api works correctly.</response>
        /// <response code="400">If an invalid identifier is given.</response>
        /// <response code="404">When order were not found.</response>
        /// <response code="500">If an unexpected error occurs.</response>
        [HttpGet("{pOrderCode}"),
         Consumes("application/json"),
         ProducesResponseType(StatusCodes.Status200OK),
         ProducesResponseType(StatusCodes.Status400BadRequest),
         ProducesResponseType(StatusCodes.Status404NotFound),
         ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        /// Performs the check-in for the order according to the identifier provided.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /Orders
        ///     {
        ///        "OrderCode": "xxxxxxxxxx"
        ///     }
        /// </remarks>
        /// <param name="pOrderCode">Order identifier.</param>
        /// <returns>No content.</returns>
        /// <response code="204">When api works correctly.</response>
        /// <response code="400">If an invalid identifier is given.</response>
        /// <response code="404">When order were not found.</response>
        /// <response code="500">If an unexpected error occurs.</response>
        [HttpPut,
         Consumes("application/json"),
         ProducesResponseType(StatusCodes.Status204NoContent),
         ProducesResponseType(StatusCodes.Status400BadRequest),
         ProducesResponseType(StatusCodes.Status404NotFound),
         ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutInLine([FromBody] Order pOrderCode)
        {
            try
            {
                await fOrder.ToAwaiting(new Order { OrderCode = pOrderCode.OrderCode });

                await _hubContext.Clients.All.SendAsync($"{Utilities.RELOAD_AWAITING_CONTAINER}");

                return NoContent();
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
