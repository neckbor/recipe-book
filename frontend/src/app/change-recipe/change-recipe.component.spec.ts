import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeRecipeComponent } from './change-recipe.component';

describe('ChangeRecipeComponent', () => {
  let component: ChangeRecipeComponent;
  let fixture: ComponentFixture<ChangeRecipeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChangeRecipeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChangeRecipeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
