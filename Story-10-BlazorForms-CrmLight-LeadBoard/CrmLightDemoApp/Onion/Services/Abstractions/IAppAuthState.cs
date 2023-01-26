using CrmLightDemoApp.Onion.Services.Model;

namespace CrmLightDemoApp.Onion.Services.Abstractions
{
    public interface IAppAuthState
    {
        PersonModel GetCurrentUser();
    }
}
