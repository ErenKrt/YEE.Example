# İkinci aşama: ASP.NET uygulamasını oluşturun
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
EXPOSE 80

COPY ../src/YEE.Identity/YEE.Identity.API/YEE.Identity.API.csproj YEE.Identity.API/
RUN dotnet restore YEE.Identity.API/YEE.Identity.API.csproj

COPY src ./
RUN dotnet build YEE.Identity.API/YEE.Identity.API.csproj -c Release -o /app/build
RUN dotnet publish YEE.Identity.API/YEE.Identity.API.csproj -c Release -o /app/publish /p:UseAppHost=false
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "YEE.Identity.API.dll"]
