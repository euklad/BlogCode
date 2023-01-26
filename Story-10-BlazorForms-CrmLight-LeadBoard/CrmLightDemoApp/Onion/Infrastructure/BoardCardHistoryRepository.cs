using BlazorForms.Flows;
using BlazorForms.Shared;
using CrmLightDemoApp.Onion.Domain;
using CrmLightDemoApp.Onion.Domain.Repositories;
using System.ComponentModel.Design;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CrmLightDemoApp.Onion.Infrastructure
{
    public class BoardCardHistoryRepository : LocalCacheRepository<BoardCardHistory>, IBoardCardHistoryRepository
    {
        private readonly IPersonRepository _personRepository;

        public BoardCardHistoryRepository(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
            
            // pre fill some data
             _localCache.Add(new BoardCardHistory { Id = 1, BoardCardId = 1, Title = "Title", Text = "Amazing opportunity",
                PersonId = 3, Date = new DateTime(2023, 1, 12) });

             _localCache.Add(new BoardCardHistory { Id = 2, BoardCardId = 1, Title = "Title", Text = "Lot's of interest from linkedin publications",
                PersonId = 11, Date = new DateTime(2023, 1, 13) });

             _localCache.Add(new BoardCardHistory { Id = 3, BoardCardId = 1, Title = "Title", Text = "Got a call from Haley but missed it",
                PersonId = 3, Date = new DateTime(2023, 1, 14) });

            _id = 10;
        }

        public async Task<List<BoardCardHistoryDetails>> GetListByCardIdAsync(int cardId)
        {
            var list = _localCache.Where(x => !x.Deleted && x.BoardCardId == cardId).Select(x =>
            {
                var item = new BoardCardHistoryDetails();
                x.ReflectionCopyTo(item);
                return item;
            }).OrderByDescending(x => x.Date).ToList();

            var personIds = list.Select(x => x.PersonId).Distinct().ToList();
            var persons = (await _personRepository.GetListByIdsAsync(personIds)).ToDictionary(x => x.Id, x => x);

            foreach (var item in list)
            {
                item.PersonFullName = $"{persons[item.PersonId].FirstName} {persons[item.PersonId].LastName}";
            }

            return list;
        }
    }
}
