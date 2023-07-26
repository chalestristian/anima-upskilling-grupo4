FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY ./*.csproj ./
RUN dotnet restore

COPY ./ ./

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

RUN apt-get update \
    && apt-get install -y --no-install-recommends libgdiplus \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

COPY Assets /app/Assets

ENV ASPNETCORE_URLS=http://localhost:5400

EXPOSE 5400

ENTRYPOINT ["dotnet", "boleto-api.dll"]
