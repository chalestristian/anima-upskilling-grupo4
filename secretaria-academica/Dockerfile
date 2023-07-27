# Define a imagem base do ASP.NET
FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8

# Define o diretório de trabalho dentro do container
WORKDIR /inetpub/wwwroot

# Copia os arquivos do projeto da aplicação WebForms para o diretório de trabalho do container
COPY ./secretaria_academica/. .

# Define a porta que será exposta pelo container
EXPOSE 5600

# Inicia o servidor IIS
ENTRYPOINT ["C:\\ServiceMonitor.exe", "w3svc"]
