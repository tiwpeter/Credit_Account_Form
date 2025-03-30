import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoComponent } from './ro.component';

describe('RoComponent', () => {
  let component: RoComponent;
  let fixture: ComponentFixture<RoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
