# 1. ESTÁGIO DE CONSTRUÇÃO (Build)
# Usamos o SDK (Software Development Kit) que contém o compilador
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia o arquivo de projeto (.csproj) e restaura as dependências (NuGet)
# Fazemos isso separado para aproveitar o cache do Docker
COPY ["Login.csproj", "./"]
RUN dotnet restore

# Agora copia o restante dos arquivos e compila
COPY . .
RUN dotnet build "Login.csproj" -c Release -o /app/build

# Publica o app (gera os arquivos finais otimizados para produção)
FROM build AS publish
RUN dotnet publish "Login.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 2. ESTÁGIO DE EXECUÇÃO (Runtime)
# Usamos uma imagem muito menor que só tem o necessário para rodar o app
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Copia apenas os arquivos compilados do estágio anterior
COPY --from=publish /app/publish .

# CONFIGURAÇÕES PARA O RENDER
# O Render espera que o container ouça na porta 8080 por padrão
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Comando para iniciar a aplicação
# Substitua "SeuProjeto.dll" pelo nome real do arquivo gerado (geralmente o nome do seu .csproj)
ENTRYPOINT ["dotnet", "Login.dll"]
