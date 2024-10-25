import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
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
    return this.http.post(`${this.apiUrl}/save`, { vertices });
  }

  loadAllPolygons(): Observable<any> {
    return this.http.get(`${this.apiUrl}/load-all`);
  }

  loadPolygonById(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/load/${id}`);
  }
}
