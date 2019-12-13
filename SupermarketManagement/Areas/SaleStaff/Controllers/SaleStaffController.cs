using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SupermarketManagement.Areas.Admin.Controllers;
using SupermarketManagement.Data;
using SupermarketManagement.Extensions;

namespace SupermarketManagement.Areas.SaleStaff.Controllers
{
    [Area("SaleStaff")]
    public class SaleStaffController : BaseController
    {
        private readonly ApplicationDbContext _db;

        public SaleStaffController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            
                return View();
   
        }
    }
}