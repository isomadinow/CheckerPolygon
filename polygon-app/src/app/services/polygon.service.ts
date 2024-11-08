import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PolygonService {
  private apiUrl = `${environment.API_URL}/api/Polygon`;

  
  constructor(private http: HttpClient) { }

  checkPoint(request: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/check-point`, request);
  }

  savePolygon(vertices: any[]): Observable<any> {
    // Оборачиваем массив точек в объект с полем 'vertices'
    const polygonRequest = {
      vertices: vertices
    };
  
    return this.http.post(`${this.apiUrl}/save`, polygonRequest);
  }
  

  loadAllPolygons(): Observable<any> { // Убедитесь, что возвращаете Observable
    return this.http.get(`${this.apiUrl}/load-all`);
  }

  loadPolygon(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/load/${id}`).pipe(
      map((response: any) => {
        // Извлекаем только нужные вершины из $values
        return {
          id: response.id,
          vertices: response.vertices.$values.map((v: any) => ({
            x: v.x,
            y: v.y
          }))
        };
      })
    );
  }
  
}
