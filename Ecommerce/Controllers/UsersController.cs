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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ecommerce.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService _service;

        public UsersController(UserService service)
        {
            _service = service;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var user = await _service.GetAllAsync();
            return PartialView("_UserList", user);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _service.GetOneAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
           await PopulateAccountsAsync();
            return PartialView("_UserFormCreate");
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_user,fullname,email, address,phone,id_account")] User user,string username, string password)
        {
            Account acc = new Account
            {
                username = username,
                password = password,
                type = 0
            };

            Account responseAcc = await _service.InsertAccount(acc);
            user.id_account = responseAcc.id_account;
            await _service.InsertAsync(user);
            return Redirect("/admin.html");
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _service.GetOneAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            await PopulateAccountsAsync(user.id_account);

            return PartialView("_UserFormEdit", user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_user,fullname,email,address,phone,id_account")] User user)
        {
            if (id != user.id_user)
            {
                return NotFound();
            }

            await _service.UpdateAsync(user);
            return Redirect("/admin.html");
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _service.GetOneAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return PartialView("_UserDelete", user);
        }

        // POST: Users/Delete/5
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

        private bool UserExists(int id)
        {
            return _service.IsExists(id);
        }


        private async Task PopulateAccountsAsync(int? selectedAccountId = null)
        {
            var accounts = await _service.GetAccountsWithoutUsersAsync();

            ViewBag.Accounts = new SelectList(accounts, "id_account", "username", selectedAccountId);
            
        }

    }
}
