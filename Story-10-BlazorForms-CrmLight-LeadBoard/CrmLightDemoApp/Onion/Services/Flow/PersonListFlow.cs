using BlazorForms.Flows;
using BlazorForms.Flows.Engine.Fluent;
using BlazorForms.Forms;
using BlazorForms.Shared;
using BlazorForms.Shared.Extensions;
using CrmLightDemoApp.Onion.Domain.Repositories;
using CrmLightDemoApp.Onion.Services.Model;

namespace CrmLightDemoApp.Onion.Services.Flow
{
    public class PersonListFlow : ListFlowBase<PersonListModel, FormPersonList>
    {
        private readonly IPersonRepository _personRepository;

        public PersonListFlow(IPersonRepository personRepository) 
        {
            _personRepository = personRepository;
        }

        public override async Task<PersonListModel> LoadDataAsync(QueryOptions queryOptions)
        {
            using var ctx = _personRepository.GetContextQuery();

            if (!string.IsNullOrWhiteSpace(queryOptions.SearchString))
            {
                ctx.Query = ctx.Query.Where(x => x.FirstName.Contains(queryOptions.SearchString, StringComparison.OrdinalIgnoreCase)
                        || x.LastName.Contains(queryOptions.SearchString, StringComparison.OrdinalIgnoreCase)
                        || (x.Phone != null && x.Phone.Contains(queryOptions.SearchString, StringComparison.OrdinalIgnoreCase))
                        || (x.Email != null && x.Email.Contains(queryOptions.SearchString, StringComparison.OrdinalIgnoreCase)) );
            }

            if (queryOptions.AllowSort && !string.IsNullOrWhiteSpace(queryOptions.SortColumn) && queryOptions.SortDirection != SortDirection.None)
            {
                ctx.Query = ctx.Query.QueryOrderByDirection(queryOptions.SortDirection, queryOptions.SortColumn);
            }

            var list = await _personRepository.RunContextQueryAsync(ctx);
            var result = new PersonListModel { Data = list };
            return result;
        }
    }

    public class FormPersonList : FormListBase<PersonListModel>
    {
        protected override void Define(FormListBuilder<PersonListModel> builder)
        {
            builder.List(p => p.Data, e =>
            {
                e.DisplayName = "People";

                e.Property(p => p.Id).IsPrimaryKey();
                e.Property(p => p.FirstName).Label("First Name").Filter(FieldFilterType.TextStarts);
                e.Property(p => p.LastName).Label("Last Name").Filter(FieldFilterType.TextStarts);
                e.Property(p => p.BirthDate).Label("Date of birth").Format("dd/MM/yyyy");
                e.Property(p => p.Phone);
                e.Property(p => p.Email);

                e.ContextButton("View", "person-edit/{0}");
                e.NavigationButton("Add", "person-edit/0");
            });
        }
    }
}
