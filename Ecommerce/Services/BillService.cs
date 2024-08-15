using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    public class BillService : Iservice<Bill>
    {
        private readonly MyDBContext _dbContext;

        public BillService(MyDBContext context)
        {
            _dbContext = context;
        }

        public async Task<List<Bill>> GetAllAsync()
        {
            return await _dbContext.Bills
                .Include(b => b.Order) // Include related Order data
                .ToListAsync();
        }

        public async Task<Bill> GetOneAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Bill ID cannot be null.");
            }

            return await _dbContext.Bills
                .Include(b => b.Order)
                .FirstOrDefaultAsync(b => b.id_order == id)
                ?? throw new KeyNotFoundException("Bill not found.");
        }

        public async Task<Bill> InsertAsync(Bill bill)
        {
            if (bill == null)
            {
                throw new ArgumentNullException(nameof(bill), "Bill cannot be null.");
            }

            try
            {
                _dbContext.Bills.Add(bill);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                // Optionally rethrow or handle the exception
                throw;
            }

            return bill;
        }


        public async Task UpdateAsync(Bill bill)
        {
            if (bill == null)
            {
                throw new ArgumentNullException(nameof(bill), "Bill cannot be null.");
            }

            _dbContext.Bills.Update(bill);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            var bill = await _dbContext.Bills
                .FirstOrDefaultAsync(b => b.id_order == id);

            if (bill != null)
            {
                _dbContext.Bills.Remove(bill);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Bill not found.");
            }
        }

        public bool IsExists(int id)
        {
            return _dbContext.Bills.Any(b => b.id_order == id);
        }
    }
}
