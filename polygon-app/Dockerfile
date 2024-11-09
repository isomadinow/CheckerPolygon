# Сборка Angular приложения
FROM node:18 AS build

WORKDIR /app
COPY polygon-app/package*.json ./
RUN npm install --legacy-peer-deps
COPY polygon-app/. .

ARG API_URL=/api
RUN echo "export const environment = { production: true, API_URL: '$API_URL' };" > src/environments/environment.prod.ts

RUN npm run build -- --output-path=dist/polygon-app/browser --configuration=production

# Stage 2: настройка Nginx для фронтенда
FROM nginx:alpine
COPY --from=build /app/dist/polygon-app/browser /usr/share/nginx/html
COPY polygon-app/nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
