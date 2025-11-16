FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY "Authentication/Authentication.API/Authentication.API.csproj" "Authentication/Authentication.API/"
COPY "Authentication/Authentication.Application/Authentication.Application.csproj" "Authentication/Authentication.Application/"
COPY "SharedLibrary/SharedLibrary.csproj" "SharedLibrary/"
COPY "Authentication/Authentication.Domain/Authentication.Domain.csproj" "Authentication/Authentication.Domain/"
COPY "Authentication/Authentication.Infrastructure/Authentication.Infrastructure.csproj" "Authentication/Authentication.Infrastructure/"

RUN dotnet restore "Authentication/Authentication.API/Authentication.API.csproj"

COPY Authentication/Authentication.API/ Authentication/Authentication.API/
COPY Authentication/Authentication.Application/ Authentication/Authentication.Application/
COPY SharedLibrary/ SharedLibrary/
COPY Authentication/Authentication.Domain/ Authentication/Authentication.Domain/
COPY Authentication/Authentication.Infrastructure/ Authentication/Authentication.Infrastructure/

WORKDIR "/src/Authentication/Authentication.API"
RUN dotnet build "Authentication.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "Authentication.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY Authentication/Authentication.API/secrets ./secrets

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Authentication.API.dll"]