using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    /*
    public class OrderDetailsService : Iservice<OrderDetail, OrderDetailsVM>
    {
        private readonly MyDBContext _dbContext;

        public OrderDetailsService(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task DeleteAsync(int id)
        {
            var orderDetail = await _dbContext.OrderDetails
                .FirstOrDefaultAsync(od => od.id_order == id);
            if (orderDetail != null)
            {
                _dbContext.OrderDetails.Remove(orderDetail);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<OrderDetailsVM>> GetAllAsync()
        {
            var list = await _dbContext.OrderDetails.Select(od =>
                new OrderDetailsVM
                {
                    IdOrder = od.id_order,
                    IdProduct = od.id_product,
                    TotalPrice = od.total_price,
                    Quantity = od.quantity,
                    Price = od.price
                }).ToListAsync();
            return list;
        }

        public async Task<OrderDetailsVM> GetOneAsync(int id)
        {
            var orderDetail = await _dbContext.OrderDetails
                .Where(od => od.id_order == id) // Adjust according to your composite key structure
                .Select(od => new OrderDetailsVM
                {
                       IdOrder = od.id_order,
                    IdProduct = od.id_product,
                    TotalPrice = od.total_price,
                    Quantity = od.quantity,
                    Price = od.price
                }).SingleOrDefaultAsync();
            return orderDetail;
        }

        public async Task<OrderDetailsVM> InsertAsync(OrderDetail entity)
        {
            _dbContext.OrderDetails.Add(entity);
            await _dbContext.SaveChangesAsync();

            return new OrderDetailsVM
            {
                IdOrder = entity.id_order,
                IdProduct = entity.id_product,
                TotalPrice = entity.total_price,
                Quantity = entity.quantity,
                Price = entity.price
            };
        }

        public async Task UpdateAsync(OrderDetail entity)
        {
            _dbContext.OrderDetails.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
    */
}
