using BlazorForms.FlowRules;
using BlazorForms.Forms;
using BlazorForms.Shared;
using CrmLightDemoApp.Onion.Domain.Repositories;
using CrmLightDemoApp.Onion.Services.Abstractions;
using CrmLightDemoApp.Onion.Services.Model;

namespace CrmLightDemoApp.Onion.Services.Flow.LeadBoard
{
    public class FormLeadCardEdit : FormEditBase<LeadBoardCardModel>
    {
        protected override void Define(FormEntityTypeBuilder<LeadBoardCardModel> f)
        {
            f.DisplayName = "Lead Card";
            f.Rule(typeof(FormLeadCard_RefreshSources), FormRuleTriggers.Loaded);
            f.Confirm(ConfirmType.ChangesWillBeLost, "If you leave before saving, your changes will be lost.", ConfirmButtons.OkCancel);
            f.Layout = FormLayout.TwoColumns;

            f.Group("left");

            f.Property(p => p.State).IsReadOnly();
            f.Property(p => p.Title).IsRequired();
            f.Property(p => p.Description);

            f.Property(p => p.SalesPersonId).DropdownSearch(p => p.AllPersons, m => m.Id, m => m.FullName).Label("Sales person").IsRequired()
                .ItemDialog(typeof(PersonDialogFlow));

            f.Property(p => p.RelatedCompanyId).DropdownSearch(p => p.AllCompanies, m => m.Id, m => m.Name).Label("Lead company")
                .ItemDialog(typeof(CompanyDialogFlow));

            f.Property(p => p.RelatedPersonId).DropdownSearch(p => p.AllPersons, m => m.Id, m => m.FullName).Label("Lead contact")
                .ItemDialog(typeof(PersonDialogFlow));

            f.Property(p => p.LeadSourceTypeId).Dropdown(p => p.AllLeadSources, m => m.Id, m => m.Name).Label("Lead source");

            f.Property(p => p.Phone);
            f.Property(p => p.Email);
            f.Property(p => p.ContactDetails).Label("Other contact info");

            f.Group("right");

            f.Property(p => p.Comments).Control(ControlType.TextArea);

            f.CardList(p => p.CardHistory, e =>
            {
                e.DisplayName = "Comment history";
                e.Card(p => p.TitleMarkup, p => p.Text, p => p.AvatarMarkup);

                e.Rule(typeof(FormLeadCardEdit_ItemChangedRule));
                e.Rule(typeof(FormLeadCardEdit_ItemDeletingRule), FormRuleTriggers.ItemDeleting);
                e.Confirm(ConfirmType.DeleteItem, "Delete this comment?", ConfirmButtons.YesNo);

                e.Button(ButtonActionTypes.Edit);
                e.Button(ButtonActionTypes.Delete);
            });

            f.Button(ButtonActionTypes.Submit, "Save");
            f.Button(ButtonActionTypes.Cancel, "Cancel");
        }
    }

    public class FormLeadCard_RefreshSources : FlowRuleAsyncBase<LeadBoardCardModel>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IBoardCardHistoryRepository _boardCardHistoryRepository;
        private readonly IAppAuthState _appAuthState;

        public override string RuleCode => "BRD-4";

        public FormLeadCard_RefreshSources(ICompanyRepository companyRepository, IPersonRepository personRepository,
            IBoardCardHistoryRepository boardCardHistoryRepository, IAppAuthState appAuthState)
        {
            _companyRepository = companyRepository;
            _personRepository = personRepository;
            _boardCardHistoryRepository = boardCardHistoryRepository;
            _appAuthState = appAuthState;
        }

        public override async Task Execute(LeadBoardCardModel model)
        {
            // refresh drop down sources
            model.AllPersons = (await _personRepository.GetAllAsync())
                .Select(x =>
                {
                    var item = new PersonModel();
                    x.ReflectionCopyTo(item);
                    item.FullName = $"{x.FirstName} {x.LastName}";
                    return item;
                }).OrderBy(x => x.FullName).ToList();

            model.AllCompanies = (await _companyRepository.GetAllAsync())
                .Select(x =>
                {
                    var item = new CompanyModel();
                    x.ReflectionCopyTo(item);
                    return item;
                }).OrderBy(x => x.Name).ToList();

            // refresh comments
            if (model.Id > 0)
            {
                model.CardHistory = (await _boardCardHistoryRepository.GetListByCardIdAsync(model.Id))
                    .Select(x =>
                    {
                        var item = new CardHistoryModel();
                        x.ReflectionCopyTo(item);
                        return item;
                    }).ToList();
            }

            // refresh card buttons - display buttons only for comment owners
            for (int i = 0; i < model.CardHistory.Count; i++)
            {
                var isCurrentUser = _appAuthState.GetCurrentUser().Id == model.CardHistory[i].PersonId;
                Result.Fields[FindField(m => m.CardHistory, ModelBinding.EditButtonBinding, i)].Visible = isCurrentUser;
                Result.Fields[FindField(m => m.CardHistory, ModelBinding.DeleteButtonBinding, i)].Visible = isCurrentUser;
            }
        }
    }

    public class FormLeadCardEdit_ItemChangedRule : FlowRuleAsyncBase<LeadBoardCardModel>
    {
        private readonly IBoardCardHistoryRepository _boardCardHistoryRepository;

        public override string RuleCode => "BRD-5";

        public FormLeadCardEdit_ItemChangedRule(IBoardCardHistoryRepository boardCardHistoryRepository)
        {
            _boardCardHistoryRepository = boardCardHistoryRepository;
        }

        public override async Task Execute(LeadBoardCardModel model)
        {
            var changedCard = model.CardHistory[RunParams.RowIndex];
            changedCard.EditedDate = DateTime.Now;
            await _boardCardHistoryRepository.UpdateAsync(changedCard);
            Result.SkipThisChange = true;
        }
    }

    public class FormLeadCardEdit_ItemDeletingRule : FlowRuleAsyncBase<LeadBoardCardModel>
    {
        private readonly IBoardCardHistoryRepository _boardCardHistoryRepository;
        private readonly IAppAuthState _appAuthState;

        public override string RuleCode => "BRD-6";

        public FormLeadCardEdit_ItemDeletingRule(IBoardCardHistoryRepository boardCardHistoryRepository, IAppAuthState appAuthState)
        {
            _boardCardHistoryRepository = boardCardHistoryRepository;
            _appAuthState = appAuthState;
        }

        public override async Task Execute(LeadBoardCardModel model)
        {
            await _boardCardHistoryRepository.SoftDeleteAsync(model.CardHistory[RunParams.RowIndex]);
            Result.SkipThisChange = true;
        }
    }
}
