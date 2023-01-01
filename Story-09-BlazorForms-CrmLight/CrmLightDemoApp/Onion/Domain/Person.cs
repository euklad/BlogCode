namespace CrmLightDemoApp.Onion.Domain
{
    public class Person : IEntity
    {
        public virtual int Id { get; set; }
        public virtual string? FirstName { get; set; }
        public virtual string? LastName { get; set; }
        public virtual DateTime? BirthDate { get; set; }
        public virtual string? Phone { get; set; }
        public virtual string? Email { get; set; }
        public virtual DateTime? LastUpdatedOn { get; set; }
        public virtual bool Deleted { get; set; }

    }
}
