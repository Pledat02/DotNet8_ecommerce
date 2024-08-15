using Ecommerce.Data;
using Ecommerce.PaginatedList;
using Microsoft.EntityFrameworkCore;

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
            var product = await _dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Where(p => p.id_product == id)
                .SingleOrDefaultAsync();
            product.Comments = await _dbContext.Comments
                .Include(c => c.User)
                .Where(c => c.id_product==product.id_product)
                .ToListAsync();
             return product;
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
        public async Task<List<Product>> GetListProductVegetable()
        {
            return await _dbContext.Products
                .Include (p => p.Category)
                .Where(p => p.Category.name == "Vegetable")
                .ToListAsync();
        }

        public async Task<List<Product>> GetTopProduct(int number)
        {
            var topProducts = await _dbContext.OrderDetails
                               .GroupBy(o => o.Product)
                               .Select(g => new
                                   {
                                       Product = g.Key,
                                       TotalQuantitySold = g.Sum(o => o.quantity)
                                   })
                               .OrderByDescending(x => x.TotalQuantitySold)
                               .Take(number)
                               .Select(x => x.Product)
                               .ToListAsync();

            return topProducts;

        }
        public async Task<List<int>> GetTopProductIdsAsync(int number)
        {
            return await _dbContext.OrderDetails
                .GroupBy(o => o.id_product) 
                .Select(g => new
                {
                    ProductId = g.Key, 
                    TotalQuantitySold = g.Sum(o => o.quantity) 
                })
                .OrderByDescending(x => x.TotalQuantitySold) 
                .Take(number) 
                .Select(x => x.ProductId) 
                .ToListAsync();
        }
        public async Task<List<Product>> GetTopProducts(int number)
        {
            var topProductIds = await GetTopProductIdsAsync(number);

            var topProducts = await _dbContext.Products
                .Where(p => topProductIds.Contains(p.id_product)) // Lọc theo danh sách id_product
                .Include(p => p.Category) // Bao gồm thông tin danh mục
                .ToListAsync();

            return topProducts;
        }
        public async Task<Dictionary<string, int>> GetCategoryWithQuantity()
        {
            var categoriesWithQuantities = await _dbContext.Products
                .GroupBy(p => p.Category.name) 
                .Select(g => new
                {
                    CategoryName = g.Key,
                    Quantity = g.Count() 
                })
                .ToListAsync();

            var categoryQuantityDictionary = categoriesWithQuantities
                .ToDictionary(c => c.CategoryName, c => c.Quantity);

            return categoryQuantityDictionary;
        }
        public async Task<IQueryable<Product>> SearchAsync(string keyword, string category, decimal? maxPrice)
        {
            var query = _dbContext.Products.Include(p => p.Category) .AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(p => p.name.ToLower().Contains(keyword.ToLower()) ||
                                         p.description.ToLower().Contains(keyword.ToLower()));
            }

            // Lọc theo danh mục nếu có
            if (category != null )
            {
                query = query.Where(p => p.Category.name == category);
            }

            // Xử lý lọc theo phạm vi giá
          
            if (maxPrice.HasValue && maxPrice>0)
            {
                query = query.Where(p => p.price <= maxPrice.Value);
            }

            return query;
        }
        // pagelist
        public async Task<PaginatedList<Product>> GetPagedProductsAsync(int pageNumber, int pageSize)
        {
            var source = _dbContext.Products.Include(p => p.Category).AsQueryable();
            return await PaginatedList<Product>.CreateAsync(source, pageNumber, pageSize);
        }
        // RelatedProducts

        public async Task<List<Product>> GetSampeCategoryProductAsync(int id_category)
        {
            return await _dbContext.Products.Where(p => p.id_category == id_category).ToListAsync();
        }
        public async Task<bool> CheckIsSoldAsync(int id_product , int id_user)
        {
            bool isSold = await _dbContext.OrderDetails
                            .Join(_dbContext.Orders,
                                  detail => detail.id_order,    // Key from OrderDetails table
                                  order => order.id_order,      // Key from Orders table
                                  (detail, order) => new { detail, order }) // Result selector
                            .Where(od => od.order.Voucher_User.id_user == id_user && od.detail.id_product == id_product)
                            .AnyAsync();
            return isSold;
        }
        public async Task<bool> CheckStocking(int id_product)
        {   
            var product = await _dbContext.Products
                .SingleOrDefaultAsync(p => p.id_product == id_product);
            if (product.quantity_in_stock > 0)
                return true;
            return false;
        }
        public async Task<Comment> PostComment(Comment comment)
        {
           return  await new CommentService(_dbContext).InsertAsync(comment);
        }
        public async Task<Bill> Chackout(Bill bill)
        {
            return await new BillService(_dbContext).InsertAsync(bill);
        }
    }



}

