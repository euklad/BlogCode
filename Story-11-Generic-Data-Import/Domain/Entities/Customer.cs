namespace DataImport.Domain.Entities;

public class Customer
{
    public int Id { get; set; }
    public string? ExternalId { get; set; }
    public string? Title { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Mobile { get; set; }
    public string? Email { get; set; }
    public string? Source { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public ICollection<Purchase>? PurchaseLists { get; }
}
