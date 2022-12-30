using BlazorForms.Flows;
using BlazorForms.Shared;
using CrmLightDemoApp.Onion.Domain;
using CrmLightDemoApp.Onion.Domain.Repositories;

namespace CrmLightDemoApp.Onion.Infrastructure
{
    public class PersonRepository : LocalCacheRepository<Person>, IPersonRepository
    {
        public PersonRepository()
        {
            // pre fill some data
            _localCache.Add(new Person { Id = 1, FirstName = "Jack", LastName = "Wombat", BirthDate = new DateTime(1998, 10, 21) });
            _localCache.Add(new Person { Id = 2, FirstName = "David", LastName = "Jones", BirthDate = new DateTime(1978, 12, 1) });
            _localCache.Add(new Person { Id = 3, FirstName = "Louis", LastName = "Monero", BirthDate = new DateTime(2001, 3, 16) });
            _localCache.Add(new Person { Id = 4, FirstName = "Peter", LastName = "Lalas",  });
            _localCache.Add(new Person { Id = 5, FirstName = "Zabina", LastName = "Willis", BirthDate = new DateTime(2001, 4, 16) });
            _localCache.Add(new Person { Id = 6, FirstName = "Jim", LastName = "Tacker",  });
            _localCache.Add(new Person { Id = 7, FirstName = "Paul", LastName = "Cone", BirthDate = new DateTime(1998, 5, 26) });
            _localCache.Add(new Person { Id = 8, FirstName = "Harris", LastName = "Lancia", BirthDate = new DateTime(1977, 6, 15) });
            _localCache.Add(new Person { Id = 9, FirstName = "Haily", LastName = "Roberts",  });
            _localCache.Add(new Person { Id = 10, FirstName = "Vlad", LastName = "Ivanov",  });
            _localCache.Add(new Person { Id = 11, FirstName = "Nursula", LastName = "Benedict", BirthDate = new DateTime(2011, 5, 16) });
            _localCache.Add(new Person { Id = 12, FirstName = "Eugenio", LastName = "Tatalia", BirthDate = new DateTime(2002, 4, 11) });
            _localCache.Add(new Person { Id = 13, FirstName = "Fernando", LastName = "Lopes", BirthDate = new DateTime(1973, 11, 6) });
            _id = 15;
        }
    }
}
