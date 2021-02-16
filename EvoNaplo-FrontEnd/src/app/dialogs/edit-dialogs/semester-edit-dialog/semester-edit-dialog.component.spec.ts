import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SemesterEditDialogComponent } from './semester-edit-dialog.component';

describe('SemesterEditDialogComponent', () => {
  let component: SemesterEditDialogComponent;
  let fixture: ComponentFixture<SemesterEditDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SemesterEditDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SemesterEditDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
