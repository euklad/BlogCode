using BlazorForms.Flows;
using BlazorForms.Shared;
using CrmLightDemoApp.Onion.Domain;
using CrmLightDemoApp.Onion.Domain.Repositories;

namespace CrmLightDemoApp.Onion.Infrastructure
{
    public class CompanyRepository : LocalCacheRepository<Company>, ICompanyRepository
    {
        public CompanyRepository()
        {
            // pre fill some data
            _localCache.Add(new Company { Id = 1, Name = "Mizeratti Pty Ltd", RegistrationNumber = "99899632221", EstablishedDate = new DateTime(1908, 1, 17) });
            _localCache.Add(new Company { Id = 2, Name = "Alpha Pajero", RegistrationNumber = "89963222172", EstablishedDate = new DateTime(1956, 5, 14) });
            _localCache.Add(new Company { Id = 3, Name = "Zeppelin Ltd Inc", RegistrationNumber = "63222172899", EstablishedDate = new DateTime(2019, 11, 4) });
            _localCache.Add(new Company { Id = 4, Name = "Perpetuum Automotives Inc", RegistrationNumber = "22217289963", EstablishedDate = new DateTime(2010, 1, 7) });
            _id = 10;
        }
    }
}
