FROM mcr.microsoft.com/dotnet/core/sdk:3.0

WORKDIR /app

COPY . ./

RUN dotnet build -c Release

RUN dotnet publish -c Release -o publish

WORKDIR /app/publish
ENV ASPNETCORE_URLS http://+:80

CMD ["dotnet", "StoreApp.WebApp.dll"]

