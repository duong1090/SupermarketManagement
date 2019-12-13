using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupermarketManagement.Data;
using SupermarketManagement.Extensions;
using SupermarketManagement.Models;

namespace SupermarketManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        int codeConfirm = new Random().Next(1000, 9999);

        private readonly ApplicationDbContext _db;

        public AccountController(ApplicationDbContext db)
        {
            _db = db;
        }

        //Action vào view của trang Login
        public IActionResult Login()
        {
            
            return View();
        }

        //Action Logout tài khoản
        public IActionResult Logout()
        {
            HttpContext.Session.Set<int>("sssUserID", 0);
            return View();
        }

        //Action vào view để nhập code từ mail được gửi để thay đổi mật khẩu
        public IActionResult GetCodeFromEmail()
        {

            return View();
        }

        //Action vào view thay đổi mật khẩu
        public IActionResult ChangePassWord()
        {

            return View();
        }

        //Action yêu cầu thay đổi mật khẩu, sau đó sẽ gửi một mail đến email của tài khoản đó để xác nhận
        public ActionResult ChangePass(User user)
        {
            HttpContext.Session.Set<int>("sssStaffID", user.StaffID);
            var _user = _db.Users.Where(s => s.StaffID == user.StaffID);
            if (_user.Any())
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("99hungthinh17@gmail.com");
                string email = _db.Staffs.Where(s => s.StaffID == user.StaffID).Select(s => s.Email).FirstOrDefault();

                mail.To.Add(email);
                mail.Subject = "Đổi mật khẩu";
                mail.Body = codeConfirm.ToString();
                HttpContext.Session.Set<int>("sssCode", codeConfirm);
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("99hungthinh17", "Koieuai1712");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                return Json(new { status = true, message = "Check your mail to get code" });
            }
            else
            {
                return Json(new { status = false, message = "Invalid ID!" });
            }
           
        }

        //Action kiểm tra tài khoản đăng nhập có đúng hay không
        public ActionResult Validate (User user)
        {
            var _user = _db.Users.Where(s => s.StaffID == user.StaffID);
            if (_user.Any())
            {
                if (_user.Where(s => s.PassWord == user.PassWord).Any())
                {
                    int tempID = _user.Select(s => s.StaffID).FirstOrDefault();
                    int RoleID = _db.Staffs.Where(s => s.StaffID == tempID).Select(s => s.RoleID).FirstOrDefault();
                    HttpContext.Session.Set("sssUserID", tempID);
                    return Json(new { status = true, message = RoleID.ToString() });
                }
                else
                {

                    return Json(new { status = false, message = "Invalid Password!" });
                }
            }
            else
            {
                return Json(new { status = false, message = "Invalid ID!" });
            }

        }

        //Action kiểm tra code nhập có giống với code từ mail hay không
        public IActionResult GetCode(string code)
        {
            if (code == HttpContext.Session.Get<int>("sssCode").ToString())
                return Json(new { status = true, message = "Successful!" });
            else
                return Json(new { status = false, message = "Invalid Code" });

        }

        //Action kiểm tra mật khẩu nhập vào có đúng hay không
        public IActionResult CheckPass(string newPassword, string confirmedPassword)
        {
            if (newPassword==confirmedPassword)
            {
                var _user = _db.Users.Where(s => s.StaffID == HttpContext.Session.Get<int>("sssStaffID")).FirstOrDefault();
                _user.PassWord = newPassword;
                _db.SaveChanges();
                return Json(new { status = true, message = "Successful!" });
            }
            else
                return Json(new { status = false, message = "Invalid Code" });

        }

    }
}