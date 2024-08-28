using DataImport.Application.Mappings;
using DataImport.Domain.Model;
using System.Reflection;
using System.Text.Json;

namespace DataImport.Tests;

public class UnitTest1
{
    public static MappingConfig LoadConfig(string fileName)
    {
        using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileName)!;
        using StreamReader reader = new StreamReader(stream);

        var json = reader.ReadToEnd();
        var config = JsonSerializer.Deserialize<MappingConfig>(json)!;
        return config;
    }

    [Fact]
    public void DataImporter_Should_ReadCustomer()
    {
        var config = LoadConfig("DataImport.Tests.TestData.TestMappingConfig.json");
        List<string> fileList = ["./TestData/PersonExport.csv"];
        var importer = new DataImporter();
        var model = importer.ReadModelFromFiles(fileList, config);

        Assert.NotNull(model);
        Assert.Equal(3, model.Customers.Count);
        Assert.Equal("C00006", model.Customers[0].ExternalId);
        Assert.Empty(model.Products);
        Assert.Empty(model.Purchases);
    }

    [Fact]
    public void GenerateConfig()
    {
        var mc = new MappingConfig();
        mc.FileMappings.Add("PersonExport.csv", [new FileMapping { Position = 0, JsonPath = "$." }, new FileMapping { Position = 1, JsonPath = "$." }]);
        mc.FileMappings.Add("Product.csv", [new FileMapping { Position = 0, JsonPath = "$." }, new FileMapping { Position = 1, JsonPath = "$." }]);
        mc.FileMappings.Add("Purchase.csv", [new FileMapping { Position = 0, JsonPath = "$." }, new FileMapping { Position = 1, JsonPath = "$." }]);

        var json = JsonSerializer.Serialize(mc, new JsonSerializerOptions { WriteIndented = true });
    }

}