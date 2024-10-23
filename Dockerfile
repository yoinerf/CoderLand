# Etapa base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Instalar dotnet-ef
RUN dotnet tool install --global dotnet-ef

# Configurar el PATH para las herramientas globales
ENV PATH="$PATH:/root/.dotnet/tools"

# Copiar y restaurar dependencias
COPY ["AutoApi.csproj", "."]
RUN dotnet restore "AutoApi.csproj"

# Copiar el resto del código
COPY . .

# Construir el proyecto
RUN dotnet build "AutoApi.csproj" -c Release -o /app/build

# Etapa de publicación
FROM build AS publish
RUN dotnet publish "AutoApi.csproj" -c Release -o /app/publish

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Este debe coincidir con el nombre del .dll generado en la etapa de build/publish
ENTRYPOINT ["dotnet", "AutoApi.dll"]
