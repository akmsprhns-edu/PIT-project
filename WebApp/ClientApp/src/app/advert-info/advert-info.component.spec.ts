import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdvertInfoComponent } from './advert-info.component';

describe('AdvertInfoComponent', () => {
  let component: AdvertInfoComponent;
  let fixture: ComponentFixture<AdvertInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdvertInfoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdvertInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
