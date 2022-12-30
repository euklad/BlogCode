using BlazorForms.Flows.Definitions;
using BlazorForms.Shared;
using CrmLightDemoApp.Onion.Domain;
using System.Dynamic;

namespace CrmLightDemoApp.Onion.Services.Model
{
    public class PersonCompanyLinkTypeModel : PersonCompanyLinkType
    {
        public virtual bool Changed { get; set; }
    }

    public class PersonCompanyLinkTypeListModel : IFlowModel
    {
        public virtual List<PersonCompanyLinkTypeModel>? Data { get; set; }
        public virtual List<PersonCompanyLinkTypeModel>? Deleted { get; set; } = new List<PersonCompanyLinkTypeModel>();
    }
}
