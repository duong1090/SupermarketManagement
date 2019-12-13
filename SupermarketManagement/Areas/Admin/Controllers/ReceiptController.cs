using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupermarketManagement.Data;
using SupermarketManagement.Extensions;
using SupermarketManagement.Models;
using SupermarketManagement.Models.ViewModel;

namespace SupermarketManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReceiptController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ReceiptDetailModel receiptDetailVM { get; set; }

        public ReceiptController(ApplicationDbContext db)
        {
            _db = db;

            receiptDetailVM = new ReceiptDetailModel()
            {
                Receipt_Detail = new Models.Receipt_Detail(),
                Receipt_Details = _db.Receipt_Details.ToList()
            };
        }

        //Action vào view hiển thị tất cả các phiếu nhập hàng
        public async Task<IActionResult> Index()
        {
            var receipts = from a in _db.ImportReceipts
                           select a;
            return View(await receipts.ToListAsync());
        }

        //Action vào view tạo mới một phiếu nhập hàng
        public async Task<IActionResult> Create(ImportReceipt importReceipt)
        {
            if (ModelState.IsValid)
            {
                importReceipt.StaffID = HttpContext.Session.Get<int>("sssUserID");
                importReceipt.Date = DateTime.Now;
                _db.Add(importReceipt);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index");
        }

        //Action vào view chi tiết phiếu nhập hàng
        public IActionResult CreateReceiptDetail(int id)
        {
            receiptDetailVM.Receipt_Details = (from b in _db.Receipt_Details
                                               where b.ImportReceiptID == id
                                               select b);
            foreach (var item in receiptDetailVM.Receipt_Details)
            {
                item.Product = _db.Products.Where(s => s.ProductID == item.ProductID).Select(s => s).FirstOrDefault();
            }
            return View(receiptDetailVM);
        }

        //Action tạo mới một chi tiết phiếu nhập hàng
        [HttpPost, ActionName("CreateReceiptDetail")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReceiptDetailPost(int id)
        {
            if (ModelState.IsValid)
            {

                receiptDetailVM.Receipt_Detail.ImportReceiptID = id;
                _db.Add(receiptDetailVM.Receipt_Detail);
                await _db.SaveChangesAsync();

                var receiptFromDb = _db.ImportReceipts.Where(m => m.ImportReceiptID == id).FirstOrDefault();
                receiptFromDb.Total = (from a in _db.Receipt_Details
                                       where a.ImportReceiptID == id
                                       select a.Amount).Sum();

                await _db.SaveChangesAsync();

                int amount = (from s in _db.Receipt_Details
                              where s.ProductID == receiptDetailVM.Receipt_Detail.ProductID
                              select s.Amount).LastOrDefault();

                var productFromDb = _db.Products.Where(s => s.ProductID == receiptDetailVM.Receipt_Detail.ProductID).Select(s => s).FirstOrDefault();
                productFromDb.Amount = productFromDb.Amount + amount;

                await _db.SaveChangesAsync();

            }
            return RedirectToAction("CreateReceiptDetail");
        }


        //Action hiển thị thông tin chi tiết của một phiếu nhập hàng
        public async Task<IActionResult> IndexReceiptDetail(int id)
        {

            var receipts = from b in _db.Receipt_Details
                           where b.ImportReceiptID == id
                           group b by b.ProductID into g
                           select new Receipt_Detail
                           {
                               ProductID = g.First().ProductID,
                               Amount = g.Sum(pc => pc.Amount),
                               Product = g.First().Product,
                               ManufactureDate =g.First().ManufactureDate,
                               ExpiryDate =g.First().ExpiryDate
                           };
            return View(await receipts.ToListAsync());
        }


        //Action vào view chỉnh sửa một phiếu nhập hàng
        public async Task<IActionResult> EditReceiptDetail(int id)
        {
            Receipt_Detail receipt_Detail = new Receipt_Detail();
            receipt_Detail = await _db.Receipt_Details.FindAsync(id);
            if (receipt_Detail == null)
            {
                return NotFound();
            }
            return View(receipt_Detail);
        }


        //Action chỉnh sửa một phiếu nhập hàng
        [HttpPost, ActionName("EditReceiptDetail")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditReceiptDetailPost(int id, Receipt_Detail receipt_Detail)
        {
            if (id != receipt_Detail.Receipt_DetailID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {

                var receiptDetailFromDb = _db.Receipt_Details.Where(m => m.Receipt_DetailID == receipt_Detail.Receipt_DetailID).FirstOrDefault();
                int amount = receipt_Detail.Amount - receipt_Detail.Amount;

                receiptDetailFromDb.Amount = receipt_Detail.Amount;
                await _db.SaveChangesAsync();


                var productFromDb = _db.Products.Where(s => s.ProductID == receipt_Detail.ProductID).Select(s => s).FirstOrDefault();
                productFromDb.Amount = productFromDb.Amount + amount;
                await _db.SaveChangesAsync();

                int sum = (from s in _db.Receipt_Details
                           where s.ImportReceiptID == receipt_Detail.ImportReceiptID
                           select s.Amount).Sum();

                var receiptFromDb = _db.ImportReceipts.Where(m => m.ImportReceiptID == receiptDetailFromDb.ImportReceiptID).FirstOrDefault();
                receiptFromDb.Total = sum;
                await _db.SaveChangesAsync();


                return View(receipt_Detail);
            }
            else
            {
                return View(receipt_Detail);
            }

        }

    }
}