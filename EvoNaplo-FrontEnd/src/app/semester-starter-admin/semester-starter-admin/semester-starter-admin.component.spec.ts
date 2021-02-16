import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SemesterStarterAdminComponent } from './semester-starter-admin.component';

describe('SemesterStarterAdminComponent', () => {
  let component: SemesterStarterAdminComponent;
  let fixture: ComponentFixture<SemesterStarterAdminComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SemesterStarterAdminComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SemesterStarterAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
