using BoatRent.Core.Services;
using BoatRent.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BoatRent.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RentService _rentService;

        public HomeController(ILogger<HomeController> logger, RentService rentService)
        {
            _logger = logger;
            _rentService = rentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RentBoat()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RentBoat(RentBoatModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _rentService.RentBoat(model.BoatNumber, model.BoatType, model.BookingNumber, model.CustomerNumber, model.StartDate);
                if (result.IsSucceed)
                {
                    ModelState.Clear();
                    ViewBag.Message = "Boat rent has been registered successfully";
                }
                else
                {
                    ViewBag.Message = result.Message;
                }

            }
            return View();
        }

        public IActionResult ReturnBoat()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnBoat(ReturnBoatModel model)
        {
            if (ModelState.IsValid)
            {
                var receipt = await _rentService.ReturnBoat(model.BoatNumber, model.EndDate);
                if(receipt != null)
                {
                    var receiptModel = new ReceiptModel
                    {
                        EndDate = receipt.EndDate,
                        BoatNumber = receipt.BoatNumber,
                        BoatType = receipt.BoatType,
                        BookingNumber = receipt.BookingNumber,
                        CustomerNumber = receipt.CustomerNumber,
                        StartDate = receipt.StartDate,
                        Price = receipt.Price
                    };
                    return RedirectToAction("ShowReceipt", "Home", receiptModel);
                }
            }
            return View();
        }

        public IActionResult ShowReceipt(ReceiptModel model)
        {
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
