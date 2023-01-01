namespace CrmLightDemoApp.Onion.Domain.Repositories
{
    public interface IRepository<T>
        where T : class
    {
        Task<List<T>> GetAllAsync();
        IQueryable<T> GetAllQuery();
        Task<List<T>> RunQueryAsync(IQueryable<T> query);
        Task<T> GetByIdAsync(int id);
        Task<int> CreateAsync(T data);
        Task UpdateAsync(T data);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task<List<T>> GetListByIdsAsync(IEnumerable<int> ids);
    }
}
