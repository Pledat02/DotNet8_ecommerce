using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    public class OrderService : Iservice<Order>
    {
        private readonly MyDBContext _dbContext;

        public OrderService(MyDBContext context)
        {
            _dbContext = context;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _dbContext.Orders
                 .Include(o => o.Voucher_User)
                .Include(od => od.OrderDetails)
                .ThenInclude(od => od.Product)
                .ToListAsync();
        }

        public async Task<Order> GetOneAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Order ID cannot be null.");
            }

            return await _dbContext.Orders
                .Include(o => o.Voucher_User)
                .FirstOrDefaultAsync(o => o.id_order == id)
                ?? throw new KeyNotFoundException("Order not found.");
        }

        public async Task<Order> InsertAsync(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "Order cannot be null.");
            }

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            return order;
        }

        public async Task UpdateAsync(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "Order cannot be null.");
            }

            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            var order = await _dbContext.Orders
                .FirstOrDefaultAsync(o => o.id_order == id);

            if (order != null)
            {
                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Order not found.");
            }
        }

        public bool IsExists(int id)
        {
            return _dbContext.Orders.Any(o => o.id_order == id);
        }
        public async Task<List<Voucher>> GetVouchersAsync()
        {
            return await _dbContext.Vouchers.ToListAsync();

        }

        public async Task<List<Voucher_User>> GetVoucher_UserAsync()
        {
            return await _dbContext.Voucher_Users.ToListAsync();

        }
        public async Task<List<Voucher_User>> GetVoucher_UserAsync(int id)
        {
            return await _dbContext.Voucher_Users
         .Where(v => v.id_user == id)
         .ToListAsync();
            ;
        }

        public async Task<List<User>> GetUserAsync2(int id)
        {
            return await _dbContext.Users
         .Where(u => u.Voucher_Users.Any(v => v.id_voucher == id))
        .ToListAsync();

        }
        public async Task<List<User>> GetUsersAsync()
        {
            return await _dbContext.Users
                .ToListAsync();
        }
        public async Task<List<Voucher_User>> GetVoucherWithoutOrdersAsync()
        {
            return await _dbContext.Voucher_Users
                .Where(a => a.Order == null)
                .ToListAsync();
        }
    }
}
