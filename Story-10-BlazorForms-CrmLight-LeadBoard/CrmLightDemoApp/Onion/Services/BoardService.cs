using BlazorForms.Shared;
using CrmLightDemoApp.Onion.Domain;
using CrmLightDemoApp.Onion.Domain.Repositories;
using CrmLightDemoApp.Onion.Infrastructure;
using CrmLightDemoApp.Onion.Services.Abstractions;
using CrmLightDemoApp.Onion.Services.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CrmLightDemoApp.Onion.Services
{
    public class BoardService : IBoardService
    {
        private readonly IBoardCardRepository _repo;
        private readonly IPersonRepository _personRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IClientCompanyRepository _clientCompanyRepository;
        private readonly IRepository<LeadSourceType> _leadSourceTypeRepository;
        private readonly IBoardCardHistoryRepository _boardCardHistoryRepository;
        private readonly IAppAuthState _appAuthState;

        public BoardService(IBoardCardRepository repo, IPersonRepository personRepository, IClientCompanyRepository clientCompanyRepository,
			ICompanyRepository companyRepository, IRepository<LeadSourceType> leadSourceTypeRepository,
            IBoardCardHistoryRepository boardCardHistoryRepository, IAppAuthState appAuthState) 
        { 
            _repo = repo;
            _personRepository = personRepository;
            _clientCompanyRepository = clientCompanyRepository;
			_companyRepository = companyRepository;
            _leadSourceTypeRepository = leadSourceTypeRepository;
            _boardCardHistoryRepository = boardCardHistoryRepository;
            _appAuthState = appAuthState;
        }

        public async Task<int> CreateBoardCardAsync(LeadBoardCardModel card)
        {
            var item = new BoardCard();
            card.ReflectionCopyTo(item);
            card.Id = await _repo.CreateAsync(item);
            return card.Id;
        }

		public async Task CreatingBoardCardAsync(LeadBoardCardModel card)
		{
            card.AllPersons = await GetAllPersons();
            card.AllCompanies = await GetAllCompanies();
            card.AllLeadSources = await GetAllLeadTypes();
		}

		public async Task DeleteBoardCardAsync(LeadBoardCardModel card)
        {
            await _repo.SoftDeleteAsync(card);
        }

        private async Task<List<LeadSourceType>> GetAllLeadTypes()
        {
            return await _leadSourceTypeRepository.GetAllAsync();
		}

        private async Task<List<CompanyModel>> GetAllCompanies()
        {
            return (await _companyRepository.GetAllAsync())
                .Select(x =>
                {
                    var item = new CompanyModel();
                    x.ReflectionCopyTo(item);
                    return item;
                }).OrderBy(x => x.Name).ToList();
        }

        private async Task<List<PersonModel>> GetAllPersons()
        {
            return (await _personRepository.GetAllAsync())
				.Select(x =>
				{
					var item = new PersonModel();
					x.ReflectionCopyTo(item);
					item.FullName = $"{x.FirstName} {x.LastName}";
					return item;
				}).OrderBy(x => x.FullName).ToList();
		}

		public async Task<List<LeadBoardCardModel>> GetBoardCardsAsync()
        {
            var persons = await GetAllPersons();
            var companies = await GetAllCompanies();
			var leadTypes = await GetAllLeadTypes();

			var items = (await _repo.GetAllAsync()).Select(x =>
            {
                var item = new LeadBoardCardModel();
                x.ReflectionCopyTo(item);
                item.AllPersons = persons;
                item.AllCompanies = companies;
                item.AllLeadSources = leadTypes;
                return item;
            }).OrderBy(x => x.Order).ToList();

            return items;
        }

        public async Task UpdateBoardCardAsync(LeadBoardCardModel card)
        {
            var item = new BoardCard();
            card.ReflectionCopyTo(item);
            await _repo.UpdateAsync(item);

            if (!string.IsNullOrWhiteSpace(card.Comments))
            {
                var comment = new BoardCardHistory
                {
                    BoardCardId = card.Id,
                    Title = "Comment",
                    Text = card.Comments,
                    PersonId = _appAuthState.GetCurrentUser().Id,
                    Date = DateTime.Now,
                };

                await _boardCardHistoryRepository.CreateAsync(comment);
            }
        }

		public async Task<int> CreateCompanyAsync(Company company)
		{
			return await _companyRepository.CreateAsync(company);
		}

		public async Task<int> CreateClientCompanyAsync(ClientCompany clientCompany)
		{
			return await _clientCompanyRepository.CreateAsync(clientCompany);
		}

		public async Task UpdateClientCompanyAsync(ClientCompany clientCompany)
		{
			await _clientCompanyRepository.UpdateAsync(clientCompany);
		}

        public async Task<ClientCompany> FindClientCompanyAsync(int companyId)
        {
            return await _clientCompanyRepository.FindByCompanyIdAsync(companyId);
        }
    }
}
