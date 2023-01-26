namespace CrmLightDemoApp.Onion.Domain
{
    public interface IEntity
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
    }
}
