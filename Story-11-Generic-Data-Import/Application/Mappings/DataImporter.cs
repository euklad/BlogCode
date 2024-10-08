﻿using DataImport.Domain.Model;
using JsonPathToModel;
using DataImport.Application.Helpers;

namespace DataImport.Application.Mappings;
public class DataImporter
{
    private readonly IJsonPathModelNavigator _navigator;

    public DataImporter(IJsonPathModelNavigator navigator)
    {
        _navigator = navigator;
    }

    public DataSnapshotModel ReadModelFromFiles(List<string> fileList, MappingConfig config)
    {
        var model = new DataSnapshotModel();

        foreach (var filePath in fileList)
        {
            var csv = FileHelper.GetCsvFileLines(filePath);

            // skip the 1st line with column definitions
            foreach (var line in csv.Skip(1))
            {
                var cells = line.Split(',');
                ImportFileLine(model, config, filePath, cells);
            }
        }

        return model;
    }

    private void ImportFileLine(DataSnapshotModel model, MappingConfig config, string filePath, string[] cells)
    {
        var fileName = Path.GetFileName(filePath);
        var mappings = config.FileMappings[fileName];
        var target = new DataLineModel();

        // iterate through mappings and populate record values from cells
        foreach (var mapping in mappings)
        {
            _navigator.SetValue(target, mapping.JsonPath, cells[mapping.Position]);
        }

        // add to model only those entities that have at least ExternalId updated  
        if (target.Customer.ExternalId != null)
        {
            target.Customer.Source = fileName;
            model.Customers.Add(target.Customer);
        }
        if (target.Product.ExternalId != null)
        {
            target.Product.Source = fileName;
            model.Products.Add(target.Product);
        }
        if (target.Purchase.ExternalId != null)
        {
            target.Purchase.Source = fileName;
            model.Purchases.Add(target.Purchase);
        }
    }
}
