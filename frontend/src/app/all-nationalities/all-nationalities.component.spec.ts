import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AllNationalitiesComponent } from './all-nationalities.component';

describe('AllNationalitiesComponent', () => {
  let component: AllNationalitiesComponent;
  let fixture: ComponentFixture<AllNationalitiesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllNationalitiesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllNationalitiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
