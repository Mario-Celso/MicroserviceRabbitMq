# Estágio de construção
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copie os arquivos do projeto e restaure as dependências
COPY ["MicroserviceRabbitMq.csproj", "."]
RUN dotnet restore "./MicroserviceRabbitMq.csproj"

# Copie todo o restante e compile a aplicação
COPY . .
RUN dotnet build "./MicroserviceRabbitMq.csproj" -c Release -o /app/build

# Estágio de publicação
FROM build AS publish
RUN dotnet publish "./MicroserviceRabbitMq.csproj" -c Release -o /app/publish

# Estágio final - execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroserviceRabbitMq.dll"]