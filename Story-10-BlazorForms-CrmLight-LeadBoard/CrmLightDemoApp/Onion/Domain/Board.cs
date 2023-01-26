namespace CrmLightDemoApp.Onion.Domain
{
	public class Board : IEntity
	{
		public virtual int Id { get; set; }
		public virtual bool Deleted { get; set; }

		public virtual string Name { get; set; }

		// FK
		public List<BoardCard> RefBoardCard { get; } = new();
	}
}
