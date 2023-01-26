using BlazorForms.Flows;
using BlazorForms.Shared;
using CrmLightDemoApp.Onion.Domain;
using CrmLightDemoApp.Onion.Domain.Repositories;
using CrmLightDemoApp.Onion.Services.Model;
using Microsoft.EntityFrameworkCore;

namespace CrmLightDemoApp.Onion.Infrastructure
{
    public class ClientCompanyRepository : LocalCacheRepository<ClientCompany>, IClientCompanyRepository
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IPersonRepository _personRepository;

        public ClientCompanyRepository(IPersonRepository personRepository, ICompanyRepository companyRepository)
        {
            _personRepository = personRepository;
            _companyRepository = companyRepository;

            // pre fill some data
            _localCache.Add(new ClientCompany { Id = 1, CompanyId = 1, ClientManagerId = 5, StartContractDate = new DateTime(2023, 1, 14) });
            _id = 10;
        }

        public async Task<List<ClientCompanyDetails>> RunAllDetailsContextQueryAsync(ContextQuery<ClientCompanyDetails> ctx)
        {
            return ctx.Query.ToList();
        }

        public ContextQuery<ClientCompanyDetails> GetAllDetailsContextQuery()
        {
            var cq = _companyRepository.GetContextQuery();
            var pq = _personRepository.GetContextQuery(cq._context);
            var ccq = GetContextQuery(cq._context);

            var q = from cc in ccq.Query
                    join c in cq.Query on cc.CompanyId equals c.Id into cJoin
                    from subCompany in cJoin.DefaultIfEmpty()
                    join p in pq.Query on cc.ClientManagerId equals p.Id into pJoin
                    from subPerson in pJoin.DefaultIfEmpty()
                    join p2 in pq.Query on cc.AlternativeClientManagerId equals p2.Id into p2Join
                    from subPerson2 in p2Join.DefaultIfEmpty()
                    select new ClientCompanyDetails 
                    {
                        Id = cc.Id,
                        CompanyId = cc.CompanyId,
                        StartContractDate = cc.StartContractDate,
                        ClientManagerId = cc.ClientManagerId,
                        AlternativeClientManagerId = cc.AlternativeClientManagerId,
                        Deleted = cc.Deleted,
                        CompanyName = subCompany != null ? subCompany.Name : null,
                        ManagerFirstName = subPerson != null ? subPerson.FirstName : null,
                        ManagerLastName = subPerson != null ? subPerson.LastName : null,
                        AlternativeManagerFirstName = subPerson2 != null ? subPerson2.FirstName : null,
                        AlternativeManagerLastName = subPerson2 != null ? subPerson2.LastName : null,
                    };

            return new ContextQuery<ClientCompanyDetails>(cq._context, q);
        }

        public async Task<ClientCompany> FindByCompanyIdAsync(int companyId)
        {
            var result = _localCache.FirstOrDefault(x => x.CompanyId == companyId && !x.Deleted);
            return result;
        }
    }
}
