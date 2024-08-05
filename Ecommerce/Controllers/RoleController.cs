using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;

        // Constructor khởi tạo với RoleService
        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        // Phương thức lấy tất cả các vai trò
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleVM>>> GetAllRoles()
        {
            try
            {
                var roles = await _roleService.GetAllAsync();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server nội bộ: {ex.Message}");
            }
        }

        // Phương thức lấy một vai trò theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleVM>> GetRole(int id)
        {
            try
            {
                var role = await _roleService.GetOneAsync(id);
                if (role == null)
                {
                    return NotFound($"Không tìm thấy vai trò với ID {id}");
                }
                return Ok(role);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server nội bộ: {ex.Message}");
            }
        }

        // Phương thức tạo một vai trò mới
        [HttpPost]
        public async Task<ActionResult<RoleVM>> CreateRole(Role role)
        {
            try
            {
                var createdRole = await _roleService.InsertAsync(role);
                return CreatedAtAction(nameof(GetRole), new { id = createdRole.IdRole }, createdRole);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server nội bộ: {ex.Message}");
            }
        }

        // Phương thức cập nhật một vai trò
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, Role role)
        {
            if (id != role.id_role)
            {
                return BadRequest("ID vai trò không khớp");
            }

            try
            {
                await _roleService.UpdateAsync(role);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server nội bộ: {ex.Message}");
            }
        }

        // Phương thức xóa một vai trò
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                await _roleService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server nội bộ: {ex.Message}");
            }
        }
    }
}
