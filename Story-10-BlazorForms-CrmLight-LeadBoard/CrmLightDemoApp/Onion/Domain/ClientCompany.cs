namespace CrmLightDemoApp.Onion.Domain
{
    public class ClientCompany : IEntity
    {
        public virtual int Id { get; set; }
        public virtual int CompanyId { get; set; }
        public virtual DateTime? StartContractDate { get; set; }
        public virtual int? ClientManagerId { get; set; }
        public virtual int? AlternativeClientManagerId { get; set; }
        public virtual bool Deleted { get; set; }
	}
}
