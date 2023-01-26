using BlazorForms.Flows.Definitions;
using CrmLightDemoApp.Onion.Domain;

namespace CrmLightDemoApp.Onion.Services.Model
{
    public class ClientCompanyListModel : IFlowModel
    {
        public virtual List<ClientCompanyModel>? Data { get; set; }
    }
}
