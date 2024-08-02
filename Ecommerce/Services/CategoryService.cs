using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    public class CategoryService : Iservice<Category, CategoryVM>
    {
        private readonly MyDBContext _dbContext;

        public CategoryService(MyDBContext context)
        {
            _dbContext = context;
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(c =>c.id_category==id);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<CategoryVM>> GetAllAsync()
        {
            var list = await _dbContext.Categories
                .Select(c => new CategoryVM
                {
                    IdCategory = c.id_category,
                    Name = c.name,
                    Description = c.description
                })
                .ToListAsync();

            return list;
        }

        public async Task<CategoryVM> GetOneAsync(int id)
        {
            var category = await _dbContext.Categories
                .Where(c => c.id_category == id)
                .Select(c => new CategoryVM
                {
                    IdCategory = c.id_category,
                    Name = c.name,
                    Description = c.description
                })
                .SingleOrDefaultAsync();

            return category;
        }

        public async Task<CategoryVM> InsertAsync(Category entity)
        {
            _dbContext.Categories.Add(entity);
            await _dbContext.SaveChangesAsync();

            return new CategoryVM
            {
                IdCategory = entity.id_category,
                Name = entity.name,
                Description = entity.description
            };
        }

        public async Task UpdateAsync(Category entity)
        {
            _dbContext.Categories.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
