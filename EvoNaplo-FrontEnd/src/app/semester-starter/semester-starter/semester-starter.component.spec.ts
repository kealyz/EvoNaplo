import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SemesterStarterComponent } from './semester-starter.component';

describe('SemesterStarterComponent', () => {
  let component: SemesterStarterComponent;
  let fixture: ComponentFixture<SemesterStarterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SemesterStarterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SemesterStarterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
