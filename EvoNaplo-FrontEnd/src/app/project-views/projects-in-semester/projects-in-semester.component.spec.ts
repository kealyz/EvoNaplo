import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectsInSemesterComponent } from './projects-in-semester.component';

describe('ProjectsInSemesterComponent', () => {
  let component: ProjectsInSemesterComponent;
  let fixture: ComponentFixture<ProjectsInSemesterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectsInSemesterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectsInSemesterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
