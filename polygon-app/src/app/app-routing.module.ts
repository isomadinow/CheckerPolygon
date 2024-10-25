import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router'; // Импортируйте RouterModule из '@angular/router'

import { PolygonCanvasComponent } from './components/polygon-canvas/polygon-canvas.component';
import { LoadPolygonComponent } from './components/load-polygon/load-polygon.component';

const routes: Routes = [
  { path: '', component: PolygonCanvasComponent },
  { path: 'load', component: LoadPolygonComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)], // Используйте RouterModule.forRoot(routes)
  exports: [RouterModule] // Экспортируйте RouterModule
})
export class AppRoutingModule { }
