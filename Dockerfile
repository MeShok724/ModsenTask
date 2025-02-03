# Базовый образ для выполнения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копируем решение и файлы проектов (исправлено)
COPY ModsenTask.sln .
COPY ModsenTask/ModsenTask.API.csproj ModsenTask/
COPY ModsenTask.Application/ModsenTask.Application.csproj ModsenTask.Application/
COPY ModsenTask.Contracts/ModsenTask.Contracts.csproj ModsenTask.Contracts/
COPY ModsenTask.Core/ModsenTask.Core.csproj ModsenTask.Core/
COPY ModsenTask.Infrastructure/ModsenTask.Infrastructure.csproj ModsenTask.Infrastructure/
COPY ModsenTask.Tests/ModsenTask.Tests.csproj ModsenTask.Tests/

# Восстанавливаем зависимости
RUN dotnet restore "ModsenTask.sln"

# Копируем весь исходный код (добавлено)
COPY . .

# Собираем проект
WORKDIR "/src/ModsenTask"
RUN dotnet build "ModsenTask.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этап публикации
FROM build AS publish
RUN dotnet publish "ModsenTask.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish

# Финальный контейнер с ASP.NET Runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ModsenTask.API.dll"]
