# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем и восстанавливаем зависимости
COPY ./backend/backend.csproj ./backend/
WORKDIR /src/backend
RUN dotnet restore

# Копируем остальные файлы и публикуем проект
COPY ./backend ./
RUN dotnet publish -c Release -o /app/publish

# Этап выполнения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Устанавливаем netcat-openbsd для ожидания доступности базы данных
RUN apt-get update && apt-get install -y netcat-openbsd

# Копируем опубликованные файлы из промежуточной директории
COPY --from=build /app/publish .

# Копируем entrypoint.sh
COPY ./backend/entrypoint.sh /app/
RUN chmod +x /app/entrypoint.sh

# Открываем порт
EXPOSE 5000

# Устанавливаем entrypoint.sh как точку входа
ENTRYPOINT ["/app/entrypoint.sh"]
