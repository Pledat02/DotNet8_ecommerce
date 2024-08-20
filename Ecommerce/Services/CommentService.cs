
using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    public class CommentService : Iservice<Comment>
    {
        private readonly MyDBContext _dbContext;

        public CommentService(MyDBContext context)
        {
            _dbContext = context;

        }

        public Task DeleteAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Comment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Comment> GetOneAsync(int? id)
        {
           return await _dbContext.Comments.Include(c =>c.User).SingleOrDefaultAsync(c => c.id_comment == id);
        }

        public async Task<Comment> InsertAsync(Comment entity)
        {
            _dbContext.Comments.Add(entity);
            await _dbContext.SaveChangesAsync();
            return await GetOneAsync(entity.id_comment);
        }

        public bool IsExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Comment entity)
        {
            throw new NotImplementedException();
        }
    }
}
