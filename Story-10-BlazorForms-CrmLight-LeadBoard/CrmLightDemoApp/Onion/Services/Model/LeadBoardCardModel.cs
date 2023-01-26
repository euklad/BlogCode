using BlazorForms.Flows.Definitions;
using BlazorForms.Rendering.Model;
using CrmLightDemoApp.Onion.Domain;

namespace CrmLightDemoApp.Onion.Services.Model
{
public class LeadBoardCardModel : BoardCard, IFlowBoardCard
{
    public virtual string? Comments { get; set; }

    // for dropdowns
    public virtual List<PersonModel> AllPersons { get; set; } = new();
    public virtual List<CompanyModel> AllCompanies { get; set; } = new();
    public virtual List<LeadSourceType> AllLeadSources { get; set; } = new();

    // for ClientCompany
    public virtual ClientCompany ClientCompany { get; set; } = new();

    public virtual List<CardHistoryModel>? CardHistory { get; set; } = new();

    // properties
    public string SalesPersonFullName
    {
        get
        {
            var sp = AllPersons.FirstOrDefault(p => p.Id == SalesPersonId);

            if (sp != null)
            {
                return sp.FullName;
            }

            return null;
        }
    }
}

    public class CardHistoryModel : BoardCardHistoryDetails, IFlowModel
    {
        public virtual string AvatarMarkup { get { return null; } }
        
        public virtual string TitleMarkup 
        { 
            get 
            {
                var suff = EditedDate != null ? $"<br/><em>edited on {EditedDate.Value.ToString("dd/MM/yyyy HH:mm")}</em>" : null;
                return $"<b>{PersonFullName}</b> on {Date.ToString("dd/MM/yyyy HH:mm")}{suff}"; 
            } 
        }
    }
}
