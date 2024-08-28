using DataImport.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataImport.Application.Mappings;
using DataImport.Application.Helpers;

namespace DataImport.Console;

class Program
{
    static void Main(string[] args)
    {
        // app builder
        var app = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.Sources.Clear();
                config.AddConfiguration(hostingContext.Configuration);
                config.AddJsonFile("appsettings.json");
                config.AddJsonFile($"appsettings.Development.json", true, true);
            })
            .ConfigureServices(services =>
            {
                services.AddDbContext<MyDbContext>(options => options.UseSqlServer(), contextLifetime: ServiceLifetime.Singleton);
                services.AddSingleton<IDataAccessService, DataAccessService>();
            })
            .Build();

        var dataService = app.Services.GetService<IDataAccessService>()!;

        // read arguments
        var configFile = args[0];
        var config = FileHelper.LoadConfig(configFile);
        var csvFiles = new List<string>();

        for (int i = 1; i < args.Length; i++)
        {
            csvFiles.Add(args[i]);
        }

        // read snapshot
        var dataImporter = new DataImporter();
        var shapshot = dataImporter.ReadModelFromFiles(csvFiles, config);

        // save snapshot to db
        dataService.ImportDataSnapshot(shapshot);

        System.Console.ReadKey();
    }
}