using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    public class StaffService : IService<Staff, StaffVM>
    {
        private readonly MyDBContext _dbContext;

        // Constructor khởi tạo với MyDBContext
        public StaffService(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Phương thức lấy tất cả nhân viên
        public async Task<List<StaffVM>> GetAllAsync()
        {
            return await _dbContext.Staffs
                .Select(s => new StaffVM
                {
                    IdStaff = s.id_staff,
                    NameStaff = s.name_staff,
                    TypeStaff = s.type_staff,
                    Position = s.position,
                    Sex = s.sex,
                    Email = s.email,
                    Username = s.username,
                    Roles = s.StaffRoles.Select(sr => sr.Role.role_name).ToList()
                })
                .ToListAsync();
        }

        // Phương thức lấy một nhân viên theo ID
        public async Task<StaffVM> GetOneAsync(int id)
        {
            return await _dbContext.Staffs
                .Where(s => s.id_staff == id)
                .Select(s => new StaffVM
                {
                    IdStaff = s.id_staff,
                    NameStaff = s.name_staff,
                    TypeStaff = s.type_staff,
                    Position = s.position,
                    Sex = s.sex,
                    Email = s.email,
                    Username = s.username,
                    Roles = s.StaffRoles.Select(sr => sr.Role.role_name).ToList()
                })
                .FirstOrDefaultAsync();
        }

        // Phương thức thêm một nhân viên mới
        public async Task<StaffVM> InsertAsync(Staff staff)
        {
            _dbContext.Staffs.Add(staff);
            await _dbContext.SaveChangesAsync();
            return await GetOneAsync(staff.id_staff);
        }

        // Phương thức cập nhật một nhân viên
        public async Task UpdateAsync(Staff staff)
        {
            _dbContext.Entry(staff).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        // Phương thức xóa một nhân viên
        public async Task DeleteAsync(int id)
        {
            var staff = await _dbContext.Staffs.FindAsync(id);
            if (staff != null)
            {
                _dbContext.Staffs.Remove(staff);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
