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
            f.Rule(typeof(LoadIsNewClientCompanyRule), FormRuleTriggers.Loaded);
            f.Rule(typeof(ClientCompanyExistsRule), FormRuleTriggers.Loaded);
            f.Property(p => p.Title).IsReadOnly();

            f.Property(p => p.ClientCompany.StartContractDate).Label("Start contract date");

            f.Property(p => p.ClientCompany.ClientManagerId).DropdownSearch(p => p.AllPersons, m => m.Id, m => m.FullName)
                .Label("Client manager").ItemDialog(typeof(PersonDialogFlow));
            
            f.Property(p => p.ClientCompany.AlternativeClientManagerId).DropdownSearch(p => p.AllPersons, m => m.Id, m => m.FullName)
                .Label("Alternative client manager").ItemDialog(typeof(PersonDialogFlow));

            f.Property(p => p.IsNewCompany).Label("Create new company")
                .Rule(typeof(NewClientCompanyRule))
                .Rule(typeof(NewClientCompanyRule), FormRuleTriggers.Loaded);

            f.Property(p => p.RelatedCompanyId).DropdownSearch(p => p.AllCompanies, m => m.Id, m => m.Name)
                .Label("Existing company").ItemDialog(typeof(CompanyDialogFlow));

            f.Property(p => p.Company.Name).Label("Company name").IsRequired();
            f.Property(p => p.Company.RegistrationNumber).Label("Reg. No.");
            f.Property(p => p.Company.EstablishedDate).Label("Established date");

            f.Button(ButtonActionTypes.Submit, "Save");
            f.Button(ButtonActionTypes.Cancel, "Cancel");
        }
    }

    public class LoadIsNewClientCompanyRule : FlowRuleBase<LeadBoardCardModel>
    {
        public override string RuleCode => "BRD-1";

        public override void Execute(LeadBoardCardModel model)
        {
            model.ClientCompany.StartContractDate = DateTime.UtcNow;
            model.IsNewCompany = model.RelatedCompanyId == null;
        }
    }

    public class ClientCompanyExistsRule : FlowRuleAsyncBase<LeadBoardCardModel>
    {
        private readonly IClientCompanyRepository _clientCompanyRepository;
        public override string RuleCode => "BRD-2";

        public ClientCompanyExistsRule(IClientCompanyRepository clientCompanyRepository)
        {
            _clientCompanyRepository = clientCompanyRepository;
        }

        public override async Task Execute(LeadBoardCardModel model)
        {
            var cc = await _clientCompanyRepository.FindByCompanyIdAsync(model.RelatedCompanyId ?? 0);

            if (cc != null)
            {
                // disable change fields
                model.IsNewCompany = false;
                Result.Fields[SingleField(m => m.IsNewCompany)].Disabled = true;
                Result.Fields[SingleField(m => m.RelatedCompanyId)].Disabled = true;

                // load Client Company details
                cc.ReflectionCopyTo(model.ClientCompany);
            }
        }
    }

    public class NewClientCompanyRule : FlowRuleBase<LeadBoardCardModel>
    {
        public override string RuleCode => "BRD-3";

        public override void Execute(LeadBoardCardModel model)
        {
            Result.Fields[SingleField(m => m.RelatedCompanyId)].Visible = !model.IsNewCompany;
            Result.Fields[SingleField(m => m.RelatedCompanyId)].Required = !model.IsNewCompany;

            Result.Fields[SingleField(m => m.Company.Name)].Visible = model.IsNewCompany;
            Result.Fields[SingleField(m => m.Company.RegistrationNumber)].Visible = model.IsNewCompany;
            Result.Fields[SingleField(m => m.Company.EstablishedDate)].Visible = model.IsNewCompany;
        }
    }
}
