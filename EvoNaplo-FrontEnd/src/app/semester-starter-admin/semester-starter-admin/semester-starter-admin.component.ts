import { Component, OnInit, ViewChild } from '@angular/core';
import { ProjectService } from '../../services/project.service';
import { Project } from '../../models/project.model';
import { MatTableDataSource } from '@angular/material/table';
import { StudentService} from '../../services/student.service';
import { Student } from '../../models/student.model';
import { SemesterAdminProjectModel } from 'src/app/models/semesterAdminProject.model';

@Component({
  selector: 'app-semester-starter-admin',
  templateUrl: './semester-starter-admin.component.html',
  styleUrls: ['./semester-starter-admin.component.scss']
})
export class SemesterStarterAdminComponent implements OnInit {
  projects : SemesterAdminProjectModel[];
  studentsWithoutProjects : Student[];
  studentsOnProject : {};
  id : number = 4;

  constructor(private projectService: ProjectService,private studentService: StudentService) { }

  ngOnInit(): void {
    this.getStudentsOnSemesterWithoutProject();
    this.getStudentsOnProject(this.id);
  }

  getStudentsOnSemesterWithoutProject()
  {
    this.studentService.getStudentsOnSemesterWithoutProject().subscribe(students =>this.studentsWithoutProjects = students);
  }

  getStudentsOnProject(id)
  {
    this.projectService.getSemesterAdminProjects(id).subscribe(project => this.projects = project);
  }

  getStudentsFromProject(id)
  {
    let stds = this.projects[id];
  }
}
