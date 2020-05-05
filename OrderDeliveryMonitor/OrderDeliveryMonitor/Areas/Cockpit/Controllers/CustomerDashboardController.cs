using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderDeliveryMonitor.Areas.Cockpit.Controllers
{
    [Area(nameof(Cockpit))]
    public class CustomerDashboardController : Controller
    {
        // GET: CustomerDashboard
        public ActionResult Index()
        {
            return View();
        }

        // GET: CustomerDashboard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerDashboard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerDashboard/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerDashboard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerDashboard/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerDashboard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerDashboard/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}