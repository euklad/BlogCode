***Generate Migrations***
cd .\Console
dotnet ef migrations add Initial -c MyDbContext -p ..\Infrastructure\DataImport.Infrastructure.csproj
