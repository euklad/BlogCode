using BlazorForms.Platform.Stubs;
using BlazorForms.Platform;
using System.Diagnostics.CodeAnalysis;
using CrmLightDemoApp.Onion.Infrastructure;
using CrmLightDemoApp.Onion.Domain.Repositories;
using CrmLightDemoApp.Onion.Domain;
using CrmLightDemoApp.Onion.Services.Flow;
using CrmLightDemoApp.Onion.Services.Abstractions;
using CrmLightDemoApp.Onion.Services;

namespace CrmLightDemoApp.Onion
{
    public static class OnionServiceCollectionExtensions
    {
        public static IServiceCollection AddOnionDependencies([NotNullAttribute] this IServiceCollection serviceCollection)
        {
            serviceCollection
                // repositories
                .AddSingleton<IPersonRepository, PersonRepository>()
                .AddSingleton<ICompanyRepository, CompanyRepository>()
                .AddSingleton<IPersonCompanyRepository, PersonCompanyLinkRepository>()
                .AddSingleton<IPersonCompanyLinkTypeRepository, PersonCompanyLinkTypeRepository>()
                .AddSingleton<IRepository<PersonCompanyLinkType>, PersonCompanyLinkTypeRepository>()
                .AddSingleton<IRepository<LeadSourceType>, LeadSourceTypeRepository>()
                .AddSingleton<IBoardCardRepository, BoardCardRepository>()
                .AddSingleton<IClientCompanyRepository, ClientCompanyRepository>()
                .AddSingleton<IBoardCardHistoryRepository, BoardCardHistoryRepository>()
                // services
                .AddScoped<IBoardService, BoardService>()
                .AddScoped<IAppAuthState, MockAppAuthState>()

                //.AddSingleton<StaticTypeEditFlow<LeadSourceType>, StaticTypeEditFlow<LeadSourceType>>()
                ;
            return serviceCollection;
        }
    }
}
