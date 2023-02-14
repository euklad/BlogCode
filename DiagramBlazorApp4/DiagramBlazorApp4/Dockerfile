#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DiagramBlazorApp4/DiagramBlazorApp4.csproj", "DiagramBlazorApp4/"]
RUN dotnet restore "DiagramBlazorApp4/DiagramBlazorApp4.csproj"
COPY . .
WORKDIR "/src/DiagramBlazorApp4"
RUN dotnet build "DiagramBlazorApp4.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DiagramBlazorApp4.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DiagramBlazorApp4.dll"]