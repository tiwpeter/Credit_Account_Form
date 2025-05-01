import { TestBed } from '@angular/core/testing';

import { DowndloadService } from './downdload.service';

describe('DowndloadService', () => {
  let service: DowndloadService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DowndloadService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
