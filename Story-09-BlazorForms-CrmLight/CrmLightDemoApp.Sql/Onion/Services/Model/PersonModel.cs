using BlazorForms.Flows.Definitions;
using BlazorForms.Shared;
using CrmLightDemoApp.Onion.Domain;
using System.Dynamic;

namespace CrmLightDemoApp.Onion.Services.Model
{
    public class PersonModel : Person, IFlowModel
    {
        public virtual string? FullName { get; set; }
        public virtual List<PersonCompanyLinkDetails> CompanyLinks { get; set; }
    }
}
