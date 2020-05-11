using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderDeliveryMonitor.ApplicationConfig;
using OrderDeliveryMonitor.Model.Operation;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace OrderDeliveryMonitor.Areas.Cockpit.Controllers
{
    [Area(nameof(Cockpit))]
    public class CustomerDashboardController : Controller
    {
        // GET: CustomerDashboard
        public ActionResult Index()
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri($"{AppUtilities.WEBAPISERVERPATH}/api/Operation/Order");
            var result = httpClient.GetStringAsync(httpClient.BaseAddress);
            var content = JsonConvert.DeserializeObject<List<Order>>(result.Result);

            return View(content);
        }
    }
}