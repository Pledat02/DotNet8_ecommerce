using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly StaffService _staffService;

        // Constructor khởi tạo với StaffService
        public StaffController(StaffService staffService)
        {
            _staffService = staffService;
        }

        // Phương thức lấy tất cả nhân viên
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffVM>>> GetAllStaff()
        {
            try
            {
                var staffList = await _staffService.GetAllAsync();
                return Ok(staffList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server nội bộ: {ex.Message}");
            }
        }

        // Phương thức lấy một nhân viên theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<StaffVM>> GetStaff(int id)
        {
            try
            {
                var staff = await _staffService.GetOneAsync(id);
                if (staff == null)
                {
                    return NotFound($"Không tìm thấy nhân viên với ID {id}");
                }
                return Ok(staff);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server nội bộ: {ex.Message}");
            }
        }

        // Phương thức tạo một nhân viên mới
        [HttpPost]
        public async Task<ActionResult<StaffVM>> CreateStaff(Staff staff)
        {
            try
            {
                var createdStaff = await _staffService.InsertAsync(staff);
                return CreatedAtAction(nameof(GetStaff), new { id = createdStaff.IdStaff }, createdStaff);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server nội bộ: {ex.Message}");
            }
        }

        // Phương thức cập nhật một nhân viên
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(int id, Staff staff)
        {
            if (id != staff.id_staff)
            {
                return BadRequest("ID nhân viên không khớp");
            }

            try
            {
                await _staffService.UpdateAsync(staff);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server nội bộ: {ex.Message}");
            }
        }

        // Phương thức xóa một nhân viên
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            try
            {
                await _staffService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server nội bộ: {ex.Message}");
            }
        }
    }
}
