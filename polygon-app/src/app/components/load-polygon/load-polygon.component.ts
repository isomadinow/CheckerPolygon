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

  ngOnInit(): void {
  }

  loadAllPolygons() {
    this.polygonService.loadAllPolygons().subscribe((data: Polygon[]) => {
      this.polygons = data;
    }, error => {
      console.error(error);
      alert('Ошибка при загрузке полигонов.');
    });
  }

  loadPolygon(id: number) {
    this.polygonService.loadPolygonById(id).subscribe((polygon: Polygon) => {
      localStorage.setItem('loadedPolygon', JSON.stringify(polygon.vertices));
      this.router.navigate(['/']);
    }, error => {
      console.error(error);
      alert('Ошибка при загрузке полигона.');
    });
  }
}
