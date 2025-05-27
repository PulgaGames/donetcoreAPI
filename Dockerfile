# Imagen base para ejecutar la app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Imagen para construir el proyecto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia los archivos de solución y proyectos
COPY ./webapicore/webapicore.sln .
COPY ./webapicore/webapicore.csproj ./webapicore/
COPY ./modelo/modelo.csproj ./modelo/
COPY ./comun/comun.csproj ./comun/
COPY ./logicanegocio/logicanegocio.csproj ./logicanegocio/
COPY ./accesodatos/accesodatos.csproj ./accesodatos/

# Restaurar dependencias
RUN dotnet restore ./webapicore.sln

# Copiar el resto del contenido
COPY . .

# Publicar el proyecto
WORKDIR /src/webapicore
RUN dotnet publish -c Release -o /app/out

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "webapicore.dll"]

