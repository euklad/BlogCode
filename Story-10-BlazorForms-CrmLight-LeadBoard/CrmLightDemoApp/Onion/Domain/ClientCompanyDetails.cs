using CrmLightDemoApp.Onion.Services.Model;

namespace CrmLightDemoApp.Onion.Domain
{
    public class ClientCompanyDetails : ClientCompany
    {
        public virtual string? CompanyName { get; set; }
        public virtual string? ManagerFirstName { get; set; }
        public virtual string? ManagerLastName { get; set; }
        public virtual string? AlternativeManagerFirstName { get; set; }
        public virtual string? AlternativeManagerLastName { get; set; }

        //public string? ManagerFullName {  get { return $"{ManagerFirstName} {ManagerLastName}"; } }
        //public string? AlternativeManagerFullName {  get { return $"{AlternativeManagerFirstName} {AlternativeManagerLastName}"; } }
    }
}
