FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia el .sln y los .csproj desde la raíz del repo en GitHub (que es webapicore/)
COPY webapicore.csproj ./webapicore/
COPY ../modelo/modelo.csproj ./modelo/
COPY ../comun/comun.csproj ./comun/
COPY ../logicanegocio/logicanegocio.csproj ./logicanegocio/
COPY ../accesodatos/accesodatos.csproj ./accesodatos/
COPY webapicore.sln .

# Restaura dependencias
RUN dotnet restore "./webapicore.sln"

# Copia el resto del código
COPY . .

# Publica el proyecto
WORKDIR /src/webapicore
RUN dotnet publish -c Release -o /app/out

FROM base AS final
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "webapicore.dll"]
