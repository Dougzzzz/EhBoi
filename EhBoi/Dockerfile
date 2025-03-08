# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia o arquivo .csproj da aplicação principal
COPY ["EhBoi/EhBoi.csproj", "EhBoi/"]
# Copia os arquivos .csproj dos outros projetos
COPY ["EhBoi.Domain/EhBoi.Domain.csproj", "EhBoi.Domain/"]
COPY ["EhBoi.Infra/EhBoi.Infra.csproj", "EhBoi.Infra/"]

# Restaura os pacotes NuGet para todos os projetos
RUN dotnet restore "EhBoi/EhBoi.csproj"

# Copia todo o conteúdo da pasta raiz para o container
COPY . .

# Define o diretório de trabalho para o projeto principal
WORKDIR "/src/EhBoi"

# Compila o projeto principal
RUN dotnet build "EhBoi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EhBoi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EhBoi.dll"]