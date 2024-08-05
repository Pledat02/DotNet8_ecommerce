using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    public class StaffRoleService
    {
        private readonly MyDBContext _dbContext;

        // Constructor khởi tạo với MyDBContext
        public StaffRoleService(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Phương thức gán role cho nhân viên
        public async Task AssignRoleToStaffAsync(int staffId, int roleId)
        {
            var staffRole = new StaffRole
            {
                id_staff = staffId,
                id_role = roleId
            };
            _dbContext.StaffRoles.Add(staffRole);
            await _dbContext.SaveChangesAsync();
        }

        // Phương thức xóa role khỏi nhân viên
        public async Task RemoveRoleFromStaffAsync(int staffId, int roleId)
        {
            var staffRole = await _dbContext.StaffRoles
                .FirstOrDefaultAsync(sr => sr.id_staff == staffId && sr.id_role == roleId);
            if (staffRole != null)
            {
                _dbContext.StaffRoles.Remove(staffRole);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
