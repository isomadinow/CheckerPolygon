# Проект CheckerPolygon

Привет! Это проект **CheckerPolygon**. Он состоит из бэкенда на **ASP.NET Core** и фронтенда на **Angular**. Мы используем **Docker Compose**, чтобы всё это запускать вместе.

## Структура проекта

- `/backend` — тут находится серверная часть на ASP.NET Core.
- `/polygon-app` — здесь лежит клиентская часть на Angular.
- `/assets` — картинки и другие ресурсы.

## Примеры работы программы

### Проверка точки внутри полигона

![Пример 1](assets/image_1.png)

### Результат проверки

![Пример 2](assets/image_2.png)

### Загрузка данных

![Пример 3](assets/image_3.png)

## Как запустить проект

### Требования

- Установленный **Docker** и **Docker Compose**.

### Шаги

1. **Клонируйте репозиторий**:

   ```bash
   git clone https://github.com/isomadinow/CheckerPolygon
   cd CheckerPolygon
   ```

2. **Запустите Docker Compose**:

   ```bash
   docker-compose up --build
   ```

   Эта команда соберёт образы и запустит все сервисы.

3. **Откройте приложение**:

   - **Фронтенд** будет доступен по адресу: `http://localhost:80`.
   - **Бэкенд** работает внутри Docker и доступен через фронтенд (http://localhost:5000/swagger/index.html).

## Детали проекта

### Docker Compose

В файле `docker-compose.yml` описаны три сервиса:

- **postgres** — база данных PostgreSQL.
- **backend** — серверное приложение на ASP.NET Core.
- **frontend** — клиентское приложение на Angular с Nginx.

Они все подключены к сети `app-network`, чтобы общаться между собой.

### Фронтенд

- Написан на Angular.
- Использует Nginx для обслуживания и проксирования запросов к бэкенду.
- Файл `environment.prod.ts` настроен так, чтобы API-запросы шли на `/api`.

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

- Убедитесь, что Docker и Docker Compose установлены и работают.
- Проверьте, не заняты ли порты 80 или 5432 другими приложениями.
- Посмотрите логи контейнеров для ошибок:

  ```bash
  docker-compose logs backend
  docker-compose logs frontend
  docker-compose logs postgres
  ```

- Попробуйте пересобрать образы:

  ```bash
  docker-compose up --build
  ```

