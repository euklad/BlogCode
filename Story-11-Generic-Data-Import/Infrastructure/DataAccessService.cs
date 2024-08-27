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
    }
}
