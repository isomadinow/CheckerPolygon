import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PolygonCanvasComponent } from './polygon-canvas.component';

describe('PolygonCanvasComponent', () => {
  let component: PolygonCanvasComponent;
  let fixture: ComponentFixture<PolygonCanvasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PolygonCanvasComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PolygonCanvasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
