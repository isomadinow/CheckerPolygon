# Проект CheckerPolygon

Проект **CheckerPolygon** включает в себя серверную часть на **ASP.NET Core** и клиентскую часть на **Angular**. Для удобства использования используется **Docker Compose**, который запускает все компоненты вместе.

## Структура проекта

- `/backend` — серверная часть на ASP.NET Core.
- `/polygon-app` — клиентская часть на Angular.
- `/assets` — изображения и другие ресурсы.

## Примеры работы программы

### Проверка точки внутри полигона

![Пример 1](assets/image_1.png)

### Результат проверки

![Пример 2](assets/image_2.png)

### Загрузка данных

![Пример 3](assets/image_3.png)

## Как запустить проект

### Требования

- Установленные **Docker** и **Docker Compose**.

### Шаги

1. **Клонировать репозиторий**:

   ```bash
   git clone https://github.com/isomadinow/CheckerPolygon
   cd CheckerPolygon
   ```

2. **Запустить Docker Compose**:

   ```bash
   docker-compose up --build
   ```

   Эта команда соберет образы и запустит все сервисы.

3. **Открыть приложение**:

   - **Фронтенд** будет доступен по адресу: `http://localhost:80`.
   - **Бэкенд** работает внутри Docker и доступен через фронтенд (http://localhost:5000/swagger/index.html).

## Детали проекта

### Docker Compose

В файле `docker-compose.yml` описаны три сервиса:

- **postgres** — база данных PostgreSQL.
- **backend** — серверное приложение на ASP.NET Core.
- **frontend** — клиентское приложение на Angular с Nginx.

Все сервисы подключены к сети `app-network`, чтобы обеспечить их взаимодействие.

### Фронтенд

- Написан на Angular.
- Использует Nginx для обслуживания и проксирования запросов к бэкенду.
- Файл `environment.prod.ts` настроен для API-запросов по пути `/api`.

### Бэкенд

- Написан на ASP.NET Core.
- Подключается к базе данных PostgreSQL.
- Строка подключения настроена через переменные окружения.

### База данных

- Используется PostgreSQL.
- Данные сохраняются в Docker volume `postgres_data`.

## Полезные команды

- **Запуск проекта**:

  ```bash
  docker-compose up
  ```

- **Остановка проекта**:

  ```bash
  docker-compose down
  ```

- **Пересобрать образы и запустить**:

  ```bash
  docker-compose up --build
  ```

- **Посмотреть логи**:

  ```bash
  docker-compose logs
  ```

## Если возникли проблемы

- Убедиться, что Docker и Docker Compose установлены и работают корректно.
- Проверить, не заняты ли порты 80 или 5432 другими приложениями.
- Просмотреть логи контейнеров для ошибок:

  ```bash
  docker-compose logs backend
  docker-compose logs frontend
  docker-compose logs postgres
  ```

- Попробовать пересобрать образы:

  ```bash
  docker-compose up --build
  ```

### Решение проблемы с запуском контейнера бэкенда: `exec /app/entrypoint.sh: no such file or directory`

Если при запуске контейнера бэкенда возникает ошибка:

```
backend-1 exited with code 0
backend-1 | exec /app/entrypoint.sh: no such file or directory
```

Это может быть связано с неправильными переносами строк в файле `entrypoint.sh`. Для исправления нужно:

1. Открыть файл `entrypoint.sh` в редакторе (например, в **VS Code** или **Notepad++**).
2. Убедиться, что файл сохранён с использованием **Unix (LF)** переноса строк.
   - В **VS Code**: выбрать `LF` в правом нижнем углу.
   - В **Notepad++**: перейти в **Edit -> EOL Conversion -> Unix/OSX Format**.
3. Перезапустить контейнеры:

   ```bash
   docker-compose up --build
   ```

Это решение должно устранить ошибку и позволить контейнерам запуститься корректно.

