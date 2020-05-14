using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderDeliveryMonitor.ApplicationConfig;
using OrderDeliveryMonitor.Model.Operation;

namespace OrderDeliveryMonitor.Areas.Cockpit.Controllers
{
    [Area(nameof(Cockpit))]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri($"{AppUtilities.WEB_API_SERVER_PATH}/api/Operation/Order");
            var result = httpClient.GetStringAsync(httpClient.BaseAddress);
            var content = JsonConvert.DeserializeObject<List<Order>>(result.Result);

            return View(content);
        }
    }
}