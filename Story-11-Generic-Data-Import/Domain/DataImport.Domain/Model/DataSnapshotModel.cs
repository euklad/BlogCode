using DataImport.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImport.Domain.Model;

public class DataSnapshotModel
{
    public List<Customer> Customers { get; set; } = [];
    public List<Product> Products { get; set; } = [];
    public List<Purchase> Purchases { get; set; } = [];
}

public class DataLineModel
{
    public Customer Customer { get; set; } = new();
    public Product Product { get; set; } = new();
    public Purchase Purchase { get; set; } = new();
}
