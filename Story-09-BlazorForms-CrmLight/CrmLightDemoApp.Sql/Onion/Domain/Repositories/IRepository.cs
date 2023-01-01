namespace CrmLightDemoApp.Onion.Domain.Repositories
{
    public interface IRepository<T>
        where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<int> CreateAsync(T data);
        Task UpdateAsync(T data);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task<List<T>> GetListByIdsAsync(IEnumerable<int> ids);

        ContextQuery<T> GetContextQuery();
        Task<List<T>> RunContextQueryAsync(ContextQuery<T> ctx);
    }
}
