import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AllIngredientsComponent } from './all-ingredients.component';

describe('AllIngredientsComponent', () => {
  let component: AllIngredientsComponent;
  let fixture: ComponentFixture<AllIngredientsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllIngredientsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllIngredientsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
