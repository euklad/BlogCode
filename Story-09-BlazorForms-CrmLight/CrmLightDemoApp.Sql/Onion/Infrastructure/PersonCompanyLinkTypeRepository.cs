using CrmLightDemoApp.Onion.Domain;
using CrmLightDemoApp.Onion.Domain.Repositories;

namespace CrmLightDemoApp.Onion.Infrastructure
{
    public class PersonCompanyLinkTypeRepository : SqlRepository<PersonCompanyLinkType>, IPersonCompanyLinkTypeRepository
    {
    }
}
