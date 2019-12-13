using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupermarketManagement.Data;
using SupermarketManagement.Models.ViewModel;

namespace SupermarketManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomersController : BaseController
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public CustomersViewModel CustomersVM { get; set; }
        public CustomersController(ApplicationDbContext db)
        {
            _db = db;
            CustomersVM = new CustomersViewModel()
            {
                Customers = new Models.Customer()
            };
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


        public IActionResult Create()
        {
            return View(CustomersVM);
        }
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatPost()
        {
            if (!ModelState.IsValid)
            {
                return View(CustomersVM);
            }
            _db.Customers.Add(CustomersVM.Customers);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CustomersVM.Customers = await _db.Customers.FindAsync(id);
            if (CustomersVM.Customers == null)
            {
                return NotFound();
            }
            return View(CustomersVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (ModelState.IsValid)
            {
                var customerFromDb = _db.Customers.Where(m => m.CustomerID == CustomersVM.Customers.CustomerID).FirstOrDefault();
                customerFromDb.FullName = CustomersVM.Customers.FullName;
                customerFromDb.PhoneNumber = CustomersVM.Customers.PhoneNumber;
                customerFromDb.Sex = CustomersVM.Customers.Sex;
                customerFromDb.Point = CustomersVM.Customers.Point;
                customerFromDb.Rate = CustomersVM.Customers.Rate;
                customerFromDb.Address = CustomersVM.Customers.Address;

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(CustomersVM);
            }

        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CustomersVM.Customers = await _db.Customers.FindAsync(id);
            if (CustomersVM.Customers == null)
            {
                return NotFound();
            }
            return View(CustomersVM);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CustomersVM.Customers = await _db.Customers.FindAsync(id);
            if (CustomersVM.Customers == null)
            {
                return NotFound();
            }
            return View(CustomersVM);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customers = await _db.Customers.FindAsync(id);
            _db.Customers.Remove(customers);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}