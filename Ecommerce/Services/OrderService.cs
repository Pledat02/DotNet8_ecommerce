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
                .Include(o => o.Staff)
                .Include(o => o.Voucher_User)
                .ToListAsync();
        }

        public async Task<Order> GetOneAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Order ID cannot be null.");
            }

            return await _dbContext.Orders
                .Include(o => o.Staff)
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
    }
}
