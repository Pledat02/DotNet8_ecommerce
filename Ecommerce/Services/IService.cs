namespace Ecommerce.Services
{
    public interface Iservice<TEntity,TViewModel>
    {
        Task<List<TViewModel>> GetAllAsync();
        Task<TViewModel> GetOneAsync(int id);
        Task<TViewModel> InsertAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
    }
}
