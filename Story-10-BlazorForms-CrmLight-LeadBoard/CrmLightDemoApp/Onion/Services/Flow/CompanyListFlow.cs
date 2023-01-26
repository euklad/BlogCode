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
    public class CompanyListFlow : ListFlowBase<CompanyListModel, FormCompanyList>
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyListFlow(ICompanyRepository companyRepository) 
        {
            _companyRepository = companyRepository;
        }

        public override async Task<CompanyListModel> LoadDataAsync(QueryOptions queryOptions)
        {
            using var ctx = _companyRepository.GetContextQuery();

            if (!string.IsNullOrWhiteSpace(queryOptions.SearchString))
            {
                ctx.Query = ctx.Query.Where(x => x.Name.Contains(queryOptions.SearchString, StringComparison.OrdinalIgnoreCase) 
                        || (x.RegistrationNumber != null && x.RegistrationNumber.Contains(queryOptions.SearchString, StringComparison.OrdinalIgnoreCase)) );
            }

            if (queryOptions.AllowSort && !string.IsNullOrWhiteSpace(queryOptions.SortColumn) && queryOptions.SortDirection != SortDirection.None)
            {
                ctx.Query = ctx.Query.QueryOrderByDirection(queryOptions.SortDirection, queryOptions.SortColumn);
            }
                
            var list = (await _companyRepository.RunContextQueryAsync(ctx)).Select(x =>
            {
                var item = new CompanyModel();
                x.ReflectionCopyTo(item);
                return item;
            }).ToList();

            var result = new CompanyListModel { Data = list };
            return result;
        }
    }

    public class FormCompanyList : FormListBase<CompanyListModel>
    {
        protected override void Define(FormListBuilder<CompanyListModel> builder)
        {
            builder.List(p => p.Data, e =>
            {
                e.DisplayName = "Companies";

                e.Property(p => p.Id).IsPrimaryKey();
                e.Property(p => p.Name);
                e.Property(p => p.RegistrationNumber).Label("Reg. No.");
                e.Property(p => p.EstablishedDate).Label("Established date").Format("dd/MM/yyyy");

                e.ContextButton("View", "company-edit/{0}");
                e.NavigationButton("Add", "company-edit/0");
            });
        }
    }
}
