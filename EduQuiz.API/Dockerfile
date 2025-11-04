FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["EduQuiz.API/EduQuiz.API.csproj", "EduQuiz.API/"]
COPY ["EduQuiz.Application/EduQuiz.Application.csproj", "EduQuiz.Application/"]
COPY ["EduQuiz.Domain/EduQuiz.Domain.csproj", "EduQuiz.Domain/"]
COPY ["EduQuiz.Infrastructure/EduQuiz.Infrastructure.csproj", "EduQuiz.Infrastructure/"]

RUN dotnet restore "EduQuiz.API/EduQuiz.API.csproj"

COPY . .

WORKDIR "/src/EduQuiz.API"

RUN dotnet build "./EduQuiz.API.csproj" -c $BUILD_CONFIGURATION -o /app/build 

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EduQuiz.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY /EduQuiz.API/secrets ./secrets
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "EduQuiz.API.dll"]