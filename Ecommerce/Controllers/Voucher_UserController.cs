using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Data;
using Ecommerce.Services;

namespace Ecommerce.Controllers
{
    public class Voucher_UserController : Controller
    {
        private readonly Voucher_UserService _service;

        public Voucher_UserController(Voucher_UserService service)
        {
            _service = service;
        }

        // GET: Voucher_User
        public async Task<IActionResult> Index()
        {
            var voucher = await _service.GetAllAsync();
            return View(voucher);
        }

        // GET: Voucher_User/Details/5
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

        // GET: Voucher_User/Create
        public async Task<IActionResult> Create()
        {
            await PopulateVochersAndUsersAsync();
            return View(new Voucher_User());
        }
     
        
        // POST: Voucher_User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_voucher,id_user,state,id_order")] Voucher_User voucher_User)
        {
            await _service.InsertAsync(voucher_User);
            return RedirectToAction(nameof(Index));
        }

        // GET: Voucher_User/Edit/5
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

            await PopulateVochersAndUsersAsync(voucher.id_voucher, voucher.id_user);
            return View(voucher);
        }

        // POST: Voucher_User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id_voucher,int id_user, [Bind("id_voucher,id_user,state,id_order")] Voucher_User voucher_User)
        {
            if (id_voucher != voucher_User.id_voucher && id_user != voucher_User.id_user)
            {
                return NotFound();
            }


            await _service.UpdateAsync(voucher_User);
            return RedirectToAction(nameof(Index));
        }

        // GET: Voucher_User/Delete/5
        public async Task<IActionResult> Delete(int id_voucher, int id_user)
        {
            if (id_voucher == null && id_user == null)
            {
                return NotFound();
            }

            var voucher = await _service.GetOneAsync2(id_voucher,id_user);
            if (voucher == null)
            {
                return NotFound();
            }

            return View(voucher);
        }

        // POST: Voucher_User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id_voucher, int id_user)
        {
            try
            {
                await _service.DeleteAsyn2(id_voucher,id_user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        private bool Voucher_UserExists(int id)
        {
            return _service.IsExists(id);
        }
        private async Task PopulateVochersAndUsersAsync(int? selectedVoucherId = null, int? selectedUserId = null)
        {
            var vouchers = await _service.GetVoucherAsync();
            var users = await _service.GetUserAsync();

            ViewBag.Voucher = new SelectList(vouchers, "id_voucher", "id_voucher", selectedVoucherId);
            ViewBag.User = new SelectList(users, "id_user", "username", selectedUserId);
        }
    }
}
