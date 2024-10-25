import { Component, OnInit, ViewChild, ElementRef, Inject } from '@angular/core';
import { PolygonService } from '../../services/polygon.service';
import { isPlatformBrowser } from '@angular/common';
import { PLATFORM_ID } from '@angular/core';

@Component({
  selector: 'app-polygon-canvas',
  templateUrl: './polygon-canvas.component.html',
  styleUrls: ['./polygon-canvas.component.css']
})
export class PolygonCanvasComponent implements OnInit {
  @ViewChild('canvas', { static: true }) canvasRef!: ElementRef<HTMLCanvasElement>;
  private ctx!: CanvasRenderingContext2D;
  private drawing = false;
  private vertices: { x: number, y: number }[] = [];
  private point: { x: number, y: number } | null = null;
  private checkingPoint = false;

  constructor(
    private polygonService: PolygonService,
    @Inject(PLATFORM_ID) private platformId: Object
  ) { }

  ngOnInit() {
    if (isPlatformBrowser(this.platformId)) {
      const canvas = this.canvasRef.nativeElement;
      this.ctx = canvas.getContext('2d')!;


      const loadedPolygon = localStorage.getItem('loadedPolygon');
      if (loadedPolygon) {
        this.vertices = JSON.parse(loadedPolygon);
        this.drawPolygon();
        // Удаляем из локального хранилища
        localStorage.removeItem('loadedPolygon');
      }
    }
  }

  onMouseDown(event: MouseEvent) {
    if (isPlatformBrowser(this.platformId)) {
      const rect = this.canvasRef.nativeElement.getBoundingClientRect();
      const x = event.clientX - rect.left;
      const y = event.clientY - rect.top;

      if (this.checkingPoint) {
        this.point = { x, y };
        this.drawPoint();
        this.checkPointInside();
        this.checkingPoint = false;
      } else {
        this.drawing = true;
        this.vertices.push({ x, y });
        this.drawPolygon();
      }
    }
  }

  onMouseMove(event: MouseEvent) {

  }

  onMouseUp(event: MouseEvent) {
    this.drawing = false;
  }

  drawPolygon() {
    if (this.vertices.length === 0) return;

    this.ctx.clearRect(0, 0, this.canvasRef.nativeElement.width, this.canvasRef.nativeElement.height);
    this.ctx.beginPath();
    this.ctx.moveTo(this.vertices[0].x, this.vertices[0].y);

    for (let i = 1; i < this.vertices.length; i++) {
      this.ctx.lineTo(this.vertices[i].x, this.vertices[i].y);
    }

    // Если нужно замкнуть полигон
    // this.ctx.closePath();

    this.ctx.strokeStyle = 'black';
    this.ctx.lineWidth = 2;
    this.ctx.stroke();

    if (this.point) {
      this.drawPoint();
    }
  }

  drawPoint() {
    if (!this.point) return;

    this.ctx.beginPath();
    this.ctx.arc(this.point.x, this.point.y, 5, 0, 2 * Math.PI);
    this.ctx.fillStyle = 'red';
    this.ctx.fill();
  }

  savePolygon() {
    if (this.vertices.length < 3) {
      alert('Полигон должен состоять как минимум из 3 точек.');
      return;
    }

    this.polygonService.savePolygon(this.vertices).subscribe(response => {
      alert('Полигон сохранен с ID: ' + response.id);
    }, error => {
      console.error(error);
      alert('Ошибка при сохранении полигона.');
    });
  }

  resetCanvas() {
    if (isPlatformBrowser(this.platformId)) {
      this.vertices = [];
      this.point = null;
      this.ctx.clearRect(0, 0, this.canvasRef.nativeElement.width, this.canvasRef.nativeElement.height);
    }
  }

  enablePointCheck() {
    if (this.vertices.length < 3) {
      alert('Сначала создайте полигон.');
      return;
    }

    alert('Кликните на canvas, чтобы указать точку для проверки.');
    this.checkingPoint = true;
  }

  checkPointInside() {
    const request = {
      Point: this.point,
      Polygon: {
        Vertices: this.vertices
      }
    };

    this.polygonService.checkPoint(request).subscribe(response => {
      if (response.inside) {
        alert('Точка находится внутри полигона.');
      } else {
        alert('Точка находится вне полигона.');
      }
    }, error => {
      console.error(error);
      alert('Ошибка при проверке точки.');
    });
  }
}
