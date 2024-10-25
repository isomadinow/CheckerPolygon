// src/app/app.server.module.ts
import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';

import { AppModule } from './app.module';
import { AppComponent } from './app.component';

@NgModule({
  imports: [
    AppModule,
    ServerModule, // Важно для серверного рендеринга
  ],
  bootstrap: [AppComponent],
})
export class AppServerModule {}
