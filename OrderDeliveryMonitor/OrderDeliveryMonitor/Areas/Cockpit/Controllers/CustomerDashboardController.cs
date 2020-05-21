using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderDeliveryMonitor.Facade.Implementation.Operation.DTO;
using OrderDeliveryMonitor.Utility;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrderDeliveryMonitor.Areas.Cockpit.Controllers
{
    [Area(nameof(Cockpit))]
    public class CustomerDashboardController : Controller
    {
        // GET: CustomerDashboard
        public async Task<ActionResult> Index()
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri($"{AppUtilities.WEB_API_SERVER_PATH}/api/Operation/Order");
            var result = await httpClient.GetStringAsync(httpClient.BaseAddress);
            var content = JsonConvert.DeserializeObject<List<OrderDTO>>(result);

            return View(content);
        }
    }
}