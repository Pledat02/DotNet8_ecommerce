using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    public class VoucherService : Iservice<Voucher>
    {
        private readonly MyDBContext _dbContext;

        public VoucherService(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task DeleteAsync(int? id)
        {
            var voucher = await _dbContext.Vouchers.FirstOrDefaultAsync(v => v.id_voucher == id);

            if (voucher != null)
            {
                _dbContext.Vouchers.Remove(voucher);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Voucher>> GetAllAsync()
        {
            return await _dbContext.Vouchers.ToListAsync();
        }

        public async Task<Voucher> GetOneAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Voucher ID cannot be null.");
            }

            return await _dbContext.Vouchers
                .FirstOrDefaultAsync(c => c.id_voucher == id)
                ?? throw new KeyNotFoundException("Voucher id not found.");
        }

        public async Task<Voucher> InsertAsync(Voucher voucher)
        {
            _dbContext.Vouchers.Add(voucher);
            await _dbContext.SaveChangesAsync();

            return voucher;
        }

        // Cập nhật thông tin sản phẩm
        public async Task UpdateAsync(Voucher voucher)
        {
            _dbContext.Vouchers.Update(voucher);
            await _dbContext.SaveChangesAsync();
        }

        // Kiểm tra sự tồn tại của sản phẩm
        public bool IsExists(int id)
        {
            return _dbContext.Vouchers.Any(e => e.id_voucher == id);
        }



    }
}
