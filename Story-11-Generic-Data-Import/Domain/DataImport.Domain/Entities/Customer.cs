using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImport.Domain.Entities;

public class Customer
{
    public int Id { get; set; }
    public string? ExternalId { get; set; }
    public string? Title { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Mobile { get; set; }
    public string? Email { get; set; }

}
