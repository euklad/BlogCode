namespace DataImport.Domain.Model;
public class MappingConfig
{
    public Dictionary<string, List<FileMapping>> FileMappings { get; set; } = [];
}

public class FileMapping
{
    public string JsonPath { get; set; } = null!;
    public int Position { get; set; }
}
