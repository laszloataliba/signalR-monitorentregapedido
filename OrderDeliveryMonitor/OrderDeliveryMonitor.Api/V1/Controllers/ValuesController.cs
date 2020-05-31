using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OrderDeliveryMonitor.Api.Hubs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderDeliveryMonitor.Api.V1.Controllers
{
    [ApiController,
     Route("api/v{version:apiVersion}/[controller]"),
     ApiVersion("1.0")]
    public class ValuesController : ControllerBase
    {
        private readonly IHubContext<OrderDeliveryMonitorHub> _hubContext;

        public ValuesController(IHubContext<OrderDeliveryMonitorHub> hubContext)
        {
            _hubContext = hubContext;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task Get(int id)
        {
            var parametro = $"Teste consumindo a API diretamente {id:d2}.".Split();

            await _hubContext.Clients.All.SendAsync("TesteViaWebAPI", parametro);

        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
