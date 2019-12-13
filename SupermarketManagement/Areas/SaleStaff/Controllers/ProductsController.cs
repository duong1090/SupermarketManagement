using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SupermarketManagement.Data;
using SupermarketManagement.Models;
using SupermarketManagement.Models.ViewModel;

namespace SupermarketManagement.Areas.SaleStaff.Controllers
{
    [Area("SaleStaff")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ProductsViewModel ProductsVM { get; set; }
        public ProductsController(ApplicationDbContext db)
        {
            _db = db;
            ProductsVM = new ProductsViewModel()
            {
                Categories = _db.Categories.ToList(),
                Products = new Models.Product()
            };
        }

        public async Task<IActionResult> Index(string searchString,string searchID)
        {
            var products = from p in _db.Products
                         select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(searchID))
            {
                products = products.Where(s => (s.ProductID).ToString().Contains(searchID));
            }


            return View(await products.ToListAsync());
        }

       

    }
}