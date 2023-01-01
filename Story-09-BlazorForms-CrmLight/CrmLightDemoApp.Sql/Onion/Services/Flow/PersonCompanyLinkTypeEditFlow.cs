using BlazorForms.FlowRules;
using BlazorForms.Flows;
using BlazorForms.Flows.Engine.Fluent;
using BlazorForms.Forms;
using BlazorForms.Shared;
using BlazorForms.Shared.Extensions;
using CrmLightDemoApp.Onion.Domain;
using CrmLightDemoApp.Onion.Domain.Repositories;
using CrmLightDemoApp.Onion.Infrastructure;
using CrmLightDemoApp.Onion.Services.Model;

namespace CrmLightDemoApp.Onion.Services.Flow
{
    public class PersonCompanyLinkTypeEditFlow : FluentFlowBase<PersonCompanyLinkTypeListModel>
    {
        private readonly IPersonCompanyLinkTypeRepository _repository;

        public PersonCompanyLinkTypeEditFlow(IPersonCompanyLinkTypeRepository repository)
        {
            _repository = repository;
        }

        public override void Define()
        {
            this
                .Begin(LoadData)
                .NextForm(typeof(FormPersonCompanyLinkTypeEdit))
                .Next(SaveData)
                .NextForm(typeof(FormPersonCompanyLinkTypeSaved))
                .End();
        }

        public async Task LoadData()
        {
            var items = await _repository.GetAllAsync();
            
            Model.Data = items.Select(x =>
            {
                var item = new PersonCompanyLinkTypeModel();
                x.ReflectionCopyTo(item);
                return item;
            }).ToList();
        }

        public async Task SaveData()
        {
            foreach(var item in Model.Deleted)
            {
                if (item.Id != 0)
                {
                    await _repository.SoftDeleteAsync(item.Id);
                }
            }

            foreach (var item in Model.Data)
            {
                if (item.Id == 0)
                {
                    await _repository.CreateAsync(item);
                }
                else if (item.Changed)
                {
                    await _repository.UpdateAsync(item);
                }
            }
        }
    }

    public class FormPersonCompanyLinkTypeEdit : FormEditBase<PersonCompanyLinkTypeListModel>
    {
        protected override void Define(FormEntityTypeBuilder<PersonCompanyLinkTypeListModel> f)
        {
            f.DisplayName = "Person Company Link Type";
            f.Confirm(ConfirmType.ChangesWillBeLost, "If you leave before saving, your changes will be lost.", ConfirmButtons.OkCancel);

            f.Repeater(p => p.Data, e =>
            {
                e.DisplayName = "";

                e.Property(p => p.Id).IsReadOnly().Label("Id")
                    .Rule(typeof(PersonCompanyLinkType_ItemDeletingRule), FormRuleTriggers.ItemDeleting);

                e.Property(p => p.Name).IsRequired().IsUnique().Label("Name")
                    .Rule(typeof(PersonCompanyLinkType_ItemChangedRule), FormRuleTriggers.ItemChanged);
            });

            f.Button(ButtonActionTypes.Close);
            f.Button(ButtonActionTypes.Submit, "Save Changes");
        }
    }

    public class PersonCompanyLinkType_ItemDeletingRule : FlowRuleBase<PersonCompanyLinkTypeListModel>
    {
        public override string RuleCode => "PCLT-1";

        public override void Execute(PersonCompanyLinkTypeListModel model)
        {
            // preserve all deleted items
            model.Deleted.Add(model.Data[RunParams.RowIndex]);
        }
    }

    public class PersonCompanyLinkType_ItemChangedRule : FlowRuleBase<PersonCompanyLinkTypeListModel>
    {
        public override string RuleCode => "PCLT-2";

        public override void Execute(PersonCompanyLinkTypeListModel model)
        {
            model.Data[RunParams.RowIndex].Changed = true;
        }
    }

    public class FormPersonCompanyLinkTypeSaved : FormEditBase<PersonCompanyLinkTypeListModel>
    {
        protected override void Define(FormEntityTypeBuilder<PersonCompanyLinkTypeListModel> f)
        {
            f.DisplayName = "Person Company Link Type changes saved successfully";
            f.Button(ButtonActionTypes.Close);
        }
    }
}
