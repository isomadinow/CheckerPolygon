import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

// Добавьте эти импорты
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { PolygonCanvasComponent } from './components/polygon-canvas/polygon-canvas.component';
import { LoadPolygonComponent } from './components/load-polygon/load-polygon.component';

import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

@NgModule({ declarations: [
        AppComponent,
        PolygonCanvasComponent,
        LoadPolygonComponent
    ],
    bootstrap: [AppComponent], imports: [BrowserModule,
        RouterModule,
        AppRoutingModule], providers: [provideHttpClient(withInterceptorsFromDi())] })
export class AppModule { }
