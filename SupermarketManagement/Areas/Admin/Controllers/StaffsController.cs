using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupermarketManagement.Data;
using SupermarketManagement.Extensions;
using SupermarketManagement.Models.ViewModel;
using SupermarketManagement.Utility;

namespace SupermarketManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StaffsController : BaseController
    {
        private readonly ApplicationDbContext _db;
        private readonly HostingEnvironment _hostingEnvironment;

        [BindProperty]
        public StaffsViewModel StaffsVM { get; set; }
        public StaffsController(ApplicationDbContext db, HostingEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            StaffsVM = new StaffsViewModel()
            {
                Roles = _db.Roles.ToList(),
                Staffs = new Models.Staff()
            };
        }

        public async Task<IActionResult> Index(string searchName,string searchID)
        {
            var staffs = from p in _db.Staffs
                           select p;

            if (!String.IsNullOrEmpty(searchName))
            {
                staffs = staffs.Where(s => s.FullName.Contains(searchName));
            }
            if (!String.IsNullOrEmpty(searchID))
            {
                staffs = staffs.Where(s => (s.StaffID).ToString().Contains(searchID));
            }


            return View(await staffs.ToListAsync());
        }

        public IActionResult Create()
        {
            return View(StaffsVM);
        }
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatPost()
        {
            if (!ModelState.IsValid)
            {
                return View(StaffsVM);
            }
            _db.Staffs.Add(StaffsVM.Staffs);
            await _db.SaveChangesAsync();

            //Image

            string webRootPath = _hostingEnvironment.WebRootPath;
            var file = HttpContext.Request.Form.Files;

            var staffFromDB = _db.Staffs.Find(StaffsVM.Staffs.StaffID);

            if (file.Count!=0)
            {
                var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                var extension = Path.GetExtension(file[0].FileName);

                

                using(var fileStream = new FileStream(Path.Combine(uploads,StaffsVM.Staffs.StaffID + extension),FileMode.Create))
                {
                    file[0].CopyTo(fileStream);
                }
                staffFromDB.Image = @"\" + SD.ImageFolder + @"\" + StaffsVM.Staffs.StaffID + extension;

            }
            else
            {
                var uploads = Path.Combine(webRootPath, SD.ImageFolder + @"\" + SD.DefaultStaffImage);
                System.IO.File.Copy(uploads, webRootPath + @"\" + SD.ImageFolder + @"\" + StaffsVM.Staffs.StaffID + ".jpg");
                staffFromDB.Image = @"\" + SD.ImageFolder + @"\" + StaffsVM.Staffs.StaffID + ".jpg";

            }
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            StaffsVM.Staffs = await _db.Staffs.Include(m => m.Role).SingleOrDefaultAsync(m => m.StaffID == id);
            if (StaffsVM.Staffs == null)
            {
                return NotFound();
            }
            return View(StaffsVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                var file = HttpContext.Request.Form.Files;
                var staffFromDB = _db.Staffs.Find(StaffsVM.Staffs.StaffID);
                if(file.Count>0 && file[0]!=null)
                {
                    var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                    var extension_new = Path.GetExtension(file[0].FileName);
                    var extension_old = Path.GetExtension(staffFromDB.Image);

                    if(System.IO.File.Exists(Path.Combine(uploads,StaffsVM.Staffs.StaffID+extension_old)))
                    {
                        System.IO.File.Delete(Path.Combine(uploads, StaffsVM.Staffs.StaffID + extension_old));
                    }
                    using (var fileStream = new FileStream(Path.Combine(uploads, StaffsVM.Staffs.StaffID + extension_new), FileMode.Create))
                    {
                        file[0].CopyTo(fileStream);
                    }
                    StaffsVM.Staffs.Image = @"\" + SD.ImageFolder + @"\" + StaffsVM.Staffs.StaffID + extension_new;

                }
                if (StaffsVM.Staffs.Image!=null)
                {
                    staffFromDB.Image = StaffsVM.Staffs.Image;
                }
                staffFromDB.FullName = StaffsVM.Staffs.FullName;
                staffFromDB.Sex = StaffsVM.Staffs.Sex;
                staffFromDB.PhoneNumber = StaffsVM.Staffs.PhoneNumber;
                staffFromDB.RoleID = StaffsVM.Staffs.RoleID;
                staffFromDB.BirthDay = StaffsVM.Staffs.BirthDay;
                staffFromDB.Address = StaffsVM.Staffs.Address;
                staffFromDB.Email = StaffsVM.Staffs.Email;
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));




            }
            else
            {
                return View(StaffsVM);
            }

        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            StaffsVM.Staffs = await _db.Staffs.Include(m => m.Role).SingleOrDefaultAsync(m => m.StaffID == id);
            if (StaffsVM.Staffs == null)
            {
                return NotFound();
            }
            return View(StaffsVM);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            StaffsVM.Staffs = await _db.Staffs.Include(m => m.Role).SingleOrDefaultAsync(m => m.StaffID == id);
            if (StaffsVM.Staffs == null)
            {
                return NotFound();
            }
            return View(StaffsVM);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staffs = await _db.Staffs.FindAsync(id);

            var users = _db.Users.Where(u => u.StaffID == id).Select(u => u).FirstOrDefault();
            _db.Staffs.Remove(staffs);
            _db.Users.Remove(users);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditProfile()
        {
            StaffsVM.Staffs = await _db.Staffs.Include(m => m.Role).SingleOrDefaultAsync(m => m.StaffID == HttpContext.Session.Get<int>("sssUserID"));
            if (StaffsVM.Staffs == null)
            {
                return NotFound();
            }
            return View(StaffsVM);
        }

        [HttpPost,ActionName("EditProfile")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfilePost()
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                var file = HttpContext.Request.Form.Files;
                var staffFromDB = _db.Staffs.Find(StaffsVM.Staffs.StaffID);
                if (file.Count > 0 && file[0] != null)
                {
                    var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                    var extension_new = Path.GetExtension(file[0].FileName);
                    var extension_old = Path.GetExtension(staffFromDB.Image);

                    if (System.IO.File.Exists(Path.Combine(uploads, StaffsVM.Staffs.StaffID + extension_old)))
                    {
                        System.IO.File.Delete(Path.Combine(uploads, StaffsVM.Staffs.StaffID + extension_old));
                    }
                    using (var fileStream = new FileStream(Path.Combine(uploads, StaffsVM.Staffs.StaffID + extension_new), FileMode.Create))
                    {
                        file[0].CopyTo(fileStream);
                    }
                    StaffsVM.Staffs.Image = @"\" + SD.ImageFolder + @"\" + StaffsVM.Staffs.StaffID + extension_new;

                }
                if (StaffsVM.Staffs.Image != null)
                {
                    staffFromDB.Image = StaffsVM.Staffs.Image;
                }
                staffFromDB.FullName = StaffsVM.Staffs.FullName;
                staffFromDB.Sex = StaffsVM.Staffs.Sex;
                staffFromDB.PhoneNumber = StaffsVM.Staffs.PhoneNumber;
                staffFromDB.RoleID = StaffsVM.Staffs.RoleID;
                staffFromDB.BirthDay = StaffsVM.Staffs.BirthDay;
                staffFromDB.Address = StaffsVM.Staffs.Address;
                staffFromDB.Email = StaffsVM.Staffs.Email;
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(StaffsVM);
            }

        }

    }
}