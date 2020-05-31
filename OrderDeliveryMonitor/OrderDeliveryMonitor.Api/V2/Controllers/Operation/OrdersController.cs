using Microsoft.AspNetCore.Mvc;

namespace OrderDeliveryMonitor.Api.V2.Controllers.Operation
{
    [ApiController, 
     ApiVersion("2.0"),
     Route("api/v{version:apiVersion}/Operation/[controller]")]
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