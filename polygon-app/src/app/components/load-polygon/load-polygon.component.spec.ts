import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoadPolygonComponent } from './load-polygon.component';

describe('LoadPolygonComponent', () => {
  let component: LoadPolygonComponent;
  let fixture: ComponentFixture<LoadPolygonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LoadPolygonComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoadPolygonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
