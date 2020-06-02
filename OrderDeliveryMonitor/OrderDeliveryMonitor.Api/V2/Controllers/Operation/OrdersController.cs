//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using OrderDeliveryMonitor.Model.Operation;
//using OrderDeliveryMonitor.Utility;

//namespace OrderDeliveryMonitor.Api.V2.Controllers.Operation
//{
//    /// <summary>
//    /// API to handling monitored orders.
//    /// </summary>
//    [ApiController, 
//     ApiVersion("2.0"),
//     Produces("application/json", "application/xml"),
//     Route("api/v{version:apiVersion}/Operation/[controller]")]
//    public class OrdersController : ControllerBase
//    {
//        /// <summary>
//        /// Recovers the orders according to given parameters.
//        /// </summary>
//        /// <param name="pProcess">Process that requested query must follow to recover an order list.</param>
//        /// <param name="pPagination">Pagination parameters.</param>
//        /// <returns>Order list.</returns>
//        /// <response code="200">When api works correctly.</response>
//        /// <response code="404">When orders were not found.</response>
//        /// <response code="500">If an unexpected error occurs.</response>
//        [HttpGet,
//         Consumes("application/json"),
//         ProducesResponseType(StatusCodes.Status200OK),
//         ProducesResponseType(StatusCodes.Status404NotFound),
//         ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public IActionResult GetAll(
//            EOrderProcess pProcess = EOrderProcess.None,
//            [FromQuery] Pagination pPagination = null)
//        {
//            return Ok("Versão 2.0");
//        }

//        /// <summary>
//        /// Recovers the order data by given identifier.
//        /// </summary>
//        /// <param name="pOrderCode">Order identifier.</param>
//        /// <returns>Order data.</returns>
//        /// <response code="200">When api works correctly.</response>
//        /// <response code="400">If an invalid identifier is given.</response>
//        /// <response code="404">When order were not found.</response>
//        /// <response code="500">If an unexpected error occurs.</response>
//        [HttpGet("{pOrderCode}"),
//         Consumes("application/json"),
//         ProducesResponseType(StatusCodes.Status200OK),
//         ProducesResponseType(StatusCodes.Status400BadRequest),
//         ProducesResponseType(StatusCodes.Status404NotFound),
//         ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public IActionResult Get(string pOrderCode)
//        {
//            return Ok($"Get - Version 2.0 [{pOrderCode}]");
//        }
//    }
//}