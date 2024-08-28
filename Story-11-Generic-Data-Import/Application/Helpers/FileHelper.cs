using DataImport.Domain.Model;
using System.Text.Json;

namespace DataImport.Application.Helpers;
public static class FileHelper
{
    public static List<string> GetCsvFileLines(string fileName)
    {
        var result = new List<string>();

        using Stream stream = File.OpenRead(fileName)!;
        using StreamReader reader = new StreamReader(stream);
            
        while (!reader.EndOfStream)
        {
            result.Add(reader.ReadLine()!);
        }

        return result;
    }

    public static MappingConfig LoadConfig(string fileName)
    {
        using Stream stream = File.OpenRead(fileName)!;
        using StreamReader reader = new StreamReader(stream);

        var json = reader.ReadToEnd();
        var config = JsonSerializer.Deserialize<MappingConfig>(json)!;
        return config;
    }
}
