# İkinci aşama: ASP.NET uygulamasını oluşturun
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
EXPOSE 80

COPY ../src/YEE.Inventory/YEE.Inventory.API/YEE.Inventory.API.csproj YEE.Inventory.API/
RUN dotnet restore YEE.Inventory.API/YEE.Inventory.API.csproj

COPY src ./
RUN dotnet build YEE.Inventory.API/YEE.Inventory.API.csproj -c Release -o /app/build
RUN dotnet publish YEE.Inventory.API/YEE.Inventory.API.csproj -c Release -o /app/publish /p:UseAppHost=false
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "YEE.Inventory.API.dll"]
