using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Filter;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Data;
using Ecommerce.Services;

namespace Ecommerce.Controllers
{
    [OrderAuthorizationFilter]
    public class OrdersController : Controller
    {
        private readonly OrderService _service;

        public OrdersController(OrderService service)
        {
            _service = service;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var order = await _service.GetAllAsync();
            return PartialView("_OrderList", order);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = await _service.GetOneAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        [HttpGet]

        public async Task<IActionResult> Create()
        {
            await PopulateUserAndVoucherAsync();
            return PartialView("_OrderFormCreate");
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_order,name,order_time,description,price,status,id_staff,id_voucher,id_user")] Order order)
        {
            await _service.InsertAsync(order);
            return RedirectToAction(nameof(Index));
        }
        private async Task PopulateUserAndVoucherAsync(int? selectedVoucherId = null, int? selectedUserId = null)
        {
            var vouchers = await _service.GetVoucherWithoutOrdersAsync();
            var users = await _service.GetUsersAsync(); 

            ViewBag.Vouchers = new SelectList(vouchers, "id_voucher", "id_voucher", selectedVoucherId);
            ViewBag.Users = new SelectList(users, "id_user", "fullname", selectedUserId);
        }
      
        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order= await _service.GetOneAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            await PopulateUserAndVoucherAsync();
            return PartialView("_OrderFormEdit", order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_order,name,order_time,description,price,status,id_staff,id_voucher,id_user")] Order order)
        {
            if (id != order.id_order)
            {
                return NotFound();
            }

            await _service.UpdateAsync(order);
            return Redirect("/Admin");
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _service.GetOneAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return PartialView("_OrderDelete", order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Redirect("/Admin");
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetUsersByVoucher(int id_voucher)
        {
            var users = await _service.GetUserAsync2(id_voucher);
            return Json(users.Select(u => new { id = u.id_user, name = u.fullname }));
        }
        private bool OrderExists(int id)
        {
            return _service.IsExists(id);
        }
    }
}
