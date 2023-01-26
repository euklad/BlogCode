using BlazorForms.Flows;
using BlazorForms.Shared;
using CrmLightDemoApp.Onion.Domain;
using CrmLightDemoApp.Onion.Domain.Repositories;

namespace CrmLightDemoApp.Onion.Infrastructure
{
    public class LeadSourceTypeRepository : LocalCacheRepository<LeadSourceType>
    {
        public LeadSourceTypeRepository() 
        {
            // pre fill some data
            _localCache.Add(new LeadSourceType { Id = 1, Name = "LinkedIn" });
            _localCache.Add(new LeadSourceType { Id = 2, Name = "Media Ads" });
            _localCache.Add(new LeadSourceType { Id = 3, Name = "Sales Contact" });
            _localCache.Add(new LeadSourceType { Id = 4, Name = "Meta" });
            _localCache.Add(new LeadSourceType { Id = 5, Name = "Open-source" });
            _id = 10;
        }
    }
}
