using CrmLightDemoApp.Onion.Domain;
using CrmLightDemoApp.Onion.Domain.Repositories;

namespace CrmLightDemoApp.Onion.Infrastructure
{
    public class CompanyRepository : SqlRepository<Company>, ICompanyRepository
    {
    }
}
