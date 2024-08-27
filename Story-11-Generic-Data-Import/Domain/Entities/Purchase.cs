using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImport.Domain.Entities;

public class Purchase
{
    public int Id { get; set; }
    public string? ExternalId { get; set; }
    public decimal? TotalAmount { get; set; }
    public DateTime? PeriodStartDate { get; set; }
    public DateTime? PeriodEndDate { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? PaymentDate { get; set; }

    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }

}
