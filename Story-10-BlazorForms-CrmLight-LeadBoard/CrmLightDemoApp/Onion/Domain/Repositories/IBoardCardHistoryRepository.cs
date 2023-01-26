namespace CrmLightDemoApp.Onion.Domain.Repositories
{
    public interface IBoardCardHistoryRepository : IRepository<BoardCardHistory>
    {
        Task<List<BoardCardHistoryDetails>> GetListByCardIdAsync(int cardId);
    }
}
