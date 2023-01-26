using CrmLightDemoApp.Onion.Services.Abstractions;
using CrmLightDemoApp.Onion.Services.Model;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;

namespace CrmLightDemoApp.Onion.Services
{
    public class MockAppAuthState : IAppAuthState
    {
        private PersonModel _currentUser;

        public MockAppAuthState() 
        {
            _currentUser = new PersonModel { Id = 3, FirstName = "Louis", LastName = "Monero", FullName = "Louis Monero", 
                BirthDate = new DateTime(2001, 3, 16) };
        }

        public PersonModel GetCurrentUser()
        {
            return _currentUser;
        }
    }
}
