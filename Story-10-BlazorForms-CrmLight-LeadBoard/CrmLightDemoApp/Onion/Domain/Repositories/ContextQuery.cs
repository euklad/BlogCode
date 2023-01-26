using Microsoft.EntityFrameworkCore;

namespace CrmLightDemoApp.Onion.Domain.Repositories
{
    public class ContextQuery<T> : IDisposable
        where T : class 
    {
        internal readonly DbContext _context;

        public IQueryable<T> Query { get; set; }

        public ContextQuery(DbContext context, IQueryable<T> query)
        {
            _context = context;
            Query = query;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
