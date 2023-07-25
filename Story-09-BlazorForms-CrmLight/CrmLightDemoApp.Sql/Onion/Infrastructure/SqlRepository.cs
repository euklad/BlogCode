using CrmLightDemoApp.Onion.Domain;
using CrmLightDemoApp.Onion.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CrmLightDemoApp.Onion.Infrastructure
{
    public class SqlRepository<T> : IRepository<T>
        where T : class, IEntity, new()
    {
        public async Task<int> CreateAsync(T data)
        {
            using var db = new CrmContext();
            db.Set<T>().Add(data);
            await db.SaveChangesAsync();
            return data.Id;
        }

        public async Task DeleteAsync(int id)
        {
            using var db = new CrmContext();
            var table = db.Set<T>();
            var entity = new T { Id = id };
            table.Attach(entity);
            table.Remove(entity);
            await db.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            using var db = new CrmContext();
            return await db.Set<T>().SingleAsync(x => x.Id == id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            using var db = new CrmContext();
            return await db.Set<T>().Where(x => !x.Deleted).ToListAsync();
        }

        public async Task UpdateAsync(T data)
        {
            using var db = new CrmContext();
            db.Set<T>().Update(data);
            await db.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            using var db = new CrmContext();
            var record = await db.Set<T>().SingleAsync(x => x.Id == id);
            record.Deleted = true;
            await db.SaveChangesAsync();
        }

        public async Task<List<T>> GetListByIdsAsync(IEnumerable<int> ids)
        {
            using var db = new CrmContext();
            return await db.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public ContextQuery<T> GetContextQuery()
        {
            var db = new CrmContext();
            return new ContextQuery<T>(db, db.Set<T>().Where(x => !x.Deleted));

            var qq = db.Company.Include(P => P.RefPersonCompanyLink).ThenInclude(l => l.Person);
        }

        public async Task<List<T>> RunContextQueryAsync(ContextQuery<T> query)
        {
            return await query.Query.ToListAsync();
        }
    }
}
