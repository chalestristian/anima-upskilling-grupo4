FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY ./*.csproj ./
RUN dotnet restore

COPY ./ ./

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS runtime
WORKDIR /app
COPY --from=build /app ./

COPY Assets /app/out/Assets

ENV PATH="${PATH}:/root/.dotnet/tools"
RUN dotnet tool install --global dotnet-ef --version 7.0.9
#RUN dotnet ef database update

ENV ASPNETCORE_URLS=http://localhost:5100

EXPOSE 5100

ENTRYPOINT ["dotnet", "out/api.dll"]
