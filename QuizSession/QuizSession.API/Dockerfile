FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY "QuizSession/QuizSession.API/QuizSession.API.csproj" "QuizSession/QuizSession.API/"
COPY "QuizSession/QuizSession.Application/QuizSession.Application.csproj" "QuizSession/QuizSession.Application/"
COPY "SharedLibrary/SharedLibrary.csproj" "SharedLibrary/"
COPY "QuizSession/QuizSession.Domain/QuizSession.Domain.csproj" "QuizSession/QuizSession.Domain/"
COPY "QuizSession/QuizSession.Infrastructure/QuizSession.Infrastructure.csproj" "QuizSession/QuizSession.Infrastructure/"

RUN dotnet restore "QuizSession/QuizSession.API/QuizSession.API.csproj"

COPY QuizSession/QuizSession.API/ QuizSession/QuizSession.API/
COPY QuizSession/QuizSession.Application/ QuizSession/QuizSession.Application/
COPY SharedLibrary/ SharedLibrary/
COPY QuizSession/QuizSession.Domain/ QuizSession/QuizSession.Domain/
COPY QuizSession/QuizSession.Infrastructure/ QuizSession/QuizSession.Infrastructure/

WORKDIR "/src/QuizSession/QuizSession.API"
RUN dotnet build "QuizSession.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "QuizSession.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY QuizSession/QuizSession.API/secrets ./secrets

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "QuizSession.API.dll"]