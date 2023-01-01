using CrmLightDemoApp.Onion.Domain;
using Microsoft.EntityFrameworkCore;

namespace CrmLightDemoApp.Onion.Infrastructure
{
    public class CrmContext : DbContext
    {
        public DbSet<Company> Company { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<PersonCompanyLink> PersonCompanyLink { get; set; }
        public DbSet<PersonCompanyLinkType> PersonCompanyLinkType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database=CrmLightDb1;Trusted_Connection=True;MultipleActiveResultSets=true");
    }
}
