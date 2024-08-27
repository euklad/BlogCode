using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImport.Domain.Entities;
public class Product
{
    public int Id { get; set; }
    public string? ExternalId { get; set; }
    public string? ProductCode { get; set; }
    public string ProductName { get; set; } = null!;
    public string? Description { get; set; }
    public string ProductTypeName { get; set; } = null!;

    public ICollection<Purchase>? PurchaseLists { get; }
}
