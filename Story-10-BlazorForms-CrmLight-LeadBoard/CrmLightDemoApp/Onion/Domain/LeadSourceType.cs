namespace CrmLightDemoApp.Onion.Domain
{
	public class LeadSourceType : IEntity
	{
		public int Id {get;set;}
		public bool Deleted { get; set; }
		public virtual string? Name { get; set; }
	}
}
