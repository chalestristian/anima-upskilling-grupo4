FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

RUN dotnet tool install --global dotnet-ef --version 7.0.9

ENV PATH="${PATH}:/root/.dotnet/tools"

COPY ./*.csproj ./
RUN dotnet restore

COPY ./ ./

#RUN dotnet ef database update

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

ENV ASPNETCORE_URLS=http://localhost:5100

EXPOSE 5100

ENTRYPOINT ["dotnet", "api.dll"]
