using BlazorForms.FlowRules;
using BlazorForms.Forms;
using BlazorForms.Shared;
using CrmLightDemoApp.Onion.Domain.Repositories;
using CrmLightDemoApp.Onion.Services.Model;

namespace CrmLightDemoApp.Onion.Services.Flow.LeadBoard
{
    public class FormCardCommit : FormEditBase<LeadBoardCardModel>
    {
        protected override void Define(FormEntityTypeBuilder<LeadBoardCardModel> f)
        {
            f.DisplayName = "Congrats with another win! Click 'Save' to create client record.";
            f.Property(p => p.Title).IsReadOnly();
            f.Property(p => p.ClientCompany.StartContractDate).Label("Start contract date");

            f.Property(p => p.ClientCompany.ClientManagerId).DropdownSearch(p => p.AllPersons, m => m.Id, m => m.FullName)
                .Label("Client manager").ItemDialog(typeof(PersonDialogFlow));
            
            f.Property(p => p.ClientCompany.AlternativeClientManagerId).DropdownSearch(p => p.AllPersons, m => m.Id, m => m.FullName)
                .Label("Alternative client manager").ItemDialog(typeof(PersonDialogFlow));

            f.Property(p => p.RelatedCompanyId).DropdownSearch(p => p.AllCompanies, m => m.Id, m => m.Name)
                .Label("Client company").ItemDialog(typeof(CompanyDialogFlow)).IsRequired();

            f.Button(ButtonActionTypes.Submit, "Save");
            f.Button(ButtonActionTypes.Cancel, "Cancel");
        }
    }
 }
