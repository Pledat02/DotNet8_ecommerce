using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    public class Voucher_UserService : Iservice<Voucher_User>
    {
        private readonly MyDBContext _dbContext;

        public Voucher_UserService(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task DeleteAsync(int? id)
        {
            var voucher = await _dbContext.Voucher_Users
                .FirstOrDefaultAsync(v => v.id_voucher == id );

            if (voucher != null)
            {
                _dbContext.Voucher_Users.Remove(voucher);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteAsyn2(int id_voucher, int id_user)
        {
            var voucher = await _dbContext.Voucher_Users
                .FirstOrDefaultAsync(v => v.id_voucher == id_voucher && v.id_user == id_user);

            if (voucher != null)
            {
                _dbContext.Voucher_Users.Remove(voucher);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task<List<Voucher_User>> GetAllAsync()
        {
            return await _dbContext.Voucher_Users
                .Include(v => v.Voucher) 
                .Include(v => v.User)  
                .ToListAsync();
        }

        public async Task<Voucher_User> GetOneAsync(int? id)
        {
            return await _dbContext.Voucher_Users
                .Include(v => v.Voucher)
                .Include(v => v.User)
                .Where(v => v.id_voucher == id)
                .SingleOrDefaultAsync();
        }
        public async Task<Voucher_User> GetOneAsync2(int id_voucher, int id_user)
        {
            return await _dbContext.Voucher_Users
                .Include(v => v.Voucher)
                .Include(v => v.User)
                .Where(v => v.id_voucher == id_voucher && v.id_user == id_user)
                .SingleOrDefaultAsync();
        }
        public async Task<Voucher_User> InsertAsync(Voucher_User voucher_User)
        {
            _dbContext.Voucher_Users.Add(voucher_User);
            await _dbContext.SaveChangesAsync();

            return voucher_User;
        }

        // Cập nhật thông tin sản phẩm
        public async Task UpdateAsync(Voucher_User voucher_User)
        {
            _dbContext.Voucher_Users.Update(voucher_User);
            await _dbContext.SaveChangesAsync();
        }

        // Kiểm tra sự tồn tại của sản phẩm
        public bool IsExists(int id)
        {
            return _dbContext.Voucher_Users.Any(e => e.id_voucher == id);
        }

        public async Task<List<Voucher>> GetVoucherAsync()
        {
            return await _dbContext.Vouchers
                .ToListAsync();
        }

        public async Task<List<User>> GetUserAsync()
        {
            return await _dbContext.Users
                .ToListAsync();
        }

      
    }
}
