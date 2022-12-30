using BlazorForms.Flows;
using BlazorForms.Shared;
using CrmLightDemoApp.Onion.Domain;
using CrmLightDemoApp.Onion.Domain.Repositories;

namespace CrmLightDemoApp.Onion.Infrastructure
{
    public class PersonCompanyLinkTypeRepository : LocalCacheRepository<PersonCompanyLinkType>, IPersonCompanyLinkTypeRepository
    {
        public PersonCompanyLinkTypeRepository() 
        {
            // pre fill some data
            _localCache.Add(new PersonCompanyLinkType { Id = 1, Name = "Director" });
            _localCache.Add(new PersonCompanyLinkType { Id = 2, Name = "Sales Person" });
            _localCache.Add(new PersonCompanyLinkType { Id = 3, Name = "HR" });
            _localCache.Add(new PersonCompanyLinkType { Id = 4, Name = "External Agent" });
            _localCache.Add(new PersonCompanyLinkType { Id = 5, Name = "Employee" });
            _id = 10;
        }
    }
}
