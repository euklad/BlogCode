using DataImport.Domain.Model;
using JsonPathToModel;
using DataImport.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImport.Application.Mappings;
public class DataImporter
{
    private readonly IJsonPathModelNavigator _navigator;
    public DataImporter()
    {
        _navigator = new JsonPathModelNavigator(
            new NavigatorConfigOptions
            {
                OptimizeWithCodeEmitter = false
            });
    }

    public ImportModel ReadModelFromFiles(List<string> fileList, MappingConfig config)
    {
        var model = new ImportModel();

        foreach (var filePath in fileList)
        {
            var csv = CsvHelper.GetCsvFileLines(filePath);

            // skip the 1st line with column definitions
            foreach (var line in csv.Skip(1))
            {
                var cells = line.Split(',');
                ImportFileLine(model, config, filePath, cells);
            }
        }

        return model;
    }

    private void ImportFileLine(ImportModel model, MappingConfig config, string filePath, string[] cells)
    {
        var fileName = Path.GetFileName(filePath);
        var mappings = config.FileMappings[fileName];
        var record = new ImportModelRecord();

        // iterate through mappings and populate record values from cells
        foreach (var mapping in mappings)
        {
            _navigator.SetValue(record, mapping.JsonPath, cells[mapping.Position]);
        }

        // add to model only those entities that have at least ExternalId updated  
        if (record.Customer.ExternalId != null)
        {
            model.Customers.Add(record.Customer);
        }
        if (record.Product.ExternalId != null)
        {
            model.Products.Add(record.Product);
        }
        if (record.Purchase.ExternalId != null)
        {
            model.Purchases.Add(record.Purchase);
        }
    }
}
