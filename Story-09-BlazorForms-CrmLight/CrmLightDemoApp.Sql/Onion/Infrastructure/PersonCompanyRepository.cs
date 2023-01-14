using BlazorForms.Shared;
using CrmLightDemoApp.Onion.Domain;
using CrmLightDemoApp.Onion.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CrmLightDemoApp.Onion.Infrastructure
{
    public class PersonCompanyRepository : SqlRepository<PersonCompanyLink>, IPersonCompanyRepository
    {
        private readonly IPersonRepository _personRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IPersonCompanyLinkTypeRepository _personCompanyLinkTypeRepository;

        public PersonCompanyRepository(IPersonRepository personRepository, ICompanyRepository companyRepository,
            IPersonCompanyLinkTypeRepository personCompanyLinkTypeRepository) 
        { 
            _personRepository = personRepository;
            _companyRepository = companyRepository;
            _personCompanyLinkTypeRepository = personCompanyLinkTypeRepository;
        }

        public async Task<List<PersonCompanyLinkDetails>> RunDetailsContextQueryAsync(ContextQuery<PersonCompanyLinkDetails> query)
        {
            return await query.Query.ToListAsync();
        }

        public ContextQuery<PersonCompanyLinkDetails> GetDetailsContextQuery()
        {
            var cq = _companyRepository.GetContextQuery();
            var pq = _personRepository.GetContextQuery(cq._context);

            var ccq = GetContextQuery(cq._context);

            var q = from cc in ccq.Query
                    join c in cq.Query on cc.CompanyId equals c.Id into cJoin
                    from subCompany in cJoin.DefaultIfEmpty()
                    join p in pq.Query on cc.PersonId equals p.Id into pJoin
                    from subPerson in pJoin.DefaultIfEmpty()
                    select new PersonCompanyLinkDetails
                    {
                        Id = cc.Id,
                        CompanyId = cc.CompanyId,
                        PersonId = cc.PersonId,
                        LinkTypeId = cc.LinkTypeId,
                        Deleted = cc.Deleted,
                        CompanyName = subCompany.Name,
                        PersonFirstName = subPerson.FirstName,
                        PersonLastName = subPerson.LastName,
                        PersonFullName = $"{subPerson.FirstName} {subPerson.LastName}",
                    };

            return new ContextQuery<PersonCompanyLinkDetails>(cq._context, q);
        }

        public async Task<List<PersonCompanyLinkDetails>> GetByPersonIdAsync(int personId)
        {
            using var db = new CrmContext();

            var list = (await db.Set<PersonCompanyLink>().Where(x => !x.Deleted && x.PersonId == personId).ToListAsync()).Select(x => 
            {
                var item = new PersonCompanyLinkDetails();
                x.ReflectionCopyTo(item);
                return item;
            }).ToList();

            var person = await _personRepository.GetByIdAsync(personId);
            var companyIds = list.Select(x => x.CompanyId).Distinct().ToList();
            var companies = (await _companyRepository.GetListByIdsAsync(companyIds)).ToDictionary(x => x.Id, x => x);
            var linkIds = list.Select(x => x.LinkTypeId).Distinct().ToList();
            var links = (await _personCompanyLinkTypeRepository.GetListByIdsAsync(linkIds)).ToDictionary(x => x.Id, x => x);

            foreach (var item in list)
            {
                item.LinkTypeName = links[item.LinkTypeId].Name;
                item.PersonFullName = $"{person.FirstName} {person.LastName}";
                item.PersonFirstName = person.FirstName;
                item.PersonLastName = person.LastName;
                item.CompanyName = companies[item.CompanyId].Name;
            }

            return list;
        }

        public async Task<List<PersonCompanyLinkDetails>> GetByCompanyIdAsync(int companyId)
        {
            using var db = new CrmContext();

            var list = (await db.Set<PersonCompanyLink>().Where(x => !x.Deleted && x.CompanyId == companyId).ToListAsync()).Select(x =>
            {
                var item = new PersonCompanyLinkDetails();
                x.ReflectionCopyTo(item);
                return item;
            }).ToList();

            var company = await _companyRepository.GetByIdAsync(companyId);
            var personIds = list.Select(x => x.PersonId).Distinct().ToList();
            var persons = (await _personRepository.GetListByIdsAsync(personIds)).ToDictionary(x => x.Id, x => x);
            var linkIds = list.Select(x => x.LinkTypeId).Distinct().ToList();
            var links = (await _personCompanyLinkTypeRepository.GetListByIdsAsync(linkIds)).ToDictionary(x => x.Id, x => x);

            foreach (var item in list)
            {
                item.LinkTypeName = links[item.LinkTypeId].Name;
                item.PersonFullName = $"{persons[item.PersonId].FirstName} {persons[item.PersonId].LastName}";
                item.PersonFirstName = persons[item.PersonId].FirstName;
                item.PersonLastName = persons[item.PersonId].LastName;
                item.CompanyName = company.Name;
            }

            return list;
        }
    }
}
