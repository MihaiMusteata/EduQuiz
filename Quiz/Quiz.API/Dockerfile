FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY "Quiz/Quiz.API/Quiz.API.csproj" "Quiz/Quiz.API/"
COPY "Quiz/Quiz.Application/Quiz.Application.csproj" "Quiz/Quiz.Application/"
COPY "SharedLibrary/SharedLibrary.csproj" "SharedLibrary/"
COPY "Quiz/Quiz.Domain/Quiz.Domain.csproj" "Quiz/Quiz.Domain/"
COPY "Quiz/Quiz.Infrastructure/Quiz.Infrastructure.csproj" "Quiz/Quiz.Infrastructure/"

RUN dotnet restore "Quiz/Quiz.API/Quiz.API.csproj"

COPY Quiz/Quiz.API/ Quiz/Quiz.API/
COPY Quiz/Quiz.Application/ Quiz/Quiz.Application/
COPY SharedLibrary/ SharedLibrary/
COPY Quiz/Quiz.Domain/ Quiz/Quiz.Domain/
COPY Quiz/Quiz.Infrastructure/ Quiz/Quiz.Infrastructure/

WORKDIR "/src/Quiz/Quiz.API"
RUN dotnet build "Quiz.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "Quiz.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY Quiz/Quiz.API/secrets ./secrets

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Quiz.API.dll"]