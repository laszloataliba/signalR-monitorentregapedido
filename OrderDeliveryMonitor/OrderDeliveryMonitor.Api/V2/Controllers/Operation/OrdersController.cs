using Microsoft.AspNetCore.Mvc;

namespace OrderDeliveryMonitor.Api.V2.Controllers.Operation
{
    [ApiController, 
     Route("api/v{version:apiVersion}/Operation/[controller]"), 
     ApiVersion("2.0")]
    public class OrdersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> GetAll()
        {
            return "Versão 2.0";
        }

        [HttpGet("{pOrderCode}")]
        public ActionResult<string> Get(string pOrderCode)
        {
            return $"Get - Version 2.0 [{pOrderCode}]";
        }
    }
}