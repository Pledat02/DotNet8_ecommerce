namespace Ecommerce.Services
{
    public interface Iservice<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetOneAsync(int? id);
        Task<T> InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int? id);
        bool IsExists(int id);
    }
}
