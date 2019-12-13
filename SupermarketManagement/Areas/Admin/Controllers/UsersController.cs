using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupermarketManagement.Data;
using SupermarketManagement.Models;

namespace SupermarketManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : BaseController
    {
        private readonly ApplicationDbContext _db;

       
        public UsersController(ApplicationDbContext db)
        {
            _db = db;         
        }
        public async Task<IActionResult> Index()
        {
            var users = from u in _db.Users
                        select new User
                        {
                            StaffID = u.StaffID,
                            ID = u.ID,
                            PassWord = u.PassWord,
                            Staff =u.Staff
                        };
            return View(await users.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken, ActionName("Create")]
        public async Task<IActionResult> CreatePost(User user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var _user = _db.Users.Where(s => s.StaffID == user.StaffID);
            var _staff = _db.Staffs.Where(s => s.StaffID == user.StaffID);
            if (!_user.Any())
            {
                if (_staff.Any())
                {
                    _db.Users.Add(user);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(user);
                }
            }
            return View(user);
        }
        public IActionResult Validate(User user)
        {          
            var _user = _db.Users.Where(s => s.StaffID == user.StaffID);
            var _staff = _db.Staffs.Where(s => s.StaffID == user.StaffID);
            if (!_user.Any())
            {
                if (_staff.Any())
                    return Json(new { status = true, message = "Sucessful" });
                else
                    return Json(new { status = false, message = "This user has already existed or This Staff has not existed yet" });
            }

            else
            {
                return Json(new { status = false, message = "This user has already existed or This Staff has not existed yet" });
            }
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var user = _db.Users.Where(s => s.StaffID == id).Select(s => s).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != user.StaffID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var userFromDb = _db.Users.Where(s => s.StaffID == user.StaffID).Select(s => s).FirstOrDefault();
                userFromDb.PassWord = user.PassWord;
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();
            var user = (from u in _db.Users
                       where u.StaffID==id
                       select new User
                       {
                           StaffID = u.StaffID,
                           ID = u.ID,
                           PassWord = u.PassWord,
                           Staff = u.Staff
                       }).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var user = (from u in _db.Users
                        where u.StaffID == id
                        select new User
                        {
                            StaffID = u.StaffID,
                            ID = u.ID,
                            PassWord = u.PassWord,
                            Staff = u.Staff
                        }).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = _db.Users.Where(s => s.StaffID == id).Select(s => s).FirstOrDefault();
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }

}
