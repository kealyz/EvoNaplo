import { Component, OnInit } from '@angular/core';
import { ProjectService } from '../../services/project.service';
import { Project } from '../../models/project.model';
import { ProjectListComponent } from '../project-list/project-list.component';
import { ActivatedRoute, Router } from '@angular/router';
import { SemesterAdminProjectModel } from 'src/app/models/semesterAdminProject.model';



@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.scss']
})
export class ProjectComponent implements OnInit {

  title = 'Ez ennek a projektnek a címe';
  data : Project;
  projectId: number;
  semesterProjectView : SemesterAdminProjectModel;

  constructor(private projectService: ProjectService,private activatedRoute: ActivatedRoute,private router: Router) {}

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(paramsId => {
      this.projectId = paramsId.id;
  });
    this.getProject(this.projectId);
    this.getSemesterProjectById(this.projectId);
  }

  goBack()
  {
    this.router.navigate(['project-list']);
  }

  getProject(id){
    let project = this.projectService.getProjectById(id);
    project.subscribe(proj => this.data = proj as Project)
  }

  getSemesterProjectById(id)
  {
    let project = this.projectService.getSemesterProjectById(id);
    project.subscribe(proj => this.semesterProjectView =proj);
  }
}