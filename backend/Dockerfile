# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Копируем и восстанавливаем зависимости
COPY ./backend/backend.csproj ./
RUN dotnet restore "./backend.csproj"

# Копируем оставшиеся файлы и публикуем приложение
COPY ./backend ./
RUN dotnet publish "./backend.csproj" -c Release -o /app/publish

# Этап выполнения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

# Копируем entrypoint.sh и устанавливаем права на его выполнение
COPY ./backend/entrypoint.sh ./
RUN chmod +x /app/entrypoint.sh

# Открываем порт
EXPOSE 5000

# Устанавливаем entrypoint.sh как точку входа
ENTRYPOINT ["/app/entrypoint.sh"]
