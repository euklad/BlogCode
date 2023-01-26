using BlazorForms.FlowRules;
using BlazorForms.Flows;
using BlazorForms.Flows.Definitions;
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
    public class StaticTypeEditFlow<T> : FluentFlowBase<StaticDataListModel<T>>
        where T : class, IEntity, new()
    {
        private readonly IRepository<T> _repository;

        public StaticTypeEditFlow(IRepository<T> repository)
        {
            _repository = repository;
        }

        public override void Define()
        {
            this
                .Begin(LoadData)
                .NextForm(typeof(FormStaticTypeEdit<T>))
                .Next(SaveData)
                .NextForm(typeof(FormStaticTypeSaved<T>))
                .End();
        }

        public async Task LoadData()
        {
            var items = await _repository.GetAllAsync();
            
            Model.Data = items.Select(x =>
            {
                var item = new StaticDataModel<T>();
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
                var entity = new T();
				item.ReflectionCopyTo(entity);
               
                if (item.Id == 0)
                {
					await _repository.CreateAsync(entity);
                }
                else if (item.Changed)
                {
                    await _repository.UpdateAsync(entity);
                }
            }
        }
    }

    public class FormStaticTypeEdit<T> : FormEditBase<StaticDataListModel<T>>
		where T : class, IEntity, new()
	{
        protected override void Define(FormEntityTypeBuilder<StaticDataListModel<T>> f)
        {
            f.DisplayName = $"Edit Static Type '{typeof(T).Name}'";
            f.Confirm(ConfirmType.ChangesWillBeLost, "If you leave before saving, your changes will be lost.", ConfirmButtons.OkCancel);

            f.Repeater(p => p.Data, e =>
            {
                e.DisplayName = "";

                e.Property(p => p.Id).IsReadOnly().Label("Id")
                    .Rule(typeof(FormStaticType_ItemDeletingRule<T>), FormRuleTriggers.ItemDeleting);

                e.Property(p => p.Name).IsRequired().IsUnique().Label("Name")
                    .Rule(typeof(FormStaticType_ItemChangedRule<T>), FormRuleTriggers.ItemChanged);
            });

            f.Button(ButtonActionTypes.Submit, "Save Changes");
            f.Button(ButtonActionTypes.Close);
        }
    }

    public class FormStaticType_ItemDeletingRule<T> : FlowRuleBase<StaticDataListModel<T>>
		where T : class, IEntity, new()
	{
        public override string RuleCode => GetType().FullName;

        public override void Execute(StaticDataListModel<T> model)
        {
            // preserve all deleted items
            model.Deleted.Add(model.Data[RunParams.RowIndex]);
        }
    }

    public class FormStaticType_ItemChangedRule<T> : FlowRuleBase<StaticDataListModel<T>>
		where T : class, IEntity, new()
	{
        public override string RuleCode => GetType().FullName;

        public override void Execute(StaticDataListModel<T> model)
        {
            model.Data[RunParams.RowIndex].Changed = true;
        }
    }

    public class FormStaticTypeSaved<T> : FormEditBase<StaticDataListModel<T>>
		where T : class, IEntity, new()
	{
        protected override void Define(FormEntityTypeBuilder<StaticDataListModel<T>> f)
        {
            f.DisplayName = "Static Type changes saved successfully";
            f.Button(ButtonActionTypes.Close);
        }
    }
}
