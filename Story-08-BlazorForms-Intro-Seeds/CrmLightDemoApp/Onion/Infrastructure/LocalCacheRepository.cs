using BlazorForms.Flows;
using BlazorForms.Shared;
using CrmLightDemoApp.Onion.Domain;
using CrmLightDemoApp.Onion.Domain.Repositories;

namespace CrmLightDemoApp.Onion.Infrastructure
{
    // this is repository emulator that stores all data in memory
    // it stores and retrieves object copies, like a real database
    public class LocalCacheRepository<T> : IRepository<T>
        where T : class, IEntity
    {
        protected int _id = 0;
        protected readonly List<T> _localCache = new List<T>();

        public async Task<int> CreateAsync(T data)
        {
            _id++;
            data.Id = _id;
            _localCache.Add(data.GetCopy());
            return _id;
        }

        public async Task DeleteAsync(int id)
        {
            _localCache.Remove(_localCache.Single(x => x.Id == id));
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return _localCache.Single(x => x.Id == id).GetCopy();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return _localCache.Where(x => !x.Deleted).Select(x => x.GetCopy()).ToList();
        }

        public async Task UpdateAsync(T data)
        {
            await DeleteAsync(data.Id);
            _localCache.Add(data.GetCopy());
        }

        public async Task SoftDeleteAsync(int id)
        {
            _localCache.Single(x => x.Id == id).Deleted = true;
        }

        public async Task<List<T>> GetListByIdsAsync(IEnumerable<int> ids)
        {
            return _localCache.Where(x => ids.Contains(x.Id)).Select(x => x.GetCopy()).ToList();
        }

        public IQueryable<T> GetAllQuery()
        {
            return _localCache.AsQueryable();
        }

        public async Task<List<T>> RunQueryAsync(IQueryable<T> query)
        {
            return query.ToList();
        }
    }
}
