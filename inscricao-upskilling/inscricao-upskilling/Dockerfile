FROM mcr.microsoft.com/dotnet/framework/aspnet:4.6.2 AS build

WORKDIR /app

COPY . .

RUN dotnet restore

RUN dotnet build --configuration Release --no-restore

RUN dotnet publish --configuration Release --no-build --output /app/publish

FROM mcr.microsoft.com/dotnet/framework/aspnet:4.6.2 AS final

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 5700

ENTRYPOINT ["dotnet", "inscricao-upskilling.dll"]
