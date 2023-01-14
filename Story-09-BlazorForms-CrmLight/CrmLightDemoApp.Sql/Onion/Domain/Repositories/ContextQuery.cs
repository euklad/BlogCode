using Microsoft.EntityFrameworkCore;

namespace CrmLightDemoApp.Onion.Domain.Repositories
{
    public class ContextQuery<T> : IDisposable
        where T : class 
    {
        internal readonly DbContext _context;
        internal readonly IEnumerable<IDisposable> _moreContexts;

        public IQueryable<T> Query { get; set; }

        public ContextQuery(DbContext context, IQueryable<T> query, params IDisposable[] moreContexts)
        {
            _context = context;
            Query = query;
            _moreContexts = moreContexts;
        }

        public void Dispose()
        {
            _context?.Dispose();

            if (_moreContexts != null)
            {
                _moreContexts.ToList().ForEach(x => x.Dispose());
            }
        }
    }
}
