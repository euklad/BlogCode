using BlazorForms.Flows.Definitions;
using BlazorForms.Shared;
using CrmLightDemoApp.Onion.Domain;
using System.Dynamic;

namespace CrmLightDemoApp.Onion.Services.Model
{
    public class CompanyModel : Company, IFlowModel
    {
        public virtual List<PersonCompanyLinkDetailsModel> PersonCompanyLinks { get; set; } = new List<PersonCompanyLinkDetailsModel>();
        public virtual List<PersonCompanyLinkDetailsModel> PersonCompanyLinksDeleted { get; set; } = new List<PersonCompanyLinkDetailsModel>();
        public virtual List<PersonCompanyLinkType> AllLinkTypes { get; set; }
        // public virtual Dictionary<string, PersonModel> PersonDictionary { get; set; }
        public virtual List<PersonModel> AllPersons { get; set; }
    }
}
