import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConnectCComponent } from './connect-c.component';

describe('ConnectCComponent', () => {
  let component: ConnectCComponent;
  let fixture: ComponentFixture<ConnectCComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConnectCComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConnectCComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
