using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;

namespace Ecommerce.Services
{
    public class AccountService : Iservice<Account>
    {
        private readonly MyDBContext _dbContext;

        public AccountService(MyDBContext context)
        {
            _dbContext = context;
        }
       
        public Task DeleteAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Account>> GetAllAsync()
        {
            throw new NotImplementedException();
        }


        public Task<Account> GetOneAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<Account> InsertAsync(Account entity)
        {
            throw new NotImplementedException();
        }

        public bool IsExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Account entity)
        {
            throw new NotImplementedException();
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
