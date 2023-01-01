namespace CrmLightDemoApp.Onion.Domain.Repositories
{
    public interface IPersonCompanyRepository : IRepository<PersonCompanyLink>
    {
        Task<List<PersonCompanyLinkDetails>> GetByPersonIdAsync(int personId);
        Task<List<PersonCompanyLinkDetails>> GetByCompanyIdAsync(int companyId);
    }
}
