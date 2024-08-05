using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    public class RoleService : IService<Role, RoleVM>
    {
        private readonly MyDBContext _dbContext;

        // Constructor khởi tạo với MyDBContext
        public RoleService(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Phương thức lấy tất cả các Role
        public async Task<List<RoleVM>> GetAllAsync()
        {
            return await _dbContext.Roles
                .Select(r => new RoleVM
                {
                    IdRole = r.id_role,
                    RoleName = r.role_name
                })
                .ToListAsync();
        }

        // Phương thức lấy một Role theo ID
        public async Task<RoleVM> GetOneAsync(int id)
        {
            return await _dbContext.Roles
                .Where(r => r.id_role == id)
                .Select(r => new RoleVM
                {
                    IdRole = r.id_role,
                    RoleName = r.role_name
                })
                .FirstOrDefaultAsync();
        }

        // Phương thức thêm một Role mới
        public async Task<RoleVM> InsertAsync(Role role)
        {
            _dbContext.Roles.Add(role);
            await _dbContext.SaveChangesAsync();
            return await GetOneAsync(role.id_role);
        }

        // Phương thức cập nhật một Role
        public async Task UpdateAsync(Role role)
        {
            _dbContext.Entry(role).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        // Phương thức xóa một Role
        public async Task DeleteAsync(int id)
        {
            var role = await _dbContext.Roles.FindAsync(id);
            if (role != null)
            {
                _dbContext.Roles.Remove(role);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
