using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    public class ProductService : Iservice<Product, ProductVM>
    {
        private readonly MyDBContext _dbContext;
        public ProductService (MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _dbContext.Products
                 .FirstOrDefaultAsync(p => p.id_product == id);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<ProductVM>> GetAllAsync()
        {
            var list = await _dbContext.Products.Select(p =>
            new ProductVM {
                    IdProduct = p.id_product,
                    Name = p.name,
                    Description = p.description,
                    QuantityInStock = p.quantity_in_stock,
                    Price = p.price,
                    UrlImage = p.url_image,
                    Category = new CategoryVM
                    {
                        IdCategory = p.Category.id_category,
                        Name = p.Category.name,
                        Description = p.Category.description
                    },
                    Supplier = new SupplierVM
                    {
                        IdSupplier = p.Supplier.id_supplier,
                        NameCompany = p.Supplier.name_company,
                        Address = p.Supplier.address
                    }
            }).ToListAsync();
            return list;
        }

        public async Task<ProductVM> GetOneAsync(int id)
        {
            var product = await _dbContext.Products
                .Where(p => p.id_product == id)
                .Select(p => new ProductVM {
                    IdProduct = p.id_product,
                    Name = p.name,
                    Description = p.description,
                    QuantityInStock = p.quantity_in_stock,
                    Price = p.price,
                    UrlImage = p.url_image,
                    Category = new CategoryVM
                    {
                        IdCategory = p.Category.id_category,
                        Name = p.Category.name,
                        Description = p.Category.description
                    },
                    Supplier = new SupplierVM
                    {
                        IdSupplier = p.Supplier.id_supplier,
                        NameCompany = p.Supplier.name_company,
                        Address = p.Supplier.address
                    }
                }).SingleOrDefaultAsync();
            return product;

        }

        public async Task<ProductVM> InsertAsync(Product entity)
        {
            // Kiểm tra xem Category có tồn tại không
            var existingCategory = await _dbContext.Categories.FindAsync(entity.id_category);
            if (existingCategory != null)
            {
                entity.Category = existingCategory;
            }

            // Kiểm tra xem Supplier có tồn tại không
            var existingSupplier = await _dbContext.Suppliers.FindAsync(entity.id_supplier);
            if (existingSupplier != null)
            {
                entity.Supplier = existingSupplier;
            }

            // Thêm Product vào DB
            _dbContext.Products.Add(entity);
            await _dbContext.SaveChangesAsync();

            return new ProductVM
            {
                IdProduct = entity.id_product,
                Name = entity.name,
                Description = entity.description,
                QuantityInStock = entity.quantity_in_stock,
                Price = entity.price,
                UrlImage = entity.url_image,
                Category = new CategoryVM
                {
                    IdCategory = entity.Category.id_category,
                    Name = entity.Category.name,
                    Description = entity.Category.description
                },
                Supplier = new SupplierVM
                {
                    IdSupplier = entity.Supplier.id_supplier,
                    NameCompany = entity.Supplier.name_company,
                    Address = entity.Supplier.address
                }
            };
        }


        public async Task UpdateAsync(Product entity)
        {
            _dbContext.Products.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
