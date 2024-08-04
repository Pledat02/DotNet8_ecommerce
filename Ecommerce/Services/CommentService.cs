using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class CommentService : Iservice<Comment, CommentVM>
    {
        private readonly MyDBContext _dbContext;

        public CommentService(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task DeleteAsync(int id)
        {
            var comment = await _dbContext.Comments
                .FirstOrDefaultAsync(c => c.id_comment == id);
            if (comment != null)
            {
                _dbContext.Comments.Remove(comment);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<CommentVM>> GetAllAsync()
        {
            var list = await _dbContext.Comments.Select(c =>
                new CommentVM
                {
                    IdComment = c.id_comment,
                    // IdUser = c.id_user,
                    Product = new ProductVM
                    {
                        IdProduct = c.Product.id_product,
                        Name = c.Product.name,
                        UrlImage = c.Product.url_image,
                        Price = c.Product.price,
                        Description = c.Product.description,
                        QuantityInStock = c.Product.quantity_in_stock
                    },
                    CreatedDate = c.created_date,
                    Text = c.text,
                    Rating = c.rating
                }).ToListAsync();
            return list;
        }

        public async Task<CommentVM> GetOneAsync(int id)
        {
            var comment = await _dbContext.Comments
                .Where(c => c.id_comment == id)
                .Select(c => new CommentVM
                {
                  IdComment = c.id_comment,
                    // IdUser = c.id_user,
                    Product = new ProductVM
                    {
                        IdProduct = c.Product.id_product,
                        Name = c.Product.name,
                        UrlImage = c.Product.url_image,
                        Price = c.Product.price,
                        Description = c.Product.description,
                        QuantityInStock = c.Product.quantity_in_stock
                    },
                    CreatedDate = c.created_date,
                    Text = c.text,
                    Rating = c.rating
                }).SingleOrDefaultAsync();
            return comment;
        }

        public async Task<CommentVM> InsertAsync(Comment entity)
        {
            _dbContext.Comments.Add(entity);
            await _dbContext.SaveChangesAsync();

            return new CommentVM
            {
                IdComment = entity.id_comment,
                // IdUser = entity.IdUser,
               Product = new ProductVM
                    {
                        IdProduct = entity.Product.id_product,
                        Name = entity.Product.name,
                        UrlImage = entity.Product.url_image,
                        Price = entity.Product.price,
                        Description = entity.Product.description,
                        QuantityInStock = entity.Product.quantity_in_stock
                    },
                CreatedDate = entity.created_date,
                Text = entity.text,
                Rating = entity.rating
            };
        }

        public async Task UpdateAsync(Comment entity)
        {
            _dbContext.Comments.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
