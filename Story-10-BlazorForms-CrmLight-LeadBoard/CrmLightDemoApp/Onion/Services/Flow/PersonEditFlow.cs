using BlazorForms.Flows;
using BlazorForms.Forms;
using BlazorForms.Shared;
using CrmLightDemoApp.Onion.Domain.Repositories;
using CrmLightDemoApp.Onion.Infrastructure;
using CrmLightDemoApp.Onion.Services.Model;

namespace CrmLightDemoApp.Onion.Services.Flow
{
    public class PersonEditFlow : FluentFlowBase<PersonModel>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IPersonCompanyRepository _personCompanyRepository;

        public PersonEditFlow(IPersonRepository personRepository, IPersonCompanyRepository personCompanyRepository)
        {
            _personRepository = personRepository;
            _personCompanyRepository = personCompanyRepository;
        }

        public override void Define()
        {
            this
                .Begin()
                .If(() => _flowContext.Params.ItemKeyAboveZero)
                   .Next(LoadData)
                   .NextForm(typeof(FormPersonView))
                .EndIf()
                .If(() => _flowContext.ExecutionResult.FormLastAction == ModelBinding.DeleteButtonBinding)
                    .Next(DeleteData)
                .Else()
                    .If(() => _flowContext.ExecutionResult.FormLastAction == ModelBinding.SubmitButtonBinding || !_flowContext.Params.ItemKeyAboveZero)
                        .NextForm(typeof(FormPersonEdit))
                        .Next(SaveData)
                    .EndIf()
                .EndIf()
                .End();
        }

        public async Task LoadData()
        {
            if (_flowContext.Params.ItemKeyAboveZero)
            {
                var item = await _personRepository.GetByIdAsync(_flowContext.Params.ItemKey);
                // item and Model have different types - we use reflection to copy similar properties
                item.ReflectionCopyTo(Model);
                Model.CompanyLinks = await _personCompanyRepository.GetByPersonIdAsync(Model.Id);
            }
        }

        public async Task DeleteData()
        {
            await _personRepository.SoftDeleteAsync(Model);
        }

        public async Task SaveData()
        {
            if (_flowContext.Params.ItemKeyAboveZero)
            {
                await _personRepository.UpdateAsync(Model);
            }
            else
            {
                await _personRepository.CreateAsync(Model);
            }
        }
    }

    public class FormPersonView : FormEditBase<PersonModel>
    {
        protected override void Define(FormEntityTypeBuilder<PersonModel> f)
        {
            f.DisplayName = "Person View";

            f.Property(p => p.FirstName).Label("First name").IsReadOnly();
            f.Property(p => p.LastName).Label("Last name").IsReadOnly();
            f.Property(p => p.BirthDate).Label("Date of birth").IsReadOnly();
            f.Property(p => p.Phone).IsReadOnly();
            f.Property(p => p.Email).IsReadOnly();

            f.Button(ButtonActionTypes.Submit, "Edit");

            f.Button(ButtonActionTypes.Delete, "Delete")
                .Confirm(ConfirmType.Delete, "Delete this Person?", ConfirmButtons.YesNo);

            f.Button(ButtonActionTypes.Close, "Close");
        }
    }

    public class FormPersonEdit : FormEditBase<PersonModel>
    {
        protected override void Define(FormEntityTypeBuilder<PersonModel> f)
        {
            f.DisplayName = "Person Edit";
            f.Confirm(ConfirmType.ChangesWillBeLost, "If you leave before saving, your changes will be lost.", ConfirmButtons.OkCancel);

            f.Property(p => p.FirstName).Label("First name").IsRequired();
            f.Property(p => p.LastName).Label("Last name").IsRequired();
            f.Property(p => p.BirthDate).Label("Date of birth");
            f.Property(p => p.Phone);
            f.Property(p => p.Email);

            f.Button(ButtonActionTypes.Cancel, "Cancel");
            f.Button(ButtonActionTypes.Submit, "Save");
        }
    }
}
