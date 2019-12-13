using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SupermarketManagement.Data;
using SupermarketManagement.Models;
using SupermarketManagement.Models.ViewModel;

namespace SupermarketManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatisticController : Controller
    {
        [BindProperty]
        StatisticViewModel statisticVM { get; set; }

        private readonly ApplicationDbContext _db;

        public StatisticController (ApplicationDbContext db)
        {
            _db = db;
            statisticVM = new StatisticViewModel
            {
                receipt_Detail = new Receipt_Detail(),
                receipt_Details = _db.Receipt_Details.ToList(),
                NumberOfMonth = 0,
                categoryID = 1,
            };
        }

        //Action vào view hiển thị các sản phẩm đã hết hạn
        public IActionResult Index()
        {
            statisticVM.receipt_Details = from a in _db.Receipt_Details
                                          where a.ExpiryDate < DateTime.Now
                                          select new Receipt_Detail
                                          {
                                              ProductID = a.ProductID,
                                              Amount = a.Amount,
                                              Product = a.Product,
                                              ManufactureDate = a.ManufactureDate,
                                              ExpiryDate = a.ExpiryDate
                                          };
          return View(statisticVM);
            
        }
    }
}