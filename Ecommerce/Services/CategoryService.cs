using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    public class CategoryService : Iservice<Category>
    {
        private readonly MyDBContext _dbContext;

        public CategoryService(MyDBContext context)
        {
            _dbContext = context;
        }

        public async Task DeleteAsync(int? id)
        {

            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(c => c.id_category == id);

            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Category not found.");
            }
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetOneAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Category ID cannot be null.");
            }

            return await _dbContext.Categories
                .FirstOrDefaultAsync(c => c.id_category == id)
                ?? throw new KeyNotFoundException("Category not found.");
        }

        public async Task<Category> InsertAsync(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Category cannot be null.");
            }

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }

        public async Task UpdateAsync(Category category)
        {
           
                _dbContext.Categories.Update(category);
                await _dbContext.SaveChangesAsync();
           
        }

        public bool IsExists(int id)
        {
            return _dbContext.Categories.Any(e => e.id_category == id);
        }
    }
}
