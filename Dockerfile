# ---- Stage 1: Build ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
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

# Publish the Web project
WORKDIR /src/PortalQuest.Web
# RUN dotnet publish -c Release -o /app/publish --no-restore

RUN dotnet publish -c Release -o /app/publish --no-restore -p:PublishAot=false

# ---- Stage 2: Runtime ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "PortalQuest.Web.dll"]