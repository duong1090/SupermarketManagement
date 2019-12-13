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
    public class RolesController : BaseController
    {
        private readonly ApplicationDbContext _db;
        public RolesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Roles.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Role Role)
        {
            if (ModelState.IsValid)
            {
                _db.Add(Role);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Role);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var roleName = await _db.Roles.FindAsync(id);
            if (roleName == null)
            {
                return NotFound();
            }
            return View(roleName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Role role)
        {
            if (id != role.RoleID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Update(role);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            var roleName = await _db.Roles.FindAsync(id);
            if (roleName == null)
            {
                return NotFound();
            }
            return View(roleName);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var roleName = await _db.Roles.FindAsync(id);
            if (roleName == null)
            {
                return NotFound();
            }
            return View(roleName);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roleName = await _db.Roles.FindAsync(id);
            _db.Roles.Remove(roleName);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}