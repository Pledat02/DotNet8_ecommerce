using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    public class StaffService :Iservice<Staff>
    {
        private readonly MyDBContext _dbContext;

        public StaffService (MyDBContext dbContext)
        {

        _dbContext = dbContext; 
        }
        public async Task DeleteAsync(int? id)
        {
            var Staff = await _dbContext.Staffs.FirstOrDefaultAsync(s => s.id_staff == id);
            if (Staff != null)
            {
                _dbContext.Staffs.Remove(Staff);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Staff>> GetAllAsync()
        {
           return await _dbContext.Staffs.ToListAsync();
        }

        public async Task<Staff> GetOneAsync(int? id)
        {
            var Staff = await _dbContext.Staffs.FirstOrDefaultAsync(s => s.id_staff == id);
            return Staff;
        }
        public Staff GetOneByIdAccount(int? id)
        {
            var Staff =  _dbContext.Staffs.FirstOrDefault(s => s.id_account == id);
            return Staff;
        }
        public async Task<Staff> InsertAsync(Staff entity)
        {
            await _dbContext.Staffs.AddAsync(entity);
            return entity;
        }

        public bool IsExists(int id)
        {
            return _dbContext.Staffs.Any(s => s.id_staff == id);
        }

        public async Task UpdateAsync(Staff entity)
        {
             _dbContext.Staffs.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
