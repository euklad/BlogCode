using BlazorForms.FlowRules;
using BlazorForms.Flows;
using BlazorForms.Forms;
using BlazorForms.Shared;
using CrmLightDemoApp.Onion.Domain.Repositories;
using CrmLightDemoApp.Onion.Infrastructure;
using CrmLightDemoApp.Onion.Services.Model;

namespace CrmLightDemoApp.Onion.Services.Flow
{
    public class CompanyEditFlow : FluentFlowBase<CompanyModel>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IPersonCompanyRepository _personCompanyRepository;
        private readonly IPersonCompanyLinkTypeRepository _personCompanyLinkTypeRepository;
        private readonly IPersonRepository _personRepository;

        public CompanyEditFlow(ICompanyRepository companyRepository, IPersonCompanyRepository personCompanyRepository,
            IPersonCompanyLinkTypeRepository personCompanyLinkTypeRepository, IPersonRepository personRepository)
        {
            _companyRepository = companyRepository;
            _personCompanyRepository = personCompanyRepository;
            _personCompanyLinkTypeRepository = personCompanyLinkTypeRepository; 
            _personRepository = personRepository;
        }

        public override void Define()
        {
            this
                .Begin()
                .If(() => _flowContext.Params.ItemKeyAboveZero)
                   .Next(LoadData)
                   .NextForm(typeof(FormCompanyView))
                .EndIf()
                .If(() => _flowContext.ExecutionResult.FormLastAction == ModelBinding.DeleteButtonBinding)
                    .Next(DeleteData)
                .Else()
                    .If(() => _flowContext.ExecutionResult.FormLastAction == ModelBinding.SubmitButtonBinding || !_flowContext.Params.ItemKeyAboveZero)
                        .Next(LoadRelatedData)
                        .NextForm(typeof(FormCompanyEdit))
                        .Next(SaveData)
                    .EndIf()
                .EndIf()
                .End();
        }

        public async Task LoadData()
        {
            if (_flowContext.Params.ItemKeyAboveZero)
            {
                var item = await _companyRepository.GetByIdAsync(_flowContext.Params.ItemKey);
                // item and Model have different types - we use reflection to copy similar properties
                item.ReflectionCopyTo(Model);

                Model.PersonCompanyLinks = (await _personCompanyRepository.GetByCompanyIdAsync(Model.Id))
                    .Select(x =>
                    {
                        var item = new PersonCompanyLinkDetailsModel();
                        x.ReflectionCopyTo(item);
                        return item;
                    }).ToList();
            }
        }

        public async Task DeleteData()
        {
            await _companyRepository.SoftDeleteAsync(Model);
        }

        public async Task LoadRelatedData()
        {
            Model.AllLinkTypes = await _personCompanyLinkTypeRepository.GetAllAsync();
            Model.AllLinkTypes.Insert(0, new Domain.PersonCompanyLinkType { Id = 0, Name = "" });

            var persons = (await _personRepository.GetAllAsync())
                .Select(x => 
                {
                    var item = new PersonModel(); 
                    x.ReflectionCopyTo(item);
                    item.FullName = $"{x.FirstName} {x.LastName}";
                    return item;
                }).OrderBy(x => x.FullName).ToList();

            Model.AllPersons = persons;
            //Model.PersonDictionary = persons.ToDictionary(x => x.FullName, x => x);

            // ToDo: remove it after move to Autocomplete control
            //Model.AllPersons.Insert(0, new PersonModel { Id = 0, FullName = "" });
        }

        public async Task SaveData()
        {
            if (_flowContext.Params.ItemKeyAboveZero)
            {
                await _companyRepository.UpdateAsync(Model);
            }
            else
            {
                Model.Id = await _companyRepository.CreateAsync(Model);
            }

            foreach (var item in Model.PersonCompanyLinksDeleted)
            {
                if (item.Id != 0)
                {
                    await _personCompanyRepository.SoftDeleteAsync(item);
                }
            }

            foreach (var item in Model.PersonCompanyLinks)
            {
                // we use autocomplete control, so need to resolve id by name
                //item.PersonId = Model.PersonDictionary[item.PersonFullName].Id;
                //item.PersonId = Model.AllPersons.First(p => p.FullName == item.PersonFullName).Id;

                if (item.Id == 0)
                {
                    item.CompanyId = Model.Id;
                    await _personCompanyRepository.CreateAsync(item);
                }
                else if (item.Changed)
                {
                    await _personCompanyRepository.UpdateAsync(item);
                }
            }
        }
    }

    public class FormCompanyView : FormEditBase<CompanyModel>
    {
        protected override void Define(FormEntityTypeBuilder<CompanyModel> f)
        {
            f.DisplayName = "Company View";

            f.Property(p => p.Name).Label("Name").IsReadOnly();
            f.Property(p => p.RegistrationNumber).Label("Reg. No.").IsReadOnly();
            f.Property(p => p.EstablishedDate).Label("Established date").IsReadOnly();

            f.Table(p => p.PersonCompanyLinks, e => 
            {
                e.DisplayName = "Associations";
                e.Property(p => p.LinkTypeName).Label("Type");
                e.Property(p => p.PersonFullName).Label("Person");
            });

            f.Button(ButtonActionTypes.Submit, "Edit");

            f.Button(ButtonActionTypes.Delete, "Delete")
                .Confirm(ConfirmType.Delete, "Delete this Company?", ConfirmButtons.YesNo);

            f.Button(ButtonActionTypes.Close, "Close");

        }
    }

    public class FormCompanyEdit : FormEditBase<CompanyModel>
    {
        protected override void Define(FormEntityTypeBuilder<CompanyModel> f)
        {

            f.DisplayName = "Company Edit";
            f.Confirm(ConfirmType.ChangesWillBeLost, "If you leave before saving, your changes will be lost.", ConfirmButtons.OkCancel);

            f.Property(p => p.Name).Label("Name").IsRequired();
            f.Property(p => p.RegistrationNumber).Label("Reg. No.");
            f.Property(p => p.EstablishedDate).Label("Established date");

            f.Repeater(p => p.PersonCompanyLinks, e =>
            {
                e.DisplayName = "Associations";
                e.Property(p => p.Id).IsReadOnly().Rule(typeof(FormCompanyEdit_ItemDeletingRule), FormRuleTriggers.ItemDeleting);
                
                e.PropertyRoot(p => p.LinkTypeId).Dropdown(p => p.AllLinkTypes, m => m.Id, m => m.Name).IsRequired().Label("Type")
                    .Rule(typeof(FormCompanyEdit_ItemChangedRule), FormRuleTriggers.ItemChanged);

                e.PropertyRoot(p => p.PersonId).DropdownSearch(e => e.AllPersons, m => m.Id, m => m.FullName).IsRequired().Label("Person")
                    .Rule(typeof(FormCompanyEdit_ItemChangedRule), FormRuleTriggers.ItemChanged);

                //e.PropertyRoot(p => p.PersonFullName).EditWithOptions(e => e.AllPersons, m => m.FullName).IsRequired().Label("Person")
                //    .Rule(typeof(FormCompanyEdit_ItemChangedRule), FormRuleTriggers.ItemChanged);
                //.Rule(typeof(FormCompanyEdit_CheckNameRule), FormRuleTriggers.Submit);

                //e.PropertyRoot(p => p.PersonId).Dropdown(p => p.AllPersons, m => m.Id, m => m.FullName).IsRequired().Label("Person")
                //    .Rule(typeof(FormCompanyEdit_ItemChangedRule), FormRuleTriggers.ItemChanged);
            }).Confirm(ConfirmType.DeleteItem, "Delete this association?", ConfirmButtons.YesNo);

            f.Button(ButtonActionTypes.Submit, "Save");
            f.Button(ButtonActionTypes.Cancel, "Cancel");
        }
    }

    public class FormCompanyEdit_ItemDeletingRule : FlowRuleBase<CompanyModel>
    {
        public override string RuleCode => "CMP-1";

        public override void Execute(CompanyModel model)
        {
            // preserve all deleted items
            model.PersonCompanyLinksDeleted.Add(model.PersonCompanyLinks[RunParams.RowIndex]);
        }
    }

    public class FormCompanyEdit_ItemChangedRule : FlowRuleBase<CompanyModel>
    {
        public override string RuleCode => "CMP-2";

        public override void Execute(CompanyModel model)
        {
            model.PersonCompanyLinks[RunParams.RowIndex].Changed = true;

            // Example: How to trigger cascading rule in FastRuleEngine
            Trigger(RowField(m => m.PersonCompanyLinks, m => m.Changed, RunParams.RowIndex));

            // Incorrect example: Don't use SingleField for collections
            Trigger(SingleField(m => m.PersonCompanyLinks[RunParams.RowIndex].Changed));
        }
    }
    public class FormCompanyEdit_CheckNameRule : FlowRuleBase<CompanyModel>
    {
        public override string RuleCode => "CMP-3";

        public override void Execute(CompanyModel model)
        {
            var name = model.PersonCompanyLinks[RunParams.RowIndex].PersonFullName;

            //if (!model.PersonDictionary.Keys.Contains(name))
            if (!model.AllPersons.Any(p => p.FullName == name))
            {
                Result.ValidationResult = RuleValidationResult.Error;
                Result.ValidationMessage = $"Name '{name}' not found";
            }
        }
    }
}
