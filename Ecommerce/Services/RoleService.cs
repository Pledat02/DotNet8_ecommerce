using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    public class RoleService : Iservice<Role>
    {
        private readonly MyDBContext _dBContext;
        
        public RoleService (MyDBContext dbContext)
        {
            _dBContext = dbContext;
        }

        public Task DeleteAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Role>> GetAllAsync()
        {
            return await _dBContext.Roles.ToListAsync();
        }

        public Task<Role> GetOneAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<Role> InsertAsync(Role entity)
        {
            throw new NotImplementedException();
        }

        public bool IsExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Role entity)
        {
            throw new NotImplementedException();
        }
    }
}
