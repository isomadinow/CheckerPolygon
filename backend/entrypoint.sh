#!/bin/bash

# Проверяем доступность базы данных
until nc -z -v -w30 postgres 5432
do
  echo "Ожидание доступности базы данных..."
  sleep 3
done

echo "База данных доступна. Запуск backend..."
exec dotnet backend.dll
