using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

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
            var accout = await _dbContext.Accounts.Where(a => a.User.id_user == id).FirstOrDefaultAsync();
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.Accounts.Remove(accout);
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
            return await _dbContext.Users.Include(a => a.Account)
                .ToListAsync();
        }

        public async Task<User> GetOneAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "User ID cannot be null.");
            }

            return await _dbContext.Users.Include(u => u.Account)
                .FirstOrDefaultAsync(c => c.id_user == id)
                ?? throw new KeyNotFoundException("User not found.");
        }
        public async Task<Account> GetAccountAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "User ID cannot be null.");
            }

            return await _dbContext.Accounts
                .FirstOrDefaultAsync(c => c.User.id_user == id)
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
        public async Task UpdateAsync2(Account account)
        {
            _dbContext.Accounts.Update(account);
            await _dbContext.SaveChangesAsync();
        }

        public bool IsExists(int id)
        {
            return _dbContext.Users.Any(e => e.id_user == id);
        }



        // Lấy danh sách tất cả các danh mục
        public async Task<List<Account>> GetAccountAsync()
        {
            return await _dbContext.Accounts
                .ToListAsync();
        }
        public async Task<List<Account>> GetAccountsWithoutUsersAsync()
        {
            return await _dbContext.Accounts
                .Where(a => a.User == null)
                .ToListAsync();
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

    }
}
