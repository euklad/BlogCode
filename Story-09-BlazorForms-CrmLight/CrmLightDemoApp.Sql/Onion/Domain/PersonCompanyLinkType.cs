﻿namespace CrmLightDemoApp.Onion.Domain
{
    public class PersonCompanyLinkType : IEntity
    {
        public virtual int Id { get; set; }
        public virtual string? Name { get; set; }
        public virtual bool Deleted { get; set; }

        public List<PersonCompanyLink> RefPersonCompanyLink { get; } = new();
    }
}
