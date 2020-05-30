using Microsoft.AspNetCore.Mvc;
using OrderDeliveryMonitor.Services.Operation.Interface;
using System.Threading.Tasks;

namespace OrderDeliveryMonitor.Areas.Cockpit.Controllers
{
    [Area(nameof(Cockpit))]
    public class CustomerDashboardController : Controller
    {
        private readonly IOrderService _orderService;

        public CustomerDashboardController(IOrderService pOrderService)
        {
            _orderService = pOrderService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _orderService.GetAll());
        }
    }
}