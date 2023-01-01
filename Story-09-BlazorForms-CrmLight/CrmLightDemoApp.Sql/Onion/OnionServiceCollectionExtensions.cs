using BlazorForms.Platform.Stubs;
using BlazorForms.Platform;
using System.Diagnostics.CodeAnalysis;
using CrmLightDemoApp.Onion.Infrastructure;
using CrmLightDemoApp.Onion.Domain.Repositories;

namespace CrmLightDemoApp.Onion
{
    public static class OnionServiceCollectionExtensions
    {
        public static IServiceCollection AddOnionDependencies([NotNullAttribute] this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSingleton<IPersonRepository, PersonRepository>()
                .AddSingleton<ICompanyRepository, CompanyRepository>()
                .AddSingleton<IPersonCompanyRepository, PersonCompanyRepository>()
                .AddSingleton<IPersonCompanyLinkTypeRepository, PersonCompanyLinkTypeRepository>()
                ;
            return serviceCollection;
        }
    }
}
