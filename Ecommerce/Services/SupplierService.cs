using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class SupplierService
    {
        private readonly MyDBContext _dbContext;

        public SupplierService(MyDBContext context)
        {
            _dbContext = context;
        }

        public async Task DeleteAsync(int? id)
        {
            var supplier = await _dbContext.Suppliers
                .SingleOrDefaultAsync(s => s.id_supplier == id);
            if (supplier != null)
            {
                _dbContext.Suppliers.Remove(supplier);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Supplier>> GetAllAsync()
        {
            return await _dbContext.Suppliers.ToListAsync();
        }

        public async Task<Supplier> GetOneAsync(int? id)
        {
            return await _dbContext.Suppliers
                .SingleOrDefaultAsync(s => s.id_supplier == id);
        }

        public async Task<Supplier> InsertAsync(Supplier supplier)
        {
            _dbContext.Suppliers.Add(supplier);
            await _dbContext.SaveChangesAsync();
            return supplier;
        }

        public async Task UpdateAsync(Supplier supplier)
        {
            _dbContext.Suppliers.Update(supplier);
            await _dbContext.SaveChangesAsync();
        }

        public bool IsExists(int id)
        {
            return _dbContext.Suppliers.Any(e => e.id_supplier == id);
        }
    }
}
