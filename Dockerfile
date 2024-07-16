# Est�gio de constru��o
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copie os arquivos do projeto e restaure as depend�ncias
COPY ["MicroserviceRabbitMq.csproj", "."]
RUN dotnet restore "./MicroserviceRabbitMq.csproj"

# Copie todo o restante e compile a aplica��o
COPY . .
RUN dotnet build "./MicroserviceRabbitMq.csproj" -c Release -o /app/build

# Est�gio de publica��o
FROM build AS publish
RUN dotnet publish "./MicroserviceRabbitMq.csproj" -c Release -o /app/publish

# Est�gio final - execu��o
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroserviceRabbitMq.dll"]