using Ecommerce.Data;
using Ecommerce.PaginatedList;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Ecommerce.Services
{
    public class HomeService
    {
        private readonly MyDBContext _dbContext;

        public HomeService(MyDBContext context)
        {
            _dbContext = context;
        }
        public async Task<Account> InsertAccount(Account acc)
        {
            // Check if username already exists
            acc.password = HashPassword(acc.password);
            var existingAccount = await _dbContext.Accounts.SingleOrDefaultAsync(a => a.username == acc.username);
            if (existingAccount != null)
            {
                throw new InvalidOperationException("Username already exists.");
            }

            _dbContext.Accounts.Add(acc);
            await _dbContext.SaveChangesAsync();

            return await _dbContext.Accounts.AsNoTracking().SingleOrDefaultAsync(a => a.username == acc.username);
        }
        public async Task CreateUserAsync (User user)
        {
            await new UserService(_dbContext).InsertAsync(user);
        }
        public async Task<Account> LoginAsync(string username, string password)
        {
            string hashedPassword = HashPassword(password);
            var account = await _dbContext.Accounts
         .Where(a => a.username ==username && a.password== HashPassword(password))
         .SingleOrDefaultAsync();
            if (account == null)
            {
                throw new InvalidDataException("Invalid username or password.");
            }
            account.Staff = new StaffService(_dbContext).GetOneByIdAccount(account.id_account);
            account.User = new UserService(_dbContext).GetOneByIdAccount(account.id_account);
            return account;
        }
        public async Task<dynamic> GetOwnAsync(Account acc)
        {
            if (acc.type == 0)
            {
                return await _dbContext.Users.SingleOrDefaultAsync(u => u.id_account == acc.id_account);
            }
            else
            {
                return await _dbContext.Staffs.SingleOrDefaultAsync(s => s.id_account == acc.id_account);
            }
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
                .Include(p => p.Category)
                .Where(p => p.Category.name == "Rau xanh")
                .ToListAsync();
        }
        // Lấy danh sách tất cả các danh mục
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _dbContext.Categories
                .ToListAsync();
        }
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
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
                .Include(p=> p.Comments)
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
            var query = _dbContext.Products.Include(p => p.Category).AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(p => p.name.ToLower().Contains(keyword.ToLower()) ||
                                         p.description.ToLower().Contains(keyword.ToLower()));
            }

            // Lọc theo danh mục nếu có
            if (category != null)
            {
                query = query.Where(p => p.Category.name == category);
            }

            // Xử lý lọc theo phạm vi giá

            if (maxPrice.HasValue && maxPrice > 0)
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
    }
}
