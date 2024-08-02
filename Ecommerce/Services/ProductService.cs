using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class ProductService : Iservice<Product>
    {
        private readonly MyDBContext _dbContext;

        public ProductService(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Xóa sản phẩm dựa trên id
        public async Task DeleteAsync(int? id)
        {
            var product = await _dbContext.Products
                .FirstOrDefaultAsync(p => p.id_product == id);

            if (product != null)
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
            }
        }

        // Lấy tất cả sản phẩm
        public async Task<List<Product>> GetAllAsync()
        {
            return await _dbContext.Products
                .Include(p => p.Category)  // Tải thực thể Category
                .Include(p => p.Supplier)  // Tải thực thể Supplier
                .ToListAsync();
        }

        // Lấy một sản phẩm cụ thể theo id
        public async Task<Product> GetOneAsync(int? id)
        {
            return await _dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Where(p => p.id_product == id)
                .SingleOrDefaultAsync();
        }

        // Thêm sản phẩm mới
        public async Task<Product> InsertAsync(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            return product;
        }

        // Cập nhật thông tin sản phẩm
        public async Task UpdateAsync(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        // Kiểm tra sự tồn tại của sản phẩm
        public bool IsExists(int id)
        {
            return _dbContext.Products.Any(e => e.id_product == id);
        }

        // Lấy danh sách tất cả các danh mục
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _dbContext.Categories
                .ToListAsync();
        }

        // Lấy danh sách tất cả các nhà cung cấp
        public async Task<List<Supplier>> GetSuppliersAsync()
        {
            return await _dbContext.Suppliers
                .ToListAsync();
        }
    }
}
