import { Component, OnInit,ViewChild } from '@angular/core';
import { ProjectService } from '../../services/project.service';
import { Project } from '../../models/project.model';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserProject } from 'src/app/models/userproject.model';

@Component({
  selector: 'app-semester-starter',
  templateUrl: './semester-starter.component.html',
  styleUrls: ['./semester-starter.component.scss']
})
export class SemesterStarterComponent implements OnInit {
  private _returnUrl: string;
  projects : Project[];
  id : number = 4;
  myProject : Project;
  up : UserProject;

  constructor(private projectService: ProjectService,private _router: Router,private _route: ActivatedRoute) { }

  ngOnInit(): void {
    //this.getAllProjects();
    this._returnUrl = this._route.snapshot.queryParams['returnUrl'] || '/semester-starter';
    this.getProjectsBySemesterId(this.id);
    this.getMyProjectForTheSemester();
    this.getSubscribedProjectId();
  }

  getSubscribedProjectId()
  {
    this.projectService.getSubscribedProjectId('elso.diak@gmail.com').subscribe(userProject => this.up = userProject);
  }
  getAllProjects(){
    this.projectService.getProjects().subscribe(projects => this.projects = projects);
  }

  getProjectsBySemesterId(id)
  {
    this.projectService.getProjectsBySemesterId(id).subscribe(projects => this.projects = projects);
  }

  joinProject(id)
  {
    console.log('join eleje');
    this.projectService.assignUserToProject('elso.diak@gmail.com',id);
  }

  getMyProjectForTheSemester()
  {
    this.projectService.getMyProjectForTheSemester('elso.diak@gmail.com').subscribe(project => this.myProject = project);
  }

  deleteProjectSubscription()
  {
    var id = this.up.id;
    this.projectService.deleteProjectSubscription(id);
  }
}