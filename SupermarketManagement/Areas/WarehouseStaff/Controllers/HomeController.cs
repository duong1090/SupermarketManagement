using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SupermarketManagement.Areas.Admin.Controllers;
using SupermarketManagement.Data;

namespace SupermarketManagement.Areas.WarehouseStaff.Controllers
{
    [Area("WarehouseStaff")]
    public class HomeController : BaseController
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            return View();

        }
    }
}