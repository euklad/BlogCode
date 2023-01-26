namespace CrmLightDemoApp.Onion.Domain
{
    public class PersonCompanyLink : IEntity
    {
        public virtual int Id { get; set; }
        public virtual int PersonId { get; set; }
        public virtual int CompanyId { get; set; }
        public virtual int LinkTypeId { get; set; }
        public virtual bool Deleted { get; set; }
    }
}
