using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Data;
using Ecommerce.Services;
using Ecommerce.Models;

namespace Ecommerce.Controllers
{
    public class VouchersController : Controller
    {
        private readonly VoucherService _service;


        public VouchersController(VoucherService service)
        {
            _service = service;
        }

        // GET: Vouchers
        public async Task<IActionResult> Index()
        {
            var voucher = await _service.GetAllAsync();
            return PartialView("_VoucherList", voucher);
        }

        // GET: Vouchers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var voucher = await _service.GetOneAsync(id);
            if (voucher == null)
            {
                return NotFound();
            }

            return View(voucher);
        }

        // GET: Vouchers/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return  PartialView("_VoucherFormCreate");
        }

        // POST: Vouchers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("percent_discount,start_date,finish_date")] Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                await _service.InsertAsync(voucher);
                return Json(new { success = true });
            }
            return PartialView("_VoucherFormCreate");
        }

        // GET: Vouchers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voucher = await _service.GetOneAsync(id);
            if (voucher == null)
            {
                return NotFound();
            }

            return PartialView("_VoucherFormEdit", voucher);
        }

        // POST: Vouchers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_voucher,percent_discount,start_date,finish_date")] Voucher voucher)
        {
            if (id != voucher.id_voucher)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(voucher);
                  return  Redirect("/admin.html");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoucherExists(voucher.id_voucher))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(voucher);
        }

        // GET: Vouchers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voucher = await _service.GetOneAsync(id);
            if (voucher == null)
            {
                return NotFound();
            }

            return PartialView("_VoucherDeleteInfor", voucher);
        }

        // POST: Vouchers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Redirect("/admin.html");
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, e.Message);
            }
        }

        private bool VoucherExists(int id)
        {
            return _service.IsExists(id);
        }
        public IActionResult PartialVoucherForm()
        {
            return PartialView("VoucherForm");
        }
    }
}
