using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffRoleController : ControllerBase
    {
        private readonly StaffRoleService _staffRoleService;

        // Constructor khởi tạo với StaffRoleService
        public StaffRoleController(StaffRoleService staffRoleService)
        {
            _staffRoleService = staffRoleService;
        }

        // Phương thức gán vai trò cho nhân viên
        [HttpPost("assign")]
        public async Task<IActionResult> AssignRoleToStaff(StaffRoleVM staffRole)
        {
            try
            {
                await _staffRoleService.AssignRoleToStaffAsync(staffRole.IdStaff, staffRole.IdRole);
                return Ok($"Đã gán vai trò {staffRole.IdRole} cho nhân viên {staffRole.IdStaff}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server nội bộ: {ex.Message}");
            }
        }

        // Phương thức xóa vai trò khỏi nhân viên
        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveRoleFromStaff(StaffRoleVM staffRole)
        {
            try
            {
                await _staffRoleService.RemoveRoleFromStaffAsync(staffRole.IdStaff, staffRole.IdRole);
                return Ok($"Đã xóa vai trò {staffRole.IdRole} khỏi nhân viên {staffRole.IdStaff}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server nội bộ: {ex.Message}");
            }
        }
    }
}
