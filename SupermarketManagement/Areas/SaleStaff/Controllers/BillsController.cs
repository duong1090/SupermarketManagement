using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupermarketManagement.Areas.Admin.Controllers;
using SupermarketManagement.Data;
using SupermarketManagement.Extensions;
using SupermarketManagement.Models;
using SupermarketManagement.Models.ViewModel;

namespace SupermarketManagement.Areas.SaleStaff.Controllers
{
    [Area("SaleStaff")]
    public class BillsController : BaseController
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public BillDetailsModel BillDetailsVM { get; set; }
        public BillsController(ApplicationDbContext db)
        {
            _db = db;
            BillDetailsVM = new BillDetailsModel()
            {
                bill_detail = new Models.Bill_Detail(),
                bill_details = _db.Bill_Details.ToList()
            };
            
        }

        //Action vào view hiện tất các thông của các hoá đơn
        public async Task<IActionResult> Index()
        {
            var bills = from b in _db.Bills
                           select b;
            return View(await bills.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]


        //Action tạo mới một hoá đơn với mã nhân viên của nhân viên đang sử dụng và thời gian hiện tại  
        public async Task<IActionResult> CreatPost(Bill bill)
        {
            if (ModelState.IsValid)
            {
                bill.StaffID = HttpContext.Session.Get<int>("sssUserID");
                bill.Date = DateTime.Now;
                _db.Add(bill);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bill);

        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Bill bill = new Bill();
            bill = await _db.Bills.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            return View(bill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Bill bill)
        {
            if (id != bill.BillID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Update(bill);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bill);
        }


        //Action Vào view tạo một chi tiết hoá đơn
        public IActionResult CreateBillDetail(int id)
        {
            BillDetailsVM.bill_details = (from b in _db.Bill_Details
                                          where b.BillID == id
                                          select b);
            foreach (var item in BillDetailsVM.bill_details)
            {
                item.Product = _db.Products.Where(s => s.ProductID == item.ProductID).Select(s => s).FirstOrDefault();
            }

            return View(BillDetailsVM);
        }


        //Action tạo mới một chi tiết hoá đơn, cập nhật lại số lượng của sản phẩm và điểm tích luỹ của khách hàng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBillDetail(int id,Bill_Detail bill_Detail)
        {
            if (ModelState.IsValid)
            {
                BillDetailsVM.bill_detail.BillID = id;             
                BillDetailsVM.bill_detail.TotalPrice = _db.Products.Where(s => s.ProductID == BillDetailsVM.bill_detail.ProductID).Select(s => s.Price).FirstOrDefault() * BillDetailsVM.bill_detail.ProductAmount;
                _db.Add(BillDetailsVM.bill_detail);
                await _db.SaveChangesAsync();

                float sum = (from s in _db.Bill_Details
                             where s.BillID == id
                             select s.TotalPrice).Sum();
                var billFromDb = _db.Bills.Where(m => m.BillID == id).FirstOrDefault();
                billFromDb.TotalMoney = sum;
                await _db.SaveChangesAsync();

                int amount = (from s in _db.Bill_Details
                              where s.ProductID == BillDetailsVM.bill_detail.ProductID
                              select s.ProductAmount).LastOrDefault();

                var productFromDb = _db.Products.Where(s => s.ProductID == BillDetailsVM.bill_detail.ProductID).Select(s => s).FirstOrDefault();
                productFromDb.Amount = productFromDb.Amount - amount;

                var customerFromDb = _db.Customers.Where(m => m.CustomerID == BillDetailsVM.bill_detail.Bill.CustomerID).Select(s => s).FirstOrDefault();
                customerFromDb.Point = (int)((from c in _db.Bills
                                              where c.CustomerID == customerFromDb.CustomerID
                                              select c.TotalMoney).Sum()) / 10000;

                await _db.SaveChangesAsync();
              
            }        
            return RedirectToAction("CreateBillDetail");
        }


        //Action hiển thị tất cả các chi tiết hoá đơn
        public async Task<IActionResult> IndexBillDetail(int id)
        {

            var bills = from b in _db.Bill_Details
                        where b.BillID == id
                        group b by b.ProductID into g
                        select new Bill_Detail
                        {
                           
                            ProductID = g.First().ProductID,
                            TotalPrice = g.Sum(pc => pc.TotalPrice),
                            ProductAmount = g.Sum(pc => pc.ProductAmount),
                            Product =g.First().Product,                          
                        };
            return View(await bills.ToListAsync());
        }


        //Action vào view để chỉnh sửa một chi tiết hoá đơn
        public async Task<IActionResult> EditBillDetail(int id)
        {
            Bill_Detail bill_Detail = new Bill_Detail();
            bill_Detail = await _db.Bill_Details.FindAsync(id);
            if (bill_Detail == null)
            {
                return NotFound();
            }
            return View(bill_Detail);
        }

        //Action chỉnh sửa một chi tiết hoá đơn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBillDetail(int id, Bill_Detail bill_Detail)
        {
            if (id != bill_Detail.Bill_DetailID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {

                var billDetailFromDb = _db.Bill_Details.Where(m => m.Bill_DetailID == bill_Detail.Bill_DetailID).FirstOrDefault();
                int amount = bill_Detail.ProductAmount - billDetailFromDb.ProductAmount;

                billDetailFromDb.ProductAmount = bill_Detail.ProductAmount;
                billDetailFromDb.TotalPrice = _db.Products.Where(s => s.ProductID == bill_Detail.ProductID).Select(s => s.Price).FirstOrDefault() *bill_Detail.ProductAmount;

                await _db.SaveChangesAsync();

                var productFromDb = _db.Products.Where(s => s.ProductID == billDetailFromDb.ProductID).Select(s => s).FirstOrDefault();
                productFromDb.Amount = productFromDb.Amount - amount;
                await _db.SaveChangesAsync();

                float sum = (from s in _db.Bill_Details
                             where s.BillID == billDetailFromDb.BillID
                             select s.TotalPrice).Sum();
                var billFromDb = _db.Bills.Where(m => m.BillID == billDetailFromDb.BillID).FirstOrDefault();
                billFromDb.TotalMoney = sum;
                await _db.SaveChangesAsync();

                return View(bill_Detail);
            }
            else
            {
                return View(bill_Detail);
            }
            
        }
    }
}