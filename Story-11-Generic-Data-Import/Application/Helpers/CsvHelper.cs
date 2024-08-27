using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataImport.Application.Helpers;
public static class CsvHelper
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

    
}
