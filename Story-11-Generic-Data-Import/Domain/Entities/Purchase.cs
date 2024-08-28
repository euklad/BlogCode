namespace DataImport.Domain.Entities;

public class Purchase
{
    public int Id { get; set; }
    public string? ExternalId { get; set; }
    public string? CustomerExternalId { get; set; }
    public string? ProductExternalId { get; set; }
    public decimal? TotalAmount { get; set; }
    public DateTime? PeriodStartDate { get; set; }
    public DateTime? PeriodEndDate { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? PaymentDate { get; set; }

    public string? Source { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }
}
