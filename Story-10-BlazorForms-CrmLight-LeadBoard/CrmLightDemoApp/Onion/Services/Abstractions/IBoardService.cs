using CrmLightDemoApp.Onion.Domain;
using CrmLightDemoApp.Onion.Services.Model;

namespace CrmLightDemoApp.Onion.Services.Abstractions
{
    public interface IBoardService
    {
        Task<List<LeadBoardCardModel>> GetBoardCardsAsync();
        Task CreatingBoardCardAsync(LeadBoardCardModel card);
        Task DeleteBoardCardAsync(LeadBoardCardModel card);
        Task<int> CreateBoardCardAsync(LeadBoardCardModel card);
        Task UpdateBoardCardAsync(LeadBoardCardModel card);

        Task<int> CreateCompanyAsync(Company company);
        Task<ClientCompany> FindClientCompanyAsync(int companyId);
        Task<int> CreateClientCompanyAsync(ClientCompany clientCompany);
        Task UpdateClientCompanyAsync(ClientCompany clientCompany);

	}
}
