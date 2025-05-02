# Etapa base (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia o csproj e restaura pacotes
COPY WebApplication1.csproj ./
RUN dotnet restore "WebApplication1.csproj"

# Copia todo o restante e compila
COPY . .
RUN dotnet build "WebApplication1.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa de publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WebApplication1.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final (runtime)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish ./
ENTRYPOINT ["dotnet", "WebApplication1.dll"]
