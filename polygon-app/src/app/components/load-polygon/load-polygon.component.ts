import { Component, OnInit } from '@angular/core';
import { PolygonService } from '../../services/polygon.service';
import { Router } from '@angular/router';

interface Polygon {
  id: number;
  vertices: { x: number; y: number }[];
}

@Component({
  selector: 'app-load-polygon',
  templateUrl: './load-polygon.component.html',
  styleUrls: ['./load-polygon.component.css']
})
export class LoadPolygonComponent implements OnInit {
  polygons: Polygon[] = [];

  constructor(private polygonService: PolygonService, private router: Router) { }

  ngOnInit(): void {}

  loadAllPolygons() {
    this.polygonService.loadAllPolygons().subscribe((data: any) => {
      this.polygons = data.$values.map((polygon: any) => ({
        ...polygon,
        vertices: polygon.vertices.$values
      }));
    }, error => {
      console.error(error);
      alert('Ошибка при загрузке полигонов.');
    });
  }

  loadPolygon(id: number) {
    this.polygonService.loadPolygon(id).subscribe((polygon: Polygon) => {
      const vertices = polygon.vertices;
      console.log('Сохраняемые вершины в localStorage:', vertices); // Лог для проверки
      localStorage.setItem('loadedPolygon', JSON.stringify(vertices));
      this.router.navigate(['/']); // Переход на страницу канваса, замените на ваш реальный путь
    }, error => {
      console.error('Ошибка при загрузке полигона:', error);
      alert('Ошибка при загрузке полигона.');
    });
  }
  
  
}
