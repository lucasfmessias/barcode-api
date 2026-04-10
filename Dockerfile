# Estágio 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia os arquivos de projeto primeiro para aproveitar o cache das camadas
COPY ["Api/Api.csproj", "Api/"]
COPY ["Domain/Domain.csproj", "Domain/"]

# Restaura as dependências
RUN dotnet restore "Api/Api.csproj"

# Copia o restante do código
COPY . .

# Compila o projeto
WORKDIR "/src/Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build

# Estágio 2: Publish
FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Estágio 3: Final (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Define a porta da API
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "Api.dll"]