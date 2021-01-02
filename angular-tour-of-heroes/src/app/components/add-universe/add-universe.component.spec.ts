import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddUniverseComponent } from './add-universe.component';

describe('AddUniverseComponent', () => {
  let component: AddUniverseComponent;
  let fixture: ComponentFixture<AddUniverseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddUniverseComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddUniverseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
