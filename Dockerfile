# This stage is used when running fast-mode debugging (e.g. Dev Containers / VS Code attach)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

# ---- Build stage ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy only the .csproj/.sln files first — this is the key caching trick
COPY PortalQuest.sln .
COPY PortalQuest.Domain/PortalQuest.Domain.csproj PortalQuest.Domain/
COPY PortalQuest.Application/PortalQuest.Application.csproj PortalQuest.Application/
COPY PortalQuest.Infrastructure/PortalQuest.Infrastructure.csproj PortalQuest.Infrastructure/
COPY PortalQuest.Persistence/PortalQuest.Persistence.csproj PortalQuest.Persistence/
COPY PortalQuest.Web/PortalQuest.Web.csproj PortalQuest.Web/
COPY PortalQuest.Console/PortalQuest.Console.csproj PortalQuest.Console/

# Restore only what PortalQuest.Web needs (it pulls in project references automatically)
RUN dotnet restore PortalQuest.Web/PortalQuest.Web.csproj

# Now copy the rest of the source code
COPY . .

WORKDIR /src/PortalQuest.Web
RUN dotnet build PortalQuest.Web.csproj -c $BUILD_CONFIGURATION -o /app/build

# ---- Publish stage ----
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish PortalQuest.Web.csproj -c $BUILD_CONFIGURATION -o /app/publish -p:UseAppHost=false -p:PublishAot=false

# ---- Final stage (production image) ----
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PortalQuest.Web.dll"]