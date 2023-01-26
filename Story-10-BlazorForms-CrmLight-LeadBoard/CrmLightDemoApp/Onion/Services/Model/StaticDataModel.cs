using BlazorForms.Flows.Definitions;
using BlazorForms.Shared;
using CrmLightDemoApp.Onion.Domain;
using System.Dynamic;

namespace CrmLightDemoApp.Onion.Services.Model
{
    public class StaticDataModel<T>
		where T : class
	{
		public int Id { get; set; }
		public bool Deleted { get; set; }
		public virtual string? Name { get; set; }
		public virtual bool Changed { get; set; }
    }

    public class StaticDataListModel<T> : IFlowModel
        where T : class
    {
        public virtual List<StaticDataModel<T>>? Data { get; set; }
        public virtual List<StaticDataModel<T>>? Deleted { get; set; } = new();
    }
}
