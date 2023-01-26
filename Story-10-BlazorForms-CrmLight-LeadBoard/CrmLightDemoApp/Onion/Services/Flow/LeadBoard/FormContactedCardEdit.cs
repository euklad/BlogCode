using BlazorForms.Forms;
using CrmLightDemoApp.Onion.Services.Model;

namespace CrmLightDemoApp.Onion.Services.Flow.LeadBoard
{
    public class FormContactedCardEdit : FormEditBase<LeadBoardCardModel>
    {
        protected override void Define(FormEntityTypeBuilder<LeadBoardCardModel> f)
        {
            f.DisplayName = "Lead Contacted Card";

            f.Property(p => p.RelatedCompanyId).DropdownSearch(p => p.AllCompanies, m => m.Id, m => m.Name).Label("Lead company")
                .ItemDialog(typeof(CompanyDialogFlow));

            f.Property(p => p.RelatedPersonId).DropdownSearch(p => p.AllPersons, m => m.Id, m => m.FullName).Label("Lead contact")
                .ItemDialog(typeof(PersonDialogFlow)).IsRequired();

            f.Property(p => p.Phone);
            f.Property(p => p.Email);
            f.Property(p => p.ContactDetails).Label("Other contact info");

            //f.Property(p => p.FollowUpDate).Label("Follow up date");
            //f.Property(p => p.FollowUpDetails).Label("Follow up details");
            //FormLeadCardEdit.MainSection(f);

            f.Button(ButtonActionTypes.Submit, "Save");
            f.Button(ButtonActionTypes.Cancel, "Cancel");
        }
    }
}
