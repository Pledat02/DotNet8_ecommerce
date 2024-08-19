using Ecommerce.Services;
using Ecommerce.Data;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Filter;
using System.Text;
using System.Security.Cryptography;

namespace Ecommerce.Controllers
{
    [StaffAuthorizationFilter]
    public class StaffsController : Controller
    {
        private readonly StaffService _service;
        private readonly RoleService _roleService;

        public StaffsController(StaffService service , RoleService roleService)
        {
            _service = service;
            _roleService = roleService;
        }
        public async Task<IActionResult> Index()
        {   
           
            return View(await _service.GetAllAsync());
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = await _roleService.GetAllAsync();
            return PartialView("Create", new Staff());
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _service.GetOneAsync(id);

            if (staff == null)
            {
                return NotFound();
            }
            ViewBag.Roles = await _roleService.GetAllAsync();
            return PartialView("Edit",staff);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_staff, name_staff, sex, email")] Staff staff)
        {
            if (id != staff.id_staff)
            {
                return NotFound();
            }

            // Lấy giá trị của username và password từ Request.Form
            var username = Request.Form["username"];
            var password = Request.Form["password"];
            var selectedRoles = Request.Form["roles"]; // Lấy danh sách các role đã chọn

            // Tìm đối tượng staff trong database
            var existingStaff = await _service.GetOneAsync(id);

            if (existingStaff == null)
            {
                return NotFound();
            }

            // Cập nhật các thuộc tính Staff
            existingStaff.name_staff = staff.name_staff;
            existingStaff.sex = staff.sex;
            existingStaff.email = staff.email;

            // Cập nhật username và password nếu có thay đổi
            if (!string.IsNullOrEmpty(username) && existingStaff.Account.username != username)
            {
                existingStaff.Account.username = username;
            }

            if (!string.IsNullOrEmpty(password) && existingStaff.Account.password != password)
            {
                existingStaff.Account.password = password;
            }

            // Xóa các quyền hiện có và thêm lại các quyền mới
            existingStaff.StaffRoles.Clear();
            if (selectedRoles.Count > 0)
            {
                foreach (var roleId in selectedRoles)
                {
                    existingStaff.StaffRoles.Add(new StaffRole
                    {
                        id_role = int.Parse(roleId),
                        Staff = existingStaff
                    });
                }
            }

            // Cập nhật vào database
            await _service.UpdateAsync(existingStaff);

            return Redirect("/admin");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("name_staff, sex, email, Account.username, Account.password")] Staff staff)
        {
            var username = Request.Form["username"];
            var password = Request.Form["password"];

            // Tạo đối tượng Account và gán giá trị
            staff.Account = new Account
            {
                username = username,
                password = HashPassword(password),
                type = 1
            };
            // Thêm các StaffRole dựa trên các role được chọn
            var selectedRoles = Request.Form["roles"];
            if (selectedRoles.Count > 0)
            {
                staff.StaffRoles = new List<StaffRole>();
                foreach (var roleId in selectedRoles)
                {
                    staff.StaffRoles.Add(new StaffRole
                    {
                        id_role = int.Parse(roleId),
                        Staff = staff
                    });
                }
            }
      
            staff.position = "Nhân Viên bán hàng";
            staff.type_staff = "Nhân Viên bán hàng";

            await _service.InsertAsync(staff);

            return Redirect("/Admin");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var staff = await _service.GetOneAsync(id);

            if (staff == null)
            {
                return NotFound();
            }

            // Return a partial view containing the delete confirmation form
            return PartialView("Delete", staff);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staff = await _service.GetOneAsync(id);

            if (staff == null)
            {
                return NotFound();
            }

            await _service.DeleteAsync(id);

            return Redirect("/Admin");
        }
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}
