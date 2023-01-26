using BlazorForms.Flows;
using BlazorForms.Flows.Engine.Fluent;
using BlazorForms.Forms;
using BlazorForms.Shared;
using BlazorForms.Shared.Extensions;
using CrmLightDemoApp.Onion.Domain.Repositories;
using CrmLightDemoApp.Onion.Services.Model;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CrmLightDemoApp.Onion.Services.Flow
{
    public class ClientCompanyListFlow : ListFlowBase<ClientCompanyListModel, FormClientList>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IClientCompanyRepository _clientCompanyRepository;

        public ClientCompanyListFlow(IPersonRepository personRepository, ICompanyRepository companyRepository, IClientCompanyRepository clientCompanyRepository) 
        {
            _personRepository = personRepository;
            _companyRepository = companyRepository;
            _clientCompanyRepository = clientCompanyRepository;
        }

        public override async Task<ClientCompanyListModel> LoadDataAsync(QueryOptions queryOptions)
        {
            using var ctx = _clientCompanyRepository.GetAllDetailsContextQuery();

            if (!string.IsNullOrWhiteSpace(queryOptions.SearchString))
            {
                ctx.Query = ctx.Query.Where(x => x.CompanyName.Contains(queryOptions.SearchString) 
                        || (x.ManagerFirstName != null && x.ManagerFirstName.Contains(queryOptions.SearchString))
                        || (x.ManagerLastName != null && x.ManagerLastName.Contains(queryOptions.SearchString))
                        || (x.AlternativeManagerFirstName != null && x.AlternativeManagerFirstName.Contains(queryOptions.SearchString))
                        || (x.AlternativeManagerLastName != null && x.AlternativeManagerLastName.Contains(queryOptions.SearchString))
                        );
            }

            // because FullName is not SQL field, we need to sort by FirstName and LastName
            if (queryOptions.AllowSort && !string.IsNullOrWhiteSpace(queryOptions.SortColumn) && queryOptions.SortDirection != SortDirection.None)
            {
                if (queryOptions.SortColumn == "ManagerFullName")
                {
                    if (queryOptions.SortDirection == SortDirection.Asc)
                    {
                        ctx.Query = ctx.Query.OrderBy(x => x.ManagerFirstName).ThenBy(x => x.ManagerLastName);
                    }
                    else
                    {
                        ctx.Query = ctx.Query.OrderByDescending(x => x.ManagerFirstName).ThenByDescending(x => x.ManagerLastName);
                    }
                }
                else if (queryOptions.SortColumn == "AlternativeManagerFullName")
                {
                    if (queryOptions.SortDirection == SortDirection.Asc)
                    {
                        ctx.Query = ctx.Query.OrderBy(x => x.AlternativeManagerFirstName).ThenBy(x => x.AlternativeManagerLastName);
                    }
                    else
                    {
                        ctx.Query = ctx.Query.OrderByDescending(x => x.AlternativeManagerFirstName).ThenByDescending(x => x.AlternativeManagerLastName);
                    }
                }
                else
                {
                    ctx.Query = ctx.Query.QueryOrderByDirection(queryOptions.SortDirection, queryOptions.SortColumn);
                }
            }

            var list = await _clientCompanyRepository.RunAllDetailsContextQueryAsync(ctx);

            var data = list.Select(x =>
            {
                var item = new ClientCompanyModel();
                x.ReflectionCopyTo(item);
                item.ManagerFullName = $"{x.ManagerFirstName} {x.ManagerLastName}";
                item.AlternativeManagerFullName = $"{x.AlternativeManagerFirstName} {x.AlternativeManagerLastName}";
                return item;
            }).ToList();

            var result = new ClientCompanyListModel { Data = data };
            return result;
        }
    }

    public class FormClientList : FormListBase<ClientCompanyListModel>
    {
        protected override void Define(FormListBuilder<ClientCompanyListModel> builder)
        {
            builder.List(p => p.Data, e =>
            {
                e.DisplayName = "Clients";

                e.Property(p => p.Id).IsPrimaryKey();
                e.Property(p => p.CompanyName);
                e.Property(p => p.ManagerFullName).Label("Manager");
                e.Property(p => p.AlternativeManagerFullName).Label("Alternative manager");

                e.ContextButton("View", "client-company-edit/{0}");
                e.NavigationButton("Add", "client-company-edit/0");
            });
        }
    }
}
