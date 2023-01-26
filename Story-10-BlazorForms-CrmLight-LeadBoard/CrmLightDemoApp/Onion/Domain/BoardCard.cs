namespace CrmLightDemoApp.Onion.Domain
{
	public class BoardCard : IEntity
	{
		public virtual int Id { get; set; }
		public virtual bool Deleted { get; set; }

		public virtual string State { get; set; }
		public virtual string Title { get; set; }
		public virtual string? Description { get; set; }
		public virtual int Order { get; set; }

        public virtual int? LeadSourceTypeId { get; set; }
        public virtual string? ContactDetails { get; set; }
		public virtual int? RelatedCompanyId { get; set; }
		public virtual int? RelatedPersonId { get; set; }
		public virtual string? Phone { get; set; }
		public virtual string? Email { get; set; }
		public virtual int? SalesPersonId { get; set; }

        public virtual string? FollowUpDetails { get; set; }
        public virtual DateTime? FollowUpDate { get; set; }

        public virtual int? ClientCompanyId { get; set; }
    }
}
