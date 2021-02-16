import { Component, OnInit, ViewChild } from '@angular/core';
import { ProjectService } from '../../services/project.service';
import { Project } from '../../models/project.model';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.scss']
})
export class ProjectListComponent implements OnInit {
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  data : Project[];
  displayedColumns = ['name', 'sourceLink', 'usedTechnologies', 'actions'];
  dataSource = new MatTableDataSource<Project>(this.data);

  constructor(private projectService: ProjectService,private router: Router) { }

  ngOnInit(): void {
    this.getAllProjects();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  getAllProjects(){
    let projects = this.projectService.getProjects();
    projects.subscribe(project => this.dataSource.data = project as Project[])
  }
  
  goToProject(id):void
  {
    this.router.navigate(['/project',id]);
  }
}
