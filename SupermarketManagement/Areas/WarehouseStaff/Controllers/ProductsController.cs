using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SupermarketManagement.Areas.Admin.Controllers;
using SupermarketManagement.Data;
using SupermarketManagement.Models;
using SupermarketManagement.Models.ViewModel;

namespace SupermarketManagement.Areas.WarehouseStaff.Controllers
{
    [Area("WarehouseStaff")]
    public class ProductsController : BaseController
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

        public IActionResult Create()
        {
            return View(ProductsVM);
        }
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatPost()
        {
            if (!ModelState.IsValid)
            {
                return View(ProductsVM);
            }
            _db.Products.Add(ProductsVM.Products);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProductsVM.Products = await _db.Products.Include(m => m.Category).SingleOrDefaultAsync(m => m.ProductID == id);
            if (ProductsVM.Products == null)
            {
                return NotFound();
            }
            return View(ProductsVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (ModelState.IsValid)
            {
                var productFromDb = _db.Products.Where(m => m.ProductID == ProductsVM.Products.ProductID).FirstOrDefault();
                productFromDb.Name = ProductsVM.Products.Name;
                productFromDb.Price = ProductsVM.Products.Price;
                productFromDb.Status = ProductsVM.Products.Status;
                productFromDb.CategoryID = ProductsVM.Products.CategoryID;
                productFromDb.Amount = ProductsVM.Products.Amount;
                productFromDb.Producer = ProductsVM.Products.Producer;
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(ProductsVM);
            }

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProductsVM.Products = await _db.Products.Include(m => m.Category).SingleOrDefaultAsync(m => m.ProductID == id);
            if (ProductsVM.Products == null)
            {
                return NotFound();
            }
            return View(ProductsVM);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProductsVM.Products = await _db.Products.Include(m => m.Category).SingleOrDefaultAsync(m => m.ProductID == id);
            if (ProductsVM.Products == null)
            {
                return NotFound();
            }
            return View(ProductsVM);
        }


        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _db.Products.FindAsync(id);
            _db.Products.Remove(products);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}