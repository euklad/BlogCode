using DataImport.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
        services.AddDbContextFactory<MyDbContext>(options => options.UseSqlServer());
    })
    .Build();

var db = app.Services.GetService<MyDbContext>();
db.Database.EnsureCreated();

Console.ReadKey();