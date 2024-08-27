using DataImport.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImport.Infrastructure;

public interface IDataAccessService
{
    void ImportDataSnapshot(DataSnapshotModel snapshot);
}

public class DataAccessService : IDataAccessService
{
    private readonly MyDbContext _db;

    public DataAccessService(MyDbContext db)
    {
        _db = db;
    }

    public void ImportDataSnapshot(DataSnapshotModel snapshot)
    {
        using (var transaction = _db.Database.BeginTransaction())
        {
            InsertCustomers(snapshot);
            InsertProducts(snapshot);
            InsertPurchases(snapshot);

            transaction.Commit();
        }
    }

    private void InsertPurchases(DataSnapshotModel snapshot)
    {
        // resolve Id by ExternalId
        var customerDict = snapshot.Customers.ToDictionary(c => c.ExternalId!, c => c.Id);
        var productDict = snapshot.Products.ToDictionary(c => c.ExternalId!, c => c.Id);

        foreach (var item in snapshot.Purchases)
        {
            item.CustomerId = customerDict[item.CustomerExternalId!];
            item.ProductId = customerDict[item.ProductExternalId!];
            _db.Add(item);
        }

        _db.SaveChanges();
    }

    private void InsertProducts(DataSnapshotModel snapshot)
    {
        foreach (var item in snapshot.Products)
        {
            _db.Add(item);
        }

        _db.SaveChanges();
    }

    private void InsertCustomers(DataSnapshotModel snapshot)
    {
        foreach (var item in snapshot.Customers)
        {
            _db.Add(item);
        }

        _db.SaveChanges();
    }
}
