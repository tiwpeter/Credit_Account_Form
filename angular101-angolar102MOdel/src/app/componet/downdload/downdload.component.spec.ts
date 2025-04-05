import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DowndloadComponent } from './downdload.component';

describe('DowndloadComponent', () => {
  let component: DowndloadComponent;
  let fixture: ComponentFixture<DowndloadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DowndloadComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DowndloadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
