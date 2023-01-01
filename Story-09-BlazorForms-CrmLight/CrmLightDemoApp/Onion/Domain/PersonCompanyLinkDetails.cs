namespace CrmLightDemoApp.Onion.Domain
{
    public class PersonCompanyLinkDetails : PersonCompanyLink
    {
        public virtual string? PersonFullName { get; set; }
        public virtual string? PersonFirstName { get; set; }
        public virtual string? PersonLastName { get; set; }
        public virtual string? CompanyName { get; set; }
        public virtual string? LinkTypeName { get; set; }
    }
}
