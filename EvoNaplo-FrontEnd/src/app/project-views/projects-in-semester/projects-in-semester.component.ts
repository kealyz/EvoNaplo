import { Component, OnInit, ViewChild } from '@angular/core';
import { Project } from 'src/app/models/project.model';
import { Router, ActivatedRoute } from '@angular/router';
import { ProjectService } from 'src/app/services/project.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-projects-in-semester',
  templateUrl: './projects-in-semester.component.html',
  styleUrls: ['./projects-in-semester.component.scss']
})
export class ProjectsInSemesterComponent implements OnInit {

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  data : Project[];
  displayedColumns = ['name', 'sourceLink', 'usedTechnologies', 'actions'];
  dataSource = new MatTableDataSource<Project>(this.data);
  semesterId: number;

  constructor(private projectService: ProjectService,private router: Router,private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(paramsId => {
      this.semesterId = paramsId.id;
  });
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.getProjectsBySemesterId(this.semesterId);
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  
  getProjectsBySemesterId(id)
  {
    let projects = this.projectService.getProjectsBySemesterId(id);
    projects.subscribe(proj => this.dataSource.data = proj as Project[])
  }
  
  goToProject(id):void
  {
    this.router.navigate(['/project',id]);
  }
}
