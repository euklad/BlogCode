using BlazorForms.Flows;
using BlazorForms.Shared;
using CrmLightDemoApp.Onion.Domain.Repositories;
using CrmLightDemoApp.Onion.Services.Model;

namespace CrmLightDemoApp.Onion.Services.Flow.LeadBoard
{
    public class PersonDialogFlow : DialogFlowBase<PersonModel, FormPersonEdit>
    {
        private readonly IPersonRepository _personRepository;

        public PersonDialogFlow(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public override async Task LoadDataAsync()
        {
            if (GetId() > 0)
            {
                var record = await _personRepository.GetByIdAsync(GetId());
                record.ReflectionCopyTo(Model);
            }

            var fullName = Params["Name"];

            if (fullName != null)
            {
                var split = fullName.Split(' ');
                Model.FirstName = split[0];

                if (split.Count() > 1)
                {
                    Model.LastName = split[1];
                }
            }
        }

        public override async Task SaveDataAsync()
        {
            // we need full name for drop down option
            Model.FullName = $"{Model.FirstName} {Model.LastName}";

            if (GetId() > 0)
            {
                await _personRepository.UpdateAsync(Model);
            }
            else
            {
                Model.Id = await _personRepository.CreateAsync(Model);
            }
        }
    }
}
