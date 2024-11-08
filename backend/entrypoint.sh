# backend/entrypoint.sh
#!/bin/bash

# Ожидаем доступности базы данных
until dotnet ef database update; do
  echo "Ждем доступности базы данных..."
  sleep 3
done

# Запускаем приложение после успешного выполнения миграций
dotnet backend.dll
