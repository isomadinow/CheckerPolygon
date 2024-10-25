import 'zone.js/node'; // Импортируйте zone.js для серверной среды
import { ngExpressEngine } from '@nguniversal/express-engine';
import express from 'express';
import { join } from 'path';
import { APP_BASE_HREF } from '@angular/common';
import { existsSync } from 'fs';
import { AppServerModule } from './src/app/app.module.server'; // Убедитесь, что путь правильный

export function app(): express.Express {
  const server = express();
  const distFolder = join(__dirname, '../browser'); // Корректный путь к браузерной сборке
  const indexHtml = existsSync(join(distFolder, 'index.original.html')) ? 'index.original.html' : 'index.html';

  // Настройка движка рендеринга
  server.engine('html', ngExpressEngine({
    bootstrap: AppServerModule,
  }));

  server.set('view engine', 'html');
  server.set('views', distFolder);

  // Обслуживаем статические файлы из /browser
  server.get('*.*', express.static(distFolder, {
    maxAge: '1y',
  }));

  // Все остальные маршруты обрабатываются через Angular Universal
  server.get('**', (req, res) => {
    res.render(indexHtml, { req, providers: [{ provide: APP_BASE_HREF, useValue: req.baseUrl }] });
  });

  return server;
}

function run(): void {
  const port = parseInt(process.env['PORT'] || '4000', 10); // Преобразуем значение в число

  // Запускаем Node сервер
  const server = app();
  server.listen(port, '0.0.0.0', () => {
    console.log(`Node Express сервер запущен на http://0.0.0.0:${port}`);
  });
}

// Убедитесь, что функция run вызывается
run();



export * from './src/main.server';
