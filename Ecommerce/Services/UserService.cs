using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    public class UserService : Iservice<User>
    {
        private readonly MyDBContext _dbContext;

        public UserService(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task DeleteAsync(int? id)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(p => p.id_user == id);

            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("User not found.");
            }
        }
        public User GetOneByIdAccount(int? id)
        {
            var User = _dbContext.Users.FirstOrDefault(s => s.id_account == id);
            return User;
        }
        public async Task<List<User>> GetAllAsync()
        {
            return await _dbContext.Users              
                .ToListAsync();
        }

        public async Task<User> GetOneAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "User ID cannot be null.");
            }

            return await _dbContext.Users
                .FirstOrDefaultAsync(c => c.id_user == id)
                ?? throw new KeyNotFoundException("User not found.");
        }

        public async Task<User> InsertAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public bool IsExists(int id)
        {
            return _dbContext.Users.Any(e => e.id_user == id);
        }

        // Lấy danh sách tất cả các danh mục
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _dbContext.Categories
                .ToListAsync();
        }

        
    }
}
