using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    public class SupplierService : Iservice<Supplier, SupplierVM>
    {
        private readonly MyDBContext _dbContext;

        public SupplierService(MyDBContext context)
        {
            _dbContext = context;
        }
        public async Task DeleteAsync(int id)
        {
            var supplier = _dbContext.Suppliers.SingleOrDefault(sup => sup.id_supplier == id);
            if (supplier != null)
            {
                _dbContext.Suppliers.Remove(supplier);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<SupplierVM>> GetAllAsync()
        {
            var list = await _dbContext.Suppliers.Select(
               c => new SupplierVM
               {
                   IdSupplier = c.id_supplier,
                   NameCompany = c.name_company,
                   Address = c.address
               }).ToListAsync();
            return list;
        }

        public async Task<SupplierVM> GetOneAsync(int id)
        {
           var  supplier = await _dbContext.Suppliers
                .Where( sup => sup.id_supplier == id)
                .Select( sup => new SupplierVM
                {
                    IdSupplier = sup.id_supplier,
                    NameCompany = sup.name_company,
                    Address = sup.address
                })
                .SingleOrDefaultAsync();
            return supplier;
        }

        public async Task<SupplierVM> InsertAsync(Supplier entity)
        {
            _dbContext.Suppliers.Add(entity);
            await _dbContext.SaveChangesAsync();
            return new SupplierVM {
                IdSupplier = entity.id_supplier,
                NameCompany = entity.name_company,
                Address = entity.address
            };
        }

        public async Task UpdateAsync(Supplier entity)
        {
            _dbContext.Suppliers.Update(entity);
           await _dbContext.SaveChangesAsync();

        }
    }

}
