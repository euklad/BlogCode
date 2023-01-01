namespace CrmLightDemoApp.Onion.Domain.Repositories
{
    public class ContextQuery<T> : IDisposable
        where T : class 
    {
        protected readonly IDisposable _context;

        public IQueryable<T> Query { get; set; }

        public ContextQuery(IDisposable context, IQueryable<T> query)
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
