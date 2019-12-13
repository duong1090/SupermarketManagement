using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupermarketManagement.Areas.Admin.Controllers;
using SupermarketManagement.Data;
using SupermarketManagement.Models.ViewModel;

namespace SupermarketManagement.Areas.SaleStaff.Controllers
{
    [Area("SaleStaff")]
    public class CustomersController : BaseController
    {
        private readonly ApplicationDbContext _db;
        public CustomersController(ApplicationDbContext db)
        {
            _db = db;
           
        }
        public async Task<IActionResult> Index(string searchName,string searchID)
        {
            var customers = from c in _db.Customers
                           select c;

            if (!String.IsNullOrEmpty(searchName))
            {
                customers = customers.Where(s => s.FullName.Contains(searchName));
            }
            if (!String.IsNullOrEmpty(searchID))
            {
                customers = customers.Where(s => (s.CustomerID).ToString().Contains(searchID));
            }


            return View(await customers.ToListAsync());
        }     
       
    }
}