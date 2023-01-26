using BlazorForms.Flows;
using BlazorForms.Shared;
using CrmLightDemoApp.Onion.Domain;
using CrmLightDemoApp.Onion.Domain.Repositories;

namespace CrmLightDemoApp.Onion.Infrastructure
{
    public class BoardCardRepository : LocalCacheRepository<BoardCard>, IBoardCardRepository
    {
        public BoardCardRepository()
        {
            // pre fill some data
            _localCache.Add(new BoardCard { Id = 1, Title = "Pro Coders", State = "Lead", LeadSourceTypeId = 1, Description = "Looking for SF dev",
                SalesPersonId = 3, ContactDetails = "https://www.linkedin.com/in/ev-uklad/", Order = 0 });

            _localCache.Add(new BoardCard { Id = 2, Title = "Incognita Futura", State = "Lead", LeadSourceTypeId = 2, 
                Description = "Senior BA + Solution Architect", RelatedPersonId =7,
                SalesPersonId = 13, ContactDetails = "man@daddy.au",
                Order = 1
            });

            _localCache.Add(new BoardCard { Id = 3, Title = "Maxiwise", State = "Contacted", LeadSourceTypeId = 2, 
                Description = "After the offshore team augmentation", RelatedPersonId = 9,
                SalesPersonId = 13, ContactDetails = "333-555-77",
                Order = 2
            });

            _localCache.Add(new BoardCard { Id = 4, Title = "Perpetuum Automotives Inc", State = "MeetingScheduled", LeadSourceTypeId = 4, 
                Description = "Interested in hybrid global team", RelatedCompanyId = 4, SalesPersonId = 4,
                Order = 3
            });

            _localCache.Add(new BoardCard { Id = 5, Title = "Vectra", State = "ProposalDelivered", LeadSourceTypeId = 3, 
                Description = "Australian based start up looking for free or bench resources", RelatedPersonId = 10, SalesPersonId = 4,
                Order = 4
            });
            
            _id = 10;
        }
    }
}
