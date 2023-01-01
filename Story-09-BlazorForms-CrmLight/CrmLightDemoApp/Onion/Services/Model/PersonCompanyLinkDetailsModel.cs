using BlazorForms.Flows.Definitions;
using CrmLightDemoApp.Onion.Domain;

namespace CrmLightDemoApp.Onion.Services.Model
{
    public class PersonCompanyLinkDetailsModel : PersonCompanyLinkDetails
    {
        public virtual bool Changed { get; set; }
    }
}
