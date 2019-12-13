using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SupermarketManagement.Data;
using SupermarketManagement.Models;

namespace SupermarketManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : BaseController
    {
        private readonly ApplicationDbContext _db;
        public CategoriesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Categories.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Add(category);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var categoryName = await _db.Categories.FindAsync(id);
            if (categoryName == null)
            {
                return NotFound();
            }
            return View(categoryName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.CategoryID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Update(category);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            var categoryName = await _db.Categories.FindAsync(id);
            if (categoryName == null)
            {
                return NotFound();
            }
            return View(categoryName);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var categoryName = await _db.Categories.FindAsync(id);
            if (categoryName == null)
            {
                return NotFound();
            }
            return View(categoryName);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryNames = await _db.Categories.FindAsync(id);
            _db.Categories.Remove(categoryNames);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}